using System.Collections.Generic;


namespace ObjectStructure.Serialization.Serializers
{
    public abstract class ClassSerializerBase<T> : SerializerBase<T>
        where T: class
    {
        public override void Serialize(T t, IFormatter f)
        {
            if (t == null)
            {
                f.Null();
                return;
            }

            NonNullSerialize(t, f);
        }

        public abstract void NonNullSerialize(T t, IFormatter f);
    }
}
