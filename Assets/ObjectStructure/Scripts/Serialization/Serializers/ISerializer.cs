using System;

namespace ObjectStructure.Serialization.Serializers
{
    public interface ISerializerBase : ITypeInitializer
    {
        void SerializeBoxing(object o, IFormatter f);
    }
    public abstract class ISerializer<T> : ISerializerBase
    {
        public void SerializeBoxing(object o, IFormatter f)
        {
            Serialize((T)o, f);
        }

        public abstract void Setup(TypeRegistory r);

        public abstract void Serialize(T t, IFormatter f);
    }
}
