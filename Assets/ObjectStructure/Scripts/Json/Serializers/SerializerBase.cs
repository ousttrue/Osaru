using System;
using System.Text;


namespace ObjectStructure.Json.Serializers
{
    public interface ISerializer
    {
        void Setup(JsonSerializeTypeRegistory r);
        void Serialize(object o, IWriteStream w, JsonSerializeTypeRegistory r);
        string Serialize(object o, JsonSerializeTypeRegistory r);
    }
    public abstract class SerializerBase<T> : ISerializer
    {
        #region ISerializer
        public virtual void Setup(JsonSerializeTypeRegistory r)
        {
        }
        public void Serialize(object o, IWriteStream w, JsonSerializeTypeRegistory r)
        {
            Serialize((T)o, w, r);
        }
        public string Serialize(object o, JsonSerializeTypeRegistory r)
        {
            var sb = new StringBuilder();
            var w = new StringBuilderStream(sb);
            Serialize(o, w, r);
            return sb.ToString();
        }
        #endregion
        public abstract void Serialize(T t, IWriteStream w, JsonSerializeTypeRegistory r);
    }
}
