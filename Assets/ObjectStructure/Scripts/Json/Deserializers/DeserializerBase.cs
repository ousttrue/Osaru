namespace ObjectStructure.Json.Deserializers
{
    public interface IDeserializer
    {
        void Setup(JsonSerializeTypeRegistory r);
    }
    public abstract class DeserializerBase<T> : IDeserializer
    {
        public virtual void Setup(JsonSerializeTypeRegistory r)
        {
            // default, do nothing
        }

        public abstract void Deserialize(JsonParser json, ref T outValue, JsonSerializeTypeRegistory r);

        public T Deserialize(JsonParser json, JsonSerializeTypeRegistory r)
        {
            var value = default(T);
            Deserialize(json, ref value, r);
            return value;
        }
    }
}
