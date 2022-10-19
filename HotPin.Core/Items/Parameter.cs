using System;
using System.Reflection;
using System.Text;

namespace HotPin
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Description : Attribute
    {
        public string Value { get; set; }

        public Description(string value)
        {
            Value = value;
        }

        public static string GetDocumentation(Type type)
        {
            StringBuilder output = new StringBuilder();

            PropertyInfo[] properties = type.GetProperties(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.FlattenHierarchy);

            Array.Sort(properties, (x, y) => x.Name.CompareTo(y.Name));

            output.AppendLine($"{type.Name}");
            output.AppendLine();

            foreach (PropertyInfo property in properties)
            {
                Description parameter = property.GetCustomAttribute<Description>();

                if (parameter != null)
                {
                    output.AppendLine(property.Name);
                    output.Append("    ");
                    output.AppendLine(parameter.Value);

                    Type docType = property.PropertyType;
                    if (property.PropertyType.IsGenericType)
                    {
                        docType = property.PropertyType.GenericTypeArguments[property.PropertyType.GenericTypeArguments.Length - 1];
                    }

                    if (docType.IsEnum)
                    {
                        string[] enumValue = Enum.GetNames(docType);
                        output.AppendLine("values:");
                        output.Append("    ");
                        output.AppendLine(string.Join(",", enumValue));
                    }
                    output.AppendLine();
                }
            }

            return output.ToString();
        }
    }
}
