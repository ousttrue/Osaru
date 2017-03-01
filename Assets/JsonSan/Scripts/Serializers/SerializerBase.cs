using System;


namespace JsonSan.Serializers
{
    public interface ISerializer
    {
        void Setup(TypeRegistory r);
        string Serialize(object o, TypeRegistory r);
    }
    public abstract class SerializerBase<T> : ISerializer
    {
        public virtual void Setup(TypeRegistory r)
        {
        }
        public string Serialize(object o, TypeRegistory r)
        {
            return Serialize((T)o, r);
        }
        public abstract string Serialize(T t, TypeRegistory r);
    }
}
