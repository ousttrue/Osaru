using ObjectStructure.Json;
using System.Text;


namespace ObjectStructure.Serialization.Serializers
{
    public static class ISerializerExtensions
    {
        public static void Serialize<T>(this SerializerBase<T> s, object o, IFormatter f)
        {
            s.Serialize((T)o, f);
        }
    }
}
