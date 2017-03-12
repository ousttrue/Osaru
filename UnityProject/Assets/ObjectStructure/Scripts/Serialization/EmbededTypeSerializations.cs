using ObjectStructure.Serialization.Deserializers;
using ObjectStructure.Serialization.Serializers;
using System;
using System.Collections.Generic;


namespace ObjectStructure.Serialization
{
    public static class EmbededSerializations
    {
        public static IEnumerable<TypeSerialization> Serializations
        {
            get
            {
                yield return TypeSerialization.Create(
                    new LambdaSerializer<Boolean>((x, f) => f.Value(x))
                    , new BooleanDeserializer()
                    );

                yield return TypeSerialization.Create(
                    new LambdaSerializer<SByte>((x, f) => f.Value(x))
                    , new SByteDeserializer()
                    );

                yield return TypeSerialization.Create(
                    new LambdaSerializer<Int16>((x, f) => f.Value(x))
                    , new Int16Deserializer()
                    );

                yield return TypeSerialization.Create(
                    new LambdaSerializer<Int32>((x, f) => f.Value(x))
                    , new Int32Deserializer()
                );

                yield return TypeSerialization.Create(
                    new LambdaSerializer<Int64>((x, f) => f.Value(x))
                    , new Int64Deserializer()
                );

                yield return TypeSerialization.Create(
                    new LambdaSerializer<Byte>((x, f) => f.Value(x))
                    , new ByteDeserializer()
                );

                yield return TypeSerialization.Create(
                    new LambdaSerializer<UInt16>((x, f) => f.Value(x))
                    , new UInt16Deserializer()
                );
                yield return TypeSerialization.Create(
                    new LambdaSerializer<UInt32>((x, f) => f.Value(x))
                    , new UInt32Deserializer()
                );

                yield return TypeSerialization.Create(
                    new LambdaSerializer<UInt64>((x, f) => f.Value(x))
                    , new UInt64Deserializer()
                );
                yield return TypeSerialization.Create(
                    new LambdaSerializer<Single>((x, f) => f.Value(x))
                    , new SingleDeserializer()
                );

                yield return TypeSerialization.Create(
                    new LambdaSerializer<Double>((x, f) => f.Value(x))
                    , new DoubleDeserializer()
                );

                yield return TypeSerialization.Create(
                    new LambdaSerializer<String>((x, f) => f.Value(x))
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
