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

        public abstract void Deserialize(Node json, ref T outValue, TypeRegistory r);
    }
}
