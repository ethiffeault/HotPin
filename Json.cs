/*
 * Copyright (c) 2013 Calvin Rien
 *
 * Based on the JSON parser by Patrick van Bergen
 * http://techblog.procurios.nl/k/618/news/view/14605/14863/How-do-I-write-my-own-parser-for-JSON.html
 *
 * Simplified it so that it doesn't throw exceptions
 * and can be used in Unity iPhone with maximum code stripping.
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using JsonList = System.Collections.Generic.List<object>;
using JsonObject = System.Collections.Generic.Dictionary<string, object>;

namespace HotPin
{
    // Example usage: Deserialize & Serialize
    //
    //  using UnityEngine;
    //  using System.Collections;
    //  using System.Collections.Generic;
    //  using MiniJSON;
    //
    //  public class MiniJSONTest {
    //      void Start () {
    //          var jsonString = "{ \"array\": [1.44,2,3], " +
    //                          "\"object\": {\"key1\":\"value1\", \"key2\":256}, " +
    //                          "\"string\": \"The quick brown fox \\\"jumps\\\" over the lazy dog \", " +
    //                          "\"unicode\": \"\\u3041 Men\u00fa sesi\u00f3n\", " +
    //                          "\"int\": 65536, " +
    //                          "\"float\": 3.1415926, " +
    //                          "\"bool\": true, " +
    //                          "\"null\": null }";
    //
    //          var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;
    //
    //          Debug.Log("deserialized: " + dict.GetType());
    //          Debug.Log("dict['array'][0]: " + ((List<object>) dict["array"])[0]);
    //          Debug.Log("dict['string']: " + (string) dict["string"]);
    //          Debug.Log("dict['float']: " + (double) dict["float"]); // floats come out as doubles
    //          Debug.Log("dict['int']: " + (long) dict["int"]); // int come out as longs
    //          Debug.Log("dict['unicode']: " + (string) dict["unicode"]);
    //
    //          var str = Json.Serialize(dict);
    //
    //          Debug.Log("serialized: " + str);
    //      }
    //  }
    //
    // Example usage: DeserializeObject & SerializeObject
    //
    //  public class Foo
    //  {
    //      [Json.Property]
    //      public string name = "M. Foo";
    //      [Json.Property("AGE")]
    //      public int age { get; private set; } = 41;
    //      [Json.Property]
    //      private byte level = 1;
    //      [Json.Property]
    //      private List<Doo> listOfDoo = new List<Doo>();
    //  
    //      [Json.Class]
    //      public class Doo
    //      {
    //          public float progress = 0.5f;
    //      }
    //  
    //      public Foo()
    //      {
    //          listOfDoo.Add(new Doo() { progress = 0.99f });
    //          listOfDoo.Add(new Doo() { progress = 0.01f });
    //      }
    //  
    //      public static void Test()
    //      {
    //          Foo foo = new Foo();
    //          string json = Json.SerializeType(foo);
    //          System.Console.WriteLine(Json.Prettify(json));
    //  
    //          // read it back
    //          Foo foo2 = Json.DeserializeType<Foo>(json);
    //  
    //          // output: 
    //          //
    //          //{
    //          //  "AGE": 41,
    //          //  "name": "M. Foo",
    //          //  "level": 1,
    //          //  "listOfDoo": [
    //          //    {
    //          //      "progress": 0.99
    //          //    },
    //          //    {
    //          //      "progress": 0.01
    //          //    }
    //          //  ]
    //          //}
    //      }
    //  }
    //
    // Polymorphism sample
    //
    //      [Json.Class]
    //      public class Vehicle
    //      {
    //          public int nbWheels;
    //      }
    //
    //      public class Car : Vehicle
    //      {
    //          public bool manualTransmission;
    //      }
    //
    //      class Bicycle : Vehicle
    //      {
    //          public bool hasFlatTire;
    //      }
    //
    //      List<Vehicle> vehicles = new List<Vehicle>();
    //      vehicles.Add(new Car() { nbWheels = 4, manualTransmission = true});
    //      vehicles.Add(new Bicycle() { nbWheels = 2, hasFlatTire = false });
    //
    //      // serialized to json string
    //      string json = Json.Prettify(Json.SerializeType(vehicles));
    //
    //      // deserialize from json string
    //
    //      List<Vehicle> deserializedVehicles = Json.DeserializeType<List<Vehicle>>(json);
    //
    //      Check(deserializedVehicles.Count == 2);
    //      Check(deserializedVehicles[0] is Car);
    //      Car car = deserializedVehicles[0] as Car;
    //      Check(car.nbWheels == 4);
    //      Check(car.manualTransmission == true);
    //
    //      Check(deserializedVehicles[1] is Bicycle);
    //      Bicycle bicycle = deserializedVehicles[1] as Bicycle;
    //      Check(bicycle.nbWheels == 2);
    //      Check(bicycle.hasFlatTire == false);

    public static class Json
    {
        public const string PRETTY_INDENT = "  ";
        public const string POLYMORPHISM_PROPERTY = "@type";

        /// <summary>
        /// Use to specified individual fields or property. If a name provided, it
        /// will override the c# member name.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
        public class PropertyAttribute : Attribute
        {
            public string Name { get; private set; }

            public PropertyAttribute(string name = null)
            {
                Name = name;
            }
        }

        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
        public class NonSerializeAttribute : Attribute
        {
        }

        // interface to provide serialization callback, useful to implement pattern like deprecation.
        public interface IOnSerialize
        {
            void OnSerialize(Dictionary<string, object> json);
        }

        public interface IOnDeSerialize
        {
            void OnDeSerialize(Dictionary<string, object> json);
        }

        public struct Any
        {
            public Any(object value)
            {
                Value = value;
            }

            public object Value { get; private set; }

            public bool IsString => Value is string;
            public string String => TryGetString();
            public string TryGetString(string defaultValue = null) { return IsString ? Convert.ToString(Value) : defaultValue; }

            public bool IsBool => Value is bool;
            public bool Bool => TryGetBool();
            public bool TryGetBool(bool defaultValue = false) { return IsBool ? Convert.ToBoolean(Value) : defaultValue; }

            public bool IsInt => Value is long;
            public int Int => TryGetInt(0);
            public int TryGetInt(int defaultValue = 0) { return IsInt ? Convert.ToInt32(Value) : defaultValue; }

            public bool IsNumber => Value is long;
            public long Number => TryGetNumber(0);
            public long TryGetNumber(long defaultValue = 0) { return IsNumber ? Convert.ToInt64(Value) : defaultValue; }

            public bool IsFloat => Value is long || Value is double;
            public float Float => TryGetFloat(0.0f);
            public float TryGetFloat(float defaultValue = 0.0f) { return IsDecimal ? Convert.ToSingle(Value) : defaultValue; }

            public bool IsDecimal => Value is long || Value is double;
            public double Decimal => TryGetDecimal(0);
            public double TryGetDecimal(double defaultValue = 0) { return IsDecimal ? Convert.ToDouble(Value) : defaultValue; }

            // List
            public Any this[int index] => new Any(Value is JsonList && index < ((JsonList)Value).Count ? List[index] : null);
            public bool IsList => Value is JsonList;
            public JsonList List => Value as JsonList;

            // Map
            public Any this[string key] { get => new Any(Value is JsonObject && Map.ContainsKey(key) ? Map[key] : null); }
            public bool IsMap => Value is JsonObject;
            public JsonObject Map => Value as JsonObject;

            public int Count
            {
                get
                {
                    if (IsList)
                        return List.Count;
                    else if (IsMap)
                        return Map.Count;
                    else
                        return 0;
                }
            }
        }

        /// <summary>
        /// Converts a IDictionary / IList object or a simple type (string, int, etc.) into a JSON string
        /// </summary>
        /// <param name="json">A Dictionary<string, object> or List<object></param>
        /// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
        public static string JsonToString(object jsonObj, bool pretty = false)
        {
            string result = Serializer.Serialize(jsonObj);
            if (pretty)
                result = Prettify(result);
            return result;
        }

        /// <summary>
        /// Serialize an object to json string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">object to serialize</param>
        /// <returns>json string</returns>
        public static string ToString<T>(T obj, bool pretty = false, bool polymorphism = false) where T : class
        {
            return JsonToString(ToJson(obj, polymorphism), pretty);
        }

        public static string ToString(Type type, object obj, bool pretty = false, bool polymorphism = false)
        {
            return JsonToString(ToJson(type, obj, polymorphism), pretty);
        }

        /// <summary>
        /// Parses the string json into a value
        /// </summary>
        /// <param name="json">A JSON string.</param>
        /// <returns>An List<object>, a Dictionary<string, object>, a double, an long,a string, null, true, or false</returns>
        public static object ToJson(string json)
        {
            return Parser.Parse(json);
        }

        public static JsonObject ToJsonObject(string json)
        {
            return Parser.Parse(json) as JsonObject;
        }

        public static JsonList ToJsonList(string json)
        {
            return Parser.Parse(json) as JsonList;
        }

        /// <summary>
        /// Deserialize an object from a string containing json data.
        /// 
        /// ex:
        /// 
        ///     Foo foo = Json.Deserialize<Foo>("{}");
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T ToType<T>(string jsonStr) where T : class
        {
            object json = ToJson(jsonStr);
            if (json == null)
                return null;
            return ToType<T>(json);
        }

        public static object ToType(Type type, string jsonStr)
        {
            object json = ToJson(jsonStr);
            if (json == null)
                return null;
            return ToType(type, json);
        }

        /// <summary>
        /// Deserialize an object from json Dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static T ToType<T>(object jsonObject) where T : class
        {
            object obj = DeserializeValue(jsonObject, typeof(T));
            return obj as T;
        }

        public static object ToType(Type type, object jsonObject)
        {
            object obj = DeserializeValue(jsonObject, type);
            return obj;
        }

        public static object ToJson(Type type, string jsonStr)
        {
            object json = ToJson(jsonStr);
            if (json == null)
                return null;
            return ToType(type, json);
        }

        /// <summary>
        /// Serialize an object to a json Dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">object to serialize</param>
        /// <returns>json string</returns>
        public static object ToJson<T>(T obj, bool polymorphism = false) where T : class
        {
            return SerializeValue(obj, polymorphism, typeof(T));
        }

        public static object ToJson(Type type, object obj, bool polymorphism = false)
        {
            return SerializeValue(obj, polymorphism, type);
        }

        public static JsonObject ToJsonObject<T>(T obj, bool polymorphism = false) where T : class
        {
            return SerializeValue(obj, polymorphism, typeof(T)) as JsonObject;
        }

        public static JsonList ToJsonList<T>(List<T> obj, bool polymorphism = false) where T : class
        {
            return SerializeValue(obj, polymorphism, typeof(List<T>)) as JsonList;
        }

        public static void Copy<T>(T src, ref T dst)
        {
            if (src is JsonObject srcJsonObject)
            {
                if (!(dst is JsonObject))
                    dst = (T)(object)new JsonObject();

                JsonObject dstJsonObject = dst as JsonObject;
                foreach (var keyValue in srcJsonObject)
                {
                    object dstValue;
                    dstJsonObject.TryGetValue(keyValue.Key, out dstValue);
                    Copy(keyValue.Value, ref dstValue);
                    dstJsonObject[keyValue.Key] = dstValue;
                }
            }
            else if (src is JsonList srcJsonList)
            {
                JsonList dstJsonList = dst as JsonList;
                if (dstJsonList == null)
                    dstJsonList = new JsonList(srcJsonList.Count);
                else
                    dstJsonList.Clear();
                dst = (T)(object)dstJsonList;

                foreach (object srcValue in srcJsonList)
                {
                    object dstValue = null;
                    Copy(srcValue, ref dstValue);
                    dstJsonList.Add(dstValue);
                }
            }
            else
            {
                dst = src;
            }
        }

        public static JsonObject Explode(JsonObject source)
        {
            JsonObject dest = new JsonObject();

            foreach (var pair in source)
            {
                string key = pair.Key;
                object value = pair.Value;

                string[] split = key.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                JsonObject current = dest;
                for (int i = 0; i < split.Length - 1; ++i)
                {
                    if (current.TryGetValue(split[i], out object v))
                    {
                        if (v is JsonObject)
                        {
                            current = v as JsonObject;
                        }
                        else
                        {
                            JsonObject n = new JsonObject();
                            current[split[i]] = n;
                            current = n;
                        }
                    }
                    else
                    {
                        JsonObject n = new JsonObject();
                        current[split[i]] = n;
                        current = n;
                    }
                }

                string leafKey = split[split.Length - 1];
                if (value is JsonObject valueJsonObject)
                {
                    current[leafKey] = Explode(valueJsonObject);
                }
                else
                {
                    current[leafKey] = value;
                }
            }

            return dest;

        }

        public static object Copy(object src)
        {
            if (src is JsonList list)
            {
                JsonList dst = new JsonList();
                foreach (object v in list)
                    dst.Add(Copy(v));
                return list;
            }
            else if (src is JsonObject obj)
            {
                JsonObject dst = new JsonObject();
                foreach (var kv in obj)
                    dst[kv.Key] = Copy(kv.Value);
                return dst;
            }
            else
            {
                return src;
            }
        }

        public static object Merge(object obj0, object obj1)
        {
            if (obj0 is JsonList list0)
            {
                if (obj1 is JsonList list1)
                {
                    JsonList mergeList = new JsonList();
                    int mergeSize = Math.Min(list0.Count, list1.Count);
                    for (int i = 0; i < mergeSize; ++i)
                        mergeList.Add(Merge(list0[i], list1[i]));
                    for (int i = mergeSize; i < list0.Count; ++i)
                        mergeList.Add(Copy(list0[i]));
                    for (int i = mergeSize; i < list1.Count; ++i)
                        mergeList.Add(Copy(list1[i]));
                    return mergeList;
                }
                else
                {
                    return Copy(list0);
                }
            }
            else if (obj0 is JsonObject map0)
            {
                if (obj1 is JsonObject map1)
                {
                    JsonObject mergeMap = new JsonObject();

                    foreach (var kv in map0)
                    {
                        if (map1.ContainsKey(kv.Key))
                        {
                            mergeMap[kv.Key] = Merge(kv.Value, map1[kv.Key]);
                        }
                        else
                        {
                            mergeMap[kv.Key] = Copy(kv.Value);
                        }
                    }
                    foreach (var kv in map1)
                    {
                        if (!map0.ContainsKey(kv.Key))
                            mergeMap[kv.Key] = Copy(kv.Value);
                    }
                    return mergeMap;
                }
                else
                {
                    return Copy(obj0);
                }
            }
            else
            {
                return obj0;
            }
        }

        public static void RemoveNullOrEmptyStringValue(object jsonObject)
        {
            if (jsonObject is List<object>)
            {
                List<object> list = jsonObject as List<object>;

                for (int i = list.Count - 1; i >= 0; --i)
                {
                    if (list[i] is string)
                    {
                        if (string.IsNullOrEmpty(list[i] as string))
                        {
                            list.RemoveAt(i);
                        }
                    }
                    else
                    {
                        RemoveNullOrEmptyStringValue(list[i]);
                    }
                }
            }
            else if (jsonObject is Dictionary<string, object>)
            {
                Dictionary<string, object> dictionay = jsonObject as Dictionary<string, object>;

                List<string> keys = new List<string>(dictionay.Keys);
                foreach (string key in keys)
                {
                    object value = dictionay[key];
                    if (value is string)
                    {
                        string valueStr = value as string;
                        if (string.IsNullOrEmpty(valueStr))
                            dictionay.Remove(key);
                    }
                    else if (value is Dictionary<string, object>)
                    {
                        RemoveNullOrEmptyStringValue(value);
                    }
                }
            }
        }

        // TypeDescripor cache
        private static ConcurrentDictionary<Type, TypeDescriptor> typeDescriptors = new ConcurrentDictionary<Type, TypeDescriptor>();

        // Full Type Name to Type cache
        private static ConcurrentDictionary<string, Type> typeFullNameToType = new ConcurrentDictionary<string, Type>();

        private class MemberDescriptor
        {
            public Type type;
            public FieldInfo fieldInfo;
            public PropertyInfo propertyInfo;
            public PropertyAttribute jsonProperty;
            public string propertyName;
        }

        private class TypeDescriptor
        {
            public TypeDescriptor parent = null;
            public Type type;
            public List<MemberDescriptor> getMembers = new List<MemberDescriptor>();
            public Dictionary<string, MemberDescriptor> setMembers = new Dictionary<string, MemberDescriptor>();
        }

        private static void DeserializeObject(Object obj, Dictionary<string, object> json)
        {
            TypeDescriptor typeDescriptor = GetTypeDescriptor(obj.GetType());

            if (obj is IOnDeSerialize)
            {
                ((IOnDeSerialize)obj).OnDeSerialize(json);
            }

            foreach (var pair in json)
            {
                MemberDescriptor memberDescriptor;
                if (!typeDescriptor.setMembers.TryGetValue(pair.Key, out memberDescriptor))
                    continue;

                object value = DeserializeValue(pair.Value, memberDescriptor.type);

                if (memberDescriptor.fieldInfo != null)
                    memberDescriptor.fieldInfo.SetValue(obj, value);
                else
                    memberDescriptor.propertyInfo.SetValue(obj, value);
            }

        }

        public static long ToEpochTime(DateTime time)
        {
            TimeSpan t = time.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long secondsSinceEpoch = (long)t.TotalSeconds;
            return secondsSinceEpoch;
        }

        public static DateTime FromEpochTime(long epoch)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc) + TimeSpan.FromSeconds(epoch);
        }

        private static object DeserializeValue(Object value, Type type)
        {

            if (value is long)
            {
                if (type == typeof(int))
                    return Convert.ToInt32(value);
                else if (type == typeof(float))
                    return Convert.ToSingle(value);
                else if (type == typeof(long))
                    return value;
                else if (type == typeof(double))
                    return Convert.ToDouble(value);
                else if (type == typeof(uint))
                    return Convert.ToUInt32(value);
                else if (type == typeof(ulong))
                    return Convert.ToUInt64(value);
                else if (type == typeof(sbyte))
                    return Convert.ToSByte(value);
                else if (type == typeof(byte))
                    return Convert.ToByte(value);
                else if (type == typeof(short))
                    return Convert.ToInt16(value);
                else if (type == typeof(ushort))
                    return Convert.ToUInt16(value);
                else if (type == typeof(decimal))
                    return Convert.ToDecimal(value);
                else if (type == typeof(DateTime))
                    return FromEpochTime((long)value);
                else if (type == typeof(Object))
                    return value;
                else
                    return GetDefaultValue(type);
            }
            else if (value is double)
            {
                if (type == typeof(float))
                    return Convert.ToSingle(value);
                else if (type == typeof(double))
                    return value;
                else if (type == typeof(decimal))
                    return Convert.ToDecimal(value);
                else if (type == typeof(Object))
                    return value;
                else
                    return GetDefaultValue(type);
            }
            else if (value is string)
            {
                string valueString = value as string;
                if (type == typeof(string))
                {
                    return value;
                }
                else if (type.IsEnum)
                {
                    string[] enumNames = type.GetEnumNames();
                    Array enumValues = type.GetEnumValues();
                    for (int i = 0; i < enumValues.Length; ++i)
                    {
                        if (valueString == enumNames[i])
                            return enumValues.GetValue(i);
                    }
                    return enumValues.GetValue(0);
                }
                else if (type == typeof(TimeSpan))
                {
                    TimeSpan timeSpan;
                    if (TimeSpan.TryParse(valueString, out timeSpan))
                        return timeSpan;
                    else
                        return TimeSpan.Zero;
                }
                else if (type == typeof(Object))
                {
                    return value;
                }
                else
                {
                    return GetDefaultValue(type);
                }
            }
            else if (value is bool)
            {
                if (type == typeof(bool))
                    return value;
                else if (type == typeof(Object))
                    return value;
                else
                    return GetDefaultValue(type);
            }
            else if (value is Dictionary<string, object>)
            {
                Dictionary<string, object> json = value as Dictionary<string, object>;
                if (type.GetInterface("IDictionary") != null)
                {
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                    {
                        if (type.GetGenericArguments()[0] == typeof(string))
                        {

                            IDictionary dictionary = Activator.CreateInstance(type) as IDictionary;

                            Type valueType = type.GenericTypeArguments[1];

                            foreach (var pair in json)
                            {
                                dictionary.Add(pair.Key, DeserializeValue(pair.Value, valueType));
                            }
                            return dictionary;
                        }
                    }
                    return GetDefaultValue(type);
                }
                else if (type == typeof(Object))
                {
                    Type instanceType = null;
                    object typeFullName;
                    if (json.TryGetValue(POLYMORPHISM_PROPERTY, out typeFullName) && typeFullName is string)
                    {
                        instanceType = GetType((string)typeFullName);
                    }

                    if (instanceType == null)
                    {
                        return value;
                    }
                    else
                    {
                        object instance = Activator.CreateInstance(instanceType);
                        DeserializeObject(instance, (Dictionary<string, object>)value);
                        return instance;
                    }
                }
                else if (type.IsClass || type == typeof(Object))
                {
                    Type instanceType = type;

                    object typeFullName;
                    if (json.TryGetValue(POLYMORPHISM_PROPERTY, out typeFullName) && typeFullName is string)
                    {
                        instanceType = GetType((string)typeFullName);
                        if (instanceType == null)
                            instanceType = type;
                    }

                    object instance = Activator.CreateInstance(instanceType);
                    DeserializeObject(instance, (Dictionary<string, object>)value);
                    return instance;
                }
                else
                {
                    return GetDefaultValue(type);
                }
            }
            else if (value is List<object>)
            {
                List<object> valueList = (List<object>)value;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                {
                    IList list = Activator.CreateInstance(type, valueList.Count) as IList;
                    Type elementType = type.GenericTypeArguments[0];
                    for (int i = 0; i < valueList.Count; ++i)
                        list.Add(DeserializeValue(valueList[i], elementType));
                    return list;
                }
                else if (type.IsArray)
                {
                    Type elementType = type.GetElementType();
                    IList array = Array.CreateInstance(elementType, valueList.Count);
                    for (int i = 0; i < valueList.Count; ++i)
                        array[i] = DeserializeValue(valueList[i], elementType);
                    return array;
                }
                else if (type == typeof(ArrayList))
                {
                    ArrayList arrayList = new ArrayList(valueList.Count);
                    for (int i = 0; i < valueList.Count; ++i)
                        arrayList.Add(DeserializeValue(valueList[i], valueList[i].GetType()));
                    return arrayList;
                }
                else if (type == typeof(Object))
                {
                    return value;
                }
                else
                {
                    return GetDefaultValue(type);
                }
            }
            else
            {
                return GetDefaultValue(type);
            }
        }

        private static Object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            else
            {
                return null;
            }
        }

        private static void SerializeObject(Object obj, Dictionary<string, object> json, bool polymorphism, Type expectedType = null)
        {
            Type type = obj.GetType();

            if (polymorphism)
            {
                json.Add(POLYMORPHISM_PROPERTY, type.FullName);
            }

            TypeDescriptor typeDescriptor = GetTypeDescriptor(type);

            List<TypeDescriptor> typeDescriptors = new List<TypeDescriptor>();
            {
                TypeDescriptor current = typeDescriptor;
                while (current != null)
                {
                    typeDescriptors.Insert(0, current);
                    current = current.parent;
                }
            }

            foreach (TypeDescriptor current in typeDescriptors)
            {
                for (int i = 0; i < current.getMembers.Count; ++i)
                {
                    MemberDescriptor info = current.getMembers[i];

                    Object value;
                    if (info.fieldInfo != null)
                        value = info.fieldInfo.GetValue(obj);
                    else
                        value = info.propertyInfo.GetValue(obj);

                    json.Add(info.propertyName, SerializeValue(value, polymorphism, info.type));
                }
            }

            if (obj is IOnSerialize)
            {
                ((IOnSerialize)obj).OnSerialize(json);
            }
        }

        private static object SerializeValue(object value, bool polymorphism, Type expectedType = null)
        {
            if (value == null)
                return null;

            if (IsInteger(value))
            {
                return Convert.ToInt64(value);
            }
            else if (IsFloat(value))
            {
                return Convert.ToSingle(value);
            }
            else if (value is string)
            {
                return value;
            }
            else if (value is bool)
            {
                return value;
            }
            else if (value.GetType().IsEnum)
            {
                return value.ToString();
            }
            else if (value is DateTime)
            {
                return ToEpochTime((DateTime)value);
            }
            else if (value is TimeSpan)
            {
                return ((TimeSpan)value).ToString();
            }
            else if (value is IDictionary)
            {
                return SerializeDictionary(value as IDictionary, polymorphism);
            }
            else if (value is IEnumerable)
            {
                return SerializeEnumerable(value as IEnumerable, polymorphism);
            }
            else if (value is object)
            {
                Dictionary<string, object> valueJson = new Dictionary<string, object>();
                SerializeObject(value, valueJson, polymorphism, expectedType);
                return valueJson;
            }
            else
            {
                return null;
            }
        }

        private static List<object> SerializeEnumerable(IEnumerable enumerable, bool polymorphism)
        {
            Type expectedType = null;
            Type enumerableType = enumerable.GetType();

            if (enumerableType.IsGenericType && enumerableType.GetGenericTypeDefinition() == typeof(List<>))
                expectedType = enumerableType.GetGenericArguments()[0];
            else if (enumerableType.IsArray)
                expectedType = enumerableType.GetElementType();

            List<object> jsonList = new List<object>();
            foreach (object obj in enumerable)
                jsonList.Add(SerializeValue(obj, polymorphism, expectedType));
            return jsonList;
        }

        private static Dictionary<string, object> SerializeDictionary(IDictionary dictionary, bool polymorphism)
        {
            Type expectedType = null;
            Type dictionaryType = dictionary.GetType();
            if (dictionaryType.IsGenericType && dictionaryType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                expectedType = dictionaryType.GetGenericArguments()[1];
            }

            Dictionary<string, object> json = new Dictionary<string, object>();

            foreach (DictionaryEntry entry in dictionary)
            {
                if (entry.Key is string)
                    json.Add(entry.Key as string, SerializeValue(entry.Value, polymorphism, expectedType));
            }

            return json;
        }

        private static bool IsInteger(object value)
        {
            return value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort;
        }

        private static bool IsFloat(object value)
        {
            return value is float
                    || value is double
                    || value is decimal;
        }

        private static Type SlowGetType(string fullTypeName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                Type type = assembly.GetType(fullTypeName);
                if (type != null)
                    return type;
            }
            return null;
        }

        private static Type GetType(string fullTypeName)
        {
            Type type;
            if (typeFullNameToType.TryGetValue(fullTypeName, out type))
                return type;

            type = SlowGetType(fullTypeName);

            return typeFullNameToType.GetOrAdd(fullTypeName, type);
        }

        private static TypeDescriptor GetTypeDescriptor(Type type)
        {
            TypeDescriptor typeDescriptor;
            if (typeDescriptors.TryGetValue(type, out typeDescriptor))
                return typeDescriptor;

            typeDescriptor = new TypeDescriptor();
            typeDescriptor.type = type;

            if (type.BaseType != typeof(object))
            {
                typeDescriptor.parent = GetTypeDescriptor(type.BaseType);

                // for fast get, merge setMembers with parents
                foreach (var pair in typeDescriptor.parent.setMembers)
                    typeDescriptor.setMembers.Add(pair.Key, pair.Value);
            }

            MemberInfo[] allMembers = type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            for (int i = 0; i < allMembers.Length; ++i)
            {
                MemberInfo memberInfo = allMembers[i];

                if (memberInfo.IsDefined(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false))
                    continue;

                // don't serialize [NonSerialize]
                NonSerializeAttribute nonSerializeAttribute = memberInfo.GetCustomAttribute<NonSerializeAttribute>();
                if (nonSerializeAttribute != null)
                    continue;

                // if not Class or SerializeAllProperties is false, Property is need to serialized
                PropertyAttribute property = memberInfo.GetCustomAttribute<PropertyAttribute>();


                var attrs = memberInfo.GetCustomAttributes();

                if (memberInfo.IsDefined(typeof(CompilerGeneratedAttribute), false))
                    continue;

                string name = memberInfo.Name;
                if (property != null && property.Name != null)
                    name = property.Name;

                if (memberInfo is PropertyInfo propertyInfo)
                {
                    MemberDescriptor memberDescriptor
                        = new MemberDescriptor()
                        {
                            propertyInfo = propertyInfo,
                            type = propertyInfo.PropertyType,
                            jsonProperty = property,
                            propertyName = name
                        };

                    if (propertyInfo.CanRead)
                        typeDescriptor.getMembers.Add(memberDescriptor);

                    if (propertyInfo.CanWrite)
                        typeDescriptor.setMembers.Add(name, memberDescriptor);
                }
                else if (memberInfo is FieldInfo)
                {
                    FieldInfo fieldInfo = (FieldInfo)memberInfo;

                    // detect if it's compilator generated, CompilerGeneratedAttribute is not working !?
                    if (fieldInfo.Name.StartsWith("<"))
                        continue;

                    MemberDescriptor memberDescriptor
                        = new MemberDescriptor()
                        {
                            fieldInfo = fieldInfo,
                            type = fieldInfo.FieldType,
                            jsonProperty = property,
                            propertyName = name
                        };

                    typeDescriptor.getMembers.Add(memberDescriptor);
                    typeDescriptor.setMembers.Add(name, memberDescriptor);
                }
            }

            return typeDescriptors.GetOrAdd(type, typeDescriptor);
        }

        // https://stackoverflow.com/questions/4580397/json-formatter-in-c
        public static string Prettify(string str, string indentation = PRETTY_INDENT)
        {
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        char next = i < str.Length - 1 ? str[i + 1] : char.MinValue;
                        if (next == ']' || next == '}')
                        {
                            sb.Append(ch);
                            if (next == ']')
                                sb.Append(']');
                            else
                                sb.Append('}');
                            ++i;
                        }
                        else
                        {
                            sb.Append(ch);
                            if (!quoted)
                            {
                                sb.AppendLine();
                                ++indent;
                                for (int item = 0; item < indent; ++item)
                                    sb.Append(indentation);
                            }
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            --indent;
                            for (int item = 0; item < indent; ++item)
                                sb.Append(indentation);
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            for (int item = 0; item < indent; ++item)
                                sb.Append(indentation);
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// This class encodes and decodes JSON strings.
        /// Spec. details, see http://www.json.org/
        ///
        /// JSON uses Arrays and Objects. These correspond here to the datatypes IList and IDictionary.
        /// All numbers are parsed to doubles.
        /// </summary>

        sealed class Parser : IDisposable
        {
            const string WORD_BREAK = "{}[],:\"";

            public static bool IsWordBreak(char c)
            {
                return Char.IsWhiteSpace(c) || WORD_BREAK.IndexOf(c) != -1;
            }

            enum TOKEN
            {
                NONE,
                CURLY_OPEN,
                CURLY_CLOSE,
                SQUARED_OPEN,
                SQUARED_CLOSE,
                COLON,
                COMMA,
                STRING,
                NUMBER,
                TRUE,
                FALSE,
                NULL
            };

            StringReader json;

            Parser(string jsonString)
            {
                json = new StringReader(jsonString);
            }

            public static object Parse(string jsonString)
            {
                if (jsonString == null)
                    return null;

                using (var instance = new Parser(jsonString))
                {
                    return instance.ParseValue();
                }
            }

            public void Dispose()
            {
                json.Dispose();
                json = null;
            }

            Dictionary<string, object> ParseObject()
            {
                Dictionary<string, object> table = new Dictionary<string, object>();

                // ditch opening brace
                json.Read();

                // {
                while (true)
                {
                    switch (NextToken)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMA:
                            continue;
                        case TOKEN.CURLY_CLOSE:
                            return table;
                        default:
                            // name
                            string name = ParseString();
                            if (name == null)
                            {
                                return null;
                            }

                            // :
                            if (NextToken != TOKEN.COLON)
                            {
                                return null;
                            }
                            // ditch the colon
                            json.Read();

                            // value
                            table[name] = ParseValue();
                            break;
                    }
                }
            }

            List<object> ParseArray()
            {
                List<object> array = new List<object>();

                // ditch opening bracket
                json.Read();

                // [
                var parsing = true;
                while (parsing)
                {
                    TOKEN nextToken = NextToken;

                    switch (nextToken)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMA:
                            continue;
                        case TOKEN.SQUARED_CLOSE:
                            parsing = false;
                            break;
                        default:
                            object value = ParseByToken(nextToken);

                            array.Add(value);
                            break;
                    }
                }

                return array;
            }

            object ParseValue()
            {
                TOKEN nextToken = NextToken;
                return ParseByToken(nextToken);
            }

            object ParseByToken(TOKEN token)
            {
                switch (token)
                {
                    case TOKEN.STRING:
                        return ParseString();
                    case TOKEN.NUMBER:
                        return ParseNumber();
                    case TOKEN.CURLY_OPEN:
                        return ParseObject();
                    case TOKEN.SQUARED_OPEN:
                        return ParseArray();
                    case TOKEN.TRUE:
                        return true;
                    case TOKEN.FALSE:
                        return false;
                    case TOKEN.NULL:
                        return null;
                    default:
                        return null;
                }
            }

            string ParseString()
            {
                StringBuilder s = new StringBuilder();
                char c;

                // ditch opening quote
                json.Read();

                bool parsing = true;
                while (parsing)
                {

                    if (json.Peek() == -1)
                    {
                        parsing = false;
                        break;
                    }

                    c = NextChar;
                    switch (c)
                    {
                        case '"':
                            parsing = false;
                            break;
                        case '\\':
                            if (json.Peek() == -1)
                            {
                                parsing = false;
                                break;
                            }

                            c = NextChar;
                            switch (c)
                            {
                                case '"':
                                case '\\':
                                case '/':
                                    s.Append(c);
                                    break;
                                case 'b':
                                    s.Append('\b');
                                    break;
                                case 'f':
                                    s.Append('\f');
                                    break;
                                case 'n':
                                    s.Append('\n');
                                    break;
                                case 'r':
                                    s.Append('\r');
                                    break;
                                case 't':
                                    s.Append('\t');
                                    break;
                                case 'u':
                                    var hex = new char[4];

                                    for (int i = 0; i < 4; i++)
                                    {
                                        hex[i] = NextChar;
                                    }

                                    s.Append((char)Convert.ToInt32(new string(hex), 16));
                                    break;
                                default:
                                    s.Append("\\");
                                    s.Append(c);
                                    break;
                            }
                            break;
                        default:
                            s.Append(c);
                            break;
                    }
                }

                return s.ToString();
            }

            object ParseNumber()
            {
                string number = NextWord;

                if (number.IndexOf('.') == -1)
                {
                    long parsedInt;
                    Int64.TryParse(number, out parsedInt);
                    return parsedInt;
                }

                double parsedDouble;
                Double.TryParse(number, out parsedDouble);
                return parsedDouble;
            }

            void EatWhitespace()
            {
                while (Char.IsWhiteSpace(PeekChar))
                {
                    json.Read();

                    if (json.Peek() == -1)
                    {
                        break;
                    }
                }
            }

            char PeekChar => Convert.ToChar(json.Peek());

            char NextChar => Convert.ToChar(json.Read());

            string NextWord
            {
                get
                {
                    StringBuilder word = new StringBuilder();

                    while (!IsWordBreak(PeekChar))
                    {
                        word.Append(NextChar);

                        if (json.Peek() == -1)
                        {
                            break;
                        }
                    }

                    return word.ToString();
                }
            }

            TOKEN NextToken
            {
                get
                {
                    EatWhitespace();

                    if (json.Peek() == -1)
                    {
                        return TOKEN.NONE;
                    }

                    switch (PeekChar)
                    {
                        case '{':
                            return TOKEN.CURLY_OPEN;
                        case '}':
                            json.Read();
                            return TOKEN.CURLY_CLOSE;
                        case '[':
                            return TOKEN.SQUARED_OPEN;
                        case ']':
                            json.Read();
                            return TOKEN.SQUARED_CLOSE;
                        case ',':
                            json.Read();
                            return TOKEN.COMMA;
                        case '"':
                            return TOKEN.STRING;
                        case ':':
                            return TOKEN.COLON;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        case '-':
                            return TOKEN.NUMBER;
                    }

                    switch (NextWord)
                    {
                        case "false":
                            return TOKEN.FALSE;
                        case "true":
                            return TOKEN.TRUE;
                        case "null":
                            return TOKEN.NULL;
                    }

                    return TOKEN.NONE;
                }
            }
        }

        sealed class Serializer
        {
            StringBuilder builder;

            Serializer()
            {
                builder = new StringBuilder();
            }

            public static string Serialize(object obj)
            {
                var instance = new Serializer();

                instance.SerializeValue(obj);

                return instance.builder.ToString();
            }

            void SerializeValue(object value)
            {
                IList asList;
                IDictionary asDict;
                string asStr;

                if (value == null)
                {
                    builder.Append("null");
                }
                else if ((asStr = value as string) != null)
                {
                    SerializeString(asStr);
                }
                else if (value is bool)
                {
                    builder.Append((bool)value ? "true" : "false");
                }
                else if ((asList = value as IList) != null)
                {
                    SerializeArray(asList);
                }
                else if ((asDict = value as IDictionary) != null)
                {
                    SerializeObject(asDict);
                }
                else if (value is char)
                {
                    SerializeString(new string((char)value, 1));
                }
                else
                {
                    SerializeOther(value);
                }
            }

            void SerializeObject(IDictionary obj)
            {
                bool first = true;

                builder.Append('{');

                foreach (object e in obj.Keys)
                {
                    if (!first)
                    {
                        builder.Append(',');
                    }

                    SerializeString(e.ToString());
                    builder.Append(':');

                    SerializeValue(obj[e]);

                    first = false;
                }

                builder.Append('}');
            }

            void SerializeArray(IList anArray)
            {
                builder.Append('[');

                bool first = true;

                foreach (object obj in anArray)
                {
                    if (!first)
                    {
                        builder.Append(',');
                    }

                    SerializeValue(obj);

                    first = false;
                }

                builder.Append(']');
            }

            void SerializeString(string str)
            {
                builder.Append('\"');

                char[] charArray = str.ToCharArray();
                foreach (var c in charArray)
                {
                    switch (c)
                    {
                        case '"':
                            builder.Append("\\\"");
                            break;
                        case '\\':
                            builder.Append("\\\\");
                            break;
                        case '\b':
                            builder.Append("\\b");
                            break;
                        case '\f':
                            builder.Append("\\f");
                            break;
                        case '\n':
                            builder.Append("\\n");
                            break;
                        case '\r':
                            builder.Append("\\r");
                            break;
                        case '\t':
                            builder.Append("\\t");
                            break;
                        default:
                            int codepoint = Convert.ToInt32(c);
                            if ((codepoint >= 32) && (codepoint <= 126))
                            {
                                builder.Append(c);
                            }
                            else
                            {
                                builder.Append("\\u");
                                builder.Append(codepoint.ToString("x4"));
                            }
                            break;
                    }
                }

                builder.Append('\"');
            }

            void SerializeOther(object value)
            {
                // NOTE: decimals lose precision during serialization.
                // They always have, I'm just letting you know.
                // Previously floats and doubles lost precision too.
                if (value is float)
                {
                    builder.Append(((float)value).ToString("R"));
                }
                else if (value is int
                  || value is uint
                  || value is long
                  || value is sbyte
                  || value is byte
                  || value is short
                  || value is ushort
                  || value is ulong)
                {
                    builder.Append(value);
                }
                else if (value is double
                  || value is decimal)
                {
                    builder.Append(Convert.ToDouble(value).ToString("R"));
                }
                else
                {
                    SerializeString(value.ToString());
                }
            }
        }
    }

    public abstract class JsonException : Exception
    {
        public JsonException(string msg, params object[] args)
            : base(args.Length == 0 ? msg : string.Format(msg, args))
        { }

        public JsonException(Exception inner, string msg, params object[] args)
            : base(args.Length == 0 ? msg : string.Format(msg, args), inner)
        { }
    }

    public class JsonParseException : JsonException
    {
        public JsonParseException(string msg, params object[] args)
            : base(msg, args)
        { }

        public JsonParseException(Exception inner, string msg, params object[] args)
            : base(inner, msg, args)
        { }
    }

    public class JsonShemaException : JsonException
    {
        public List<string> Errors { get; private set; }

        public JsonShemaException(List<string> errors)
        : base("  " + string.Join("\n  ", errors))
        {
            Errors = errors;
        }
    }
}
