using Osaru.MessagePack;
using Osaru.Serialization.Serializers;
using System;


namespace Osaru
{
    public static partial class ISerializerExtensions
    {
        public static ArraySegment<Byte> SerializeToMessagePack<T>(this SerializerBase<T> s, T o)
        {
            var f = new MessagePackFormatter();
            s.Serialize(o, f);
            return f.GetStore().Bytes;
        }
    }
}
