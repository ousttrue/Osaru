using System;
using System.Collections.Generic;


namespace ObjectStructure.Serialization.Serializers
{
    public class RawSerializer : ClassSerializerBase<Byte[]>
    {
        public override void Setup(TypeRegistory r)
        {
        }

        public override void NonNullSerialize(Byte[] t, IFormatter f)
        {
            f.Raw(t, t.Length);
        }
    }
}
