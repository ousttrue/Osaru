using System;
using System.Collections.Generic;


namespace Osaru.Serialization.Serializers
{
    public class RawSerializer : ClassSerializerBase<Byte[]>
    {
        public override void Setup(TypeRegistory r)
        {
        }

        public override void NonNullSerialize(Byte[] t, IFormatter f)
        {
            f.Bytes(t, t.Length);
        }
    }

    public class GenericRawSerializer<T> : ClassSerializerBase<T>
        where T: class, IList<Byte>
    {
        public override void Setup(TypeRegistory r)
        {
        }

        public override void NonNullSerialize(T t, IFormatter f)
        {
            f.Bytes(t, t.Count);
        }
    }
}
