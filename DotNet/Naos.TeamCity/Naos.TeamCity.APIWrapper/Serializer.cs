namespace Naos.TeamCity.APIWrapper
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    public class Serializer
    {
        private static Type s_objectType = typeof(object);

        public static T Deserialize<T>(string rawText)
        {
            var type = typeof(T);

            if (type == typeof(string))
            {
                return (T) ((object)rawText);
            }

            XmlRootAttribute r = GetRootAttribute(type);
            var s = r == null ? new XmlSerializer(type) : new XmlSerializer(type, r);

            var sr = new StringReader(rawText);
            var ret = s.Deserialize(sr);
            return (T)ret;
        }

        public static string Serialize<T>(T obj)
        {
            var type = typeof(T);
            if (type.Equals(s_objectType))
            {
                type = obj.GetType();
            }

            XmlRootAttribute r = GetRootAttribute(type);
            var s = r == null ? new XmlSerializer(type) : new XmlSerializer(type, r);

            var sw = new StringWriter();
            s.Serialize(sw, obj);
            return sw.ToString();
        }

        private static XmlRootAttribute GetRootAttribute(Type type)
        {
            XmlRootAttribute ret = null;
            if (type.IsArray)
            {
                var attr = type.GetElementType().GetCustomAttributes(typeof(XmlRootAttribute), false).FirstOrDefault() as XmlRootAttribute;
                if (attr != null)
                {
                    var name = attr.ElementName;
                    if (name.EndsWith("y"))
                    {
                        name = name.Substring(0, name.Length - 1) + "ies";
                    }
                    else
                    {
                        name = name + 's';
                    }

                    return new XmlRootAttribute(name);
                }
            }

            return ret;
        }
    }
}