using ObjectStructure.Serialization.Deserializers;
using ObjectStructure.Serialization.Serializers;
using System.Collections.Generic;


namespace ObjectStructure.Serialization
{
    public static class EmbededTypeSerializations
    {
        public static IEnumerable<TypeSerialization> Serializations
        {
            get
            {
                yield return TypeSerialization.Create(
                    (x, f) => f.Value(x)
                    , new BooleanDeserializer()
                    );

                yield return TypeSerialization.Create(
                    (x, f) => f.Value(x)
                    , new SByteDeserializer()
                    );

                yield return TypeSerialization.Create(
                    (x, f) => f.Value(x)
                    , new Int16Deserializer()
                    );

                yield return TypeSerialization.Create(
                    (x, f) => f.Value(x)
                    , new Int32Deserializer()
                );

                yield return TypeSerialization.Create(
                    (x, f) => f.Value(x)
                    , new Int64Deserializer()
                );

                yield return TypeSerialization.Create(
                    (x, f) => f.Value(x)
                    , new ByteDeserializer()
                );

                yield return TypeSerialization.Create(
                    (x, f) => f.Value(x)
                    , new UInt16Deserializer()
                );
                yield return TypeSerialization.Create(
                    (x, f) => f.Value(x)
                    , new UInt32Deserializer()
                );

                yield return TypeSerialization.Create(
                    (x, f) => f.Value(x)
                    , new UInt64Deserializer()
                );
                yield return TypeSerialization.Create(
                    (x, f) => f.Value(x)
                    , new SingleDeserializer()
                );

                yield return TypeSerialization.Create(
                    (x, f) => f.Value(x)
                    , new DoubleDeserializer()
                );

                yield return TypeSerialization.Create(
                    (x, f) => f.Value(x)
                    , new StringDeserializer()
                );

                yield return TypeSerialization.Create(
                    new RawSerializer()
                    , new RawDeserializer()
                );
            }
        }
    }
}
