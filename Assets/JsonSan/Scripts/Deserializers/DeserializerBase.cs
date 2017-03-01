

using System;

namespace JsonSan.Deserializers
{
    public interface IDeserializer
    {
        void Setup(TypeRegistory r);
    }
    public abstract class DeserializerBase<T> : IDeserializer
    {
        public virtual void Setup(TypeRegistory r)
        {
        }

        public abstract void Deserialize(Node json, ref T outValue, TypeRegistory r);
    }
}
