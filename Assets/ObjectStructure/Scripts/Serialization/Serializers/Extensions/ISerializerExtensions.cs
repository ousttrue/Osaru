using ObjectStructure.Json;
using System.Text;


namespace ObjectStructure.Serialization.Serializers
{
    public static class ISerializerExtensions
    {
        public static void Serialize<T>(this ISerializer<T> s, object o, IFormatter f)
        {
            s.Serialize((T)o, f);
        }

        public static string SerializeToJson<T>(this ISerializer<T> s, object o)
        {
            var sb = new StringBuilder();
            var w = new StringBuilderStream(sb);
            var f = new JsonFormatter(w);
            s.Serialize(o, f);
            return sb.ToString();
        }
    }
}
