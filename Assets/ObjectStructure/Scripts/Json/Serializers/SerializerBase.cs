using System.Text;


namespace ObjectStructure.Json.Serializers
{
    public interface ISerializer<T> : Serialization.ITypeInitializer
    {
        void Serialize(T t, IWriteStream w);
    }

    public static class SerializerBaseExtensions
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
