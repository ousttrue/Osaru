using System;

namespace ObjectStructure.Json.Deserializers
{
    public interface IDeserializer
    {
        void Setup(TypeRegistory r);
        object Deserialize<PARSER>(PARSER json, TypeRegistory r)
            where PARSER : IParser;
    }
    public abstract class DeserializerBase<T> : IDeserializer
    {
        public virtual void Setup(TypeRegistory r)
        {
            // default, do nothing
        }

        public object Deserialize<PARSER>(PARSER json, TypeRegistory r)
            where PARSER : IParser
        {
            var value = default(T);
            Deserialize(json, ref value, r);
            return value;
        }

        public abstract void Deserialize<PARSER>(PARSER json, ref T outValue, TypeRegistory r)
            where PARSER : IParser;

        /*
        public T Deserialize(PARSER json, TypeRegistory r)
        {
            var value = default(T);
            Deserialize(json, ref value, r);
            return value;
        }
        */
    }
}
