using ObjectStructure.Serialization.Serializers;
using System;


namespace ObjectStructure.MessagePack
{
    public static class ISerializerExtensions
    {
        public static ArraySegment<Byte> SerializeToMessagePack<T>(this SerializerBase<T> s, T o)
        {
            var f = new MessagePackFormatter();
            s.Serialize(o, f);
            return f.GetStore().Bytes;
        }
    }
}
