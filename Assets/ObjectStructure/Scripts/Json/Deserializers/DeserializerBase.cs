using System;

namespace ObjectStructure.Json.Deserializers
{
    public interface IDeserializer
    {
        void Setup(ITypeRegistory r);
        object Deserialize<PARSER>(PARSER json, ITypeRegistory r)
            where PARSER : IParser;
    }
    public abstract class DeserializerBase<T> : IDeserializer
    {
        public virtual void Setup(ITypeRegistory r)
        {
            // default, do nothing
        }

        public object Deserialize<PARSER>(PARSER json, ITypeRegistory r)
            where PARSER : IParser
        {
            var value = default(T);
            Deserialize(json, ref value, r);
            return value;
        }

        public abstract void Deserialize<PARSER>(PARSER json, ref T outValue, ITypeRegistory r)
            where PARSER : IParser;

        /*
        public T Deserialize(PARSER json, ITypeRegistory r)
        {
            var value = default(T);
            Deserialize(json, ref value, r);
            return value;
        }
        */
    }
}
