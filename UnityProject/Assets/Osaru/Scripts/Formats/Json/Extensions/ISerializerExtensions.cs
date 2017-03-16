using Osaru.Serialization.Serializers;
using System;
using System.Text;


namespace Osaru.Json
{
    public static class ISerializerExtensions
    {
        public static string SerializeToJson<T>(this SerializerBase<T> s, T o)
        {
            var sb = new StringBuilder();
            var w = new StringBuilderStore(sb);
            var f = new JsonFormatter(w);
            s.Serialize(o, f);
            return sb.ToString();
        }

        public static ArraySegment<Byte> SerializeToJsonBytes<T>(this SerializerBase<T> s, T o)
        {
            var f = new JsonFormatter();
            s.Serialize(o, f);
            return f.GetStore().Bytes;
        }
    }
}
