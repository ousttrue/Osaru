using ObjectStructure.Serialization.Serializers;
using System.Text;


namespace ObjectStructure.Json
{
    public static class ISerializerExtensions
    {
        public static string SerializeToJson<T>(this SerializerBase<T> s, T o)
        {
            var sb = new StringBuilder();
            var w = new StringBuilderStream(sb);
            var f = new JsonFormatter(w);
            s.Serialize(o, f);
            return sb.ToString();
        }
    }
}
