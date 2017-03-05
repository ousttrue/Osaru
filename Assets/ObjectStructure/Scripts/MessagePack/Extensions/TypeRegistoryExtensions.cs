using ObjectStructure.Serialization;
using System;


namespace ObjectStructure.MessagePack
{
    public static class TypeRegistoryExtensions
    {
        public static Byte[] SerializeToMessagePack<T>(this TypeRegistory r, T value)
        {
            var s = r.GetSerializer<T>();
            return s.SerializeToMessagePack(value);
        }

        public static Byte[] SerializeToMessagePackMap<K0, T0, K1, T1>(this TypeRegistory r
            , K0 key0, T0 value0
            , K1 key1, T1 value1
            )
        {
            var f = new MessagePackFormatter();

            f.OpenMap(2);
            r.GetSerializer<K0>().Serialize(key0, f);
            r.GetSerializer<T0>().Serialize(value0, f);
            r.GetSerializer<K1>().Serialize(key1, f);
            r.GetSerializer<T1>().Serialize(value1, f);
            f.CloseMap();

            return (Byte[])f.Result();
        }
    }
}
