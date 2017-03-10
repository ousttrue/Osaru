using ObjectStructure.Serialization.Serializers;
using System.IO;


namespace ObjectStructure.MessagePack
{
    public static class ISerializerExtensions
    {
        public static BytesSegment SerializeToMessagePack<T>(this SerializerBase<T> s, T o)
        {
            var f = new MessagePackFormatter();
            s.Serialize(o, f);
            return f.GetStore().Bytes;
        }
    }
}
