﻿

namespace ObjectStructure.Serialization.Serializers
{
    public interface ISerializer : ITypeInitializer
    {
        void SerializeBoxing(object o, IFormatter f);
    }
    public abstract class SerializerBase<T> : ISerializer
    {
        public void SerializeBoxing(object o, IFormatter f)
        {
            Serialize((T)o, f);
        }

        public abstract void Setup(TypeRegistory r);

        public abstract void Serialize(T t, IFormatter f);
    }
}
