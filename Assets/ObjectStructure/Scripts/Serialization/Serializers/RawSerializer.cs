using System;
using System.Collections.Generic;


namespace ObjectStructure.Serialization.Serializers
{
    public class RawSerializer<T> : ISerializer<T>
        where T: IList<Byte>
    {
        public override void Setup(TypeRegistory r)
        {
        }

        public override void Serialize(T t, IFormatter f)
        {
            f.Raw(t);
        }
    }
}
