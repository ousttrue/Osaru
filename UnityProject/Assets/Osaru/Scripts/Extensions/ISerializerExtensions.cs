using Osaru.Serialization.Serializers;


namespace Osaru
{
    public static partial class ISerializerExtensions
    {
        public static void Serialize<T>(this SerializerBase<T> s, object o, IFormatter f)
        {
            s.Serialize((T)o, f);
        }
    }
}
