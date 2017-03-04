using System;
using System.Text;


namespace ObjectStructure.Json.Serializers
{
    public interface ISerializer
    {
        void Setup(ITypeRegistory r);
        void Serialize(object o, IWriteStream w, ITypeRegistory r);
        string Serialize(object o, ITypeRegistory r);
    }
    public abstract class SerializerBase<T> : ISerializer
    {
        #region ISerializer
        public virtual void Setup(ITypeRegistory r)
        {
        }
        public void Serialize(object o, IWriteStream w, ITypeRegistory r)
        {
            Serialize((T)o, w, r);
        }
        public string Serialize(object o, ITypeRegistory r)
        {
            var sb = new StringBuilder();
            var w = new StringBuilderStream(sb);
            Serialize(o, w, r);
            return sb.ToString();
        }
        #endregion
        public abstract void Serialize(T t, IWriteStream w, ITypeRegistory r);
    }
}
