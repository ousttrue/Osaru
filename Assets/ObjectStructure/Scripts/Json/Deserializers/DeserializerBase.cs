namespace ObjectStructure.Json.Deserializers
{
    public interface IDeserializer
    {
        void Setup(TypeRegistory r);
    }
    public abstract class DeserializerBase<T> : IDeserializer
    {
        public virtual void Setup(TypeRegistory r)
        {
            // default, do nothing
        }

        public abstract void Deserialize(JsonParser json, ref T outValue, TypeRegistory r);

        public T Deserialize(JsonParser json, TypeRegistory r)
        {
            var value = default(T);
            Deserialize(json, ref value, r);
            return value;
        }
    }
}
