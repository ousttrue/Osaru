using Osaru.MessagePack;
using Osaru.Serialization;
using System;


namespace Osaru
{
    public static partial class TypeRegistryExtensions
    {
        public static ArraySegment<Byte> SerializeToMessagePack<T>(this TypeRegistry r, T value)
        {
            var s = r.GetSerializer<T>();
            return s.SerializeToMessagePack(value);
        }

        public static ArraySegment<Byte> SerializeToMessagePackMap<K0, T0, K1, T1>(this TypeRegistry r
            , K0 key0, T0 value0
            , K1 key1, T1 value1
            )
        {
            var f = new MessagePackFormatter();

            f.BeginMap(2);
            r.GetSerializer<K0>().Serialize(key0, f);
            r.GetSerializer<T0>().Serialize(value0, f);
            r.GetSerializer<K1>().Serialize(key1, f);
            r.GetSerializer<T1>().Serialize(value1, f);
            f.EndMap();

            return f.GetStore().Bytes;
        }

        public static void Deserialize<PARSER, T>(this TypeRegistry r, PARSER parser, ref T outValue)
            where PARSER: IParser<PARSER>
        {
            var d = r.GetDeserializer<T>();
            d.Deserialize(parser, ref outValue);
        }
    }
}
