using System;
using System.Text;


namespace ObjectStructure.Json.Serializers
{
    public interface ISerializer
    {
        void Setup(TypeRegistory r);
        void Serialize(object o, IWriteStream w, TypeRegistory r);
        string Serialize(object o, TypeRegistory r);
    }
    public abstract class SerializerBase<T> : ISerializer
    {
        #region ISerializer
        public virtual void Setup(TypeRegistory r)
        {
        }
        public void Serialize(object o, IWriteStream w, TypeRegistory r)
        {
            Serialize((T)o, w, r);
        }
        public string Serialize(object o, TypeRegistory r)
        {
            var sb = new StringBuilder();
            var w = new StringBuilderStream(sb);
            Serialize(o, w, r);
            return sb.ToString();
        }
        #endregion
        public abstract void Serialize(T t, IWriteStream w, TypeRegistory r);
    }
}
