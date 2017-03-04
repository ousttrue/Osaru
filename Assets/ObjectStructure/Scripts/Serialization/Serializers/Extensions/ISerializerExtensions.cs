using System.Text;


namespace ObjectStructure.Serialization.Serializers
{
    public static class ISerializerExtensions
    {
        public static void Serialize<T>(this ISerializer<T> s, object o, IWriteStream w)
        {
            s.Serialize((T)o, w);
        }

        public static string Serialize<T>(this ISerializer<T> s, object o)
        {
            var sb = new StringBuilder();
            var w = new StringBuilderStream(sb);
            s.Serialize(o, w);
            return sb.ToString();
        }
    }
}
