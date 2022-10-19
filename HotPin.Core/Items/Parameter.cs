using System;
using System.Reflection;
using System.Text;

namespace HotPin
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Parameter : Attribute
    {
        public string Description { get; set; }


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
                Parameter parameter = property.GetCustomAttribute<Parameter>();

                if (parameter != null)
                {
                    output.AppendLine(property.Name);
                    output.Append("    ");
                    output.AppendLine(parameter.Description);

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
