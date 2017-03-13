using Osaru.Serialization.Serializers;
using System;


namespace Osaru.MessagePack
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
