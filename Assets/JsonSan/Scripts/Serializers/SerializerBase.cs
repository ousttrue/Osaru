using System;
using System.Text;

namespace JsonSan.Serializers
{
    public interface ISerializer
    {
        void Setup(TypeRegistory r);
        void Serialize(object o, TypeRegistory r, IWriteStream<char> w);
        string Serialize(object o, TypeRegistory r);
    }
    public abstract class SerializerBase<T> : ISerializer
    {
        public virtual void Setup(TypeRegistory r)
        {
        }
        public void Serialize(object o, TypeRegistory r, IWriteStream<char> w)
        {
            Serialize((T)o, r, w);
        }
        public abstract void Serialize(T t, TypeRegistory r, IWriteStream<char> w);
        public string Serialize(object o, TypeRegistory r)
        {
            var sb = new StringBuilder();
            var w = new StringBuilderStream(sb);
            Serialize(o, r, w);
            return sb.ToString();
        }
    }
}
