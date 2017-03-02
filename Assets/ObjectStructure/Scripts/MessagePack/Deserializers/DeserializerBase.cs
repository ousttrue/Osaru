namespace NMessagePack.Deserializers
{
    public abstract class DeserializerBase<T> : IDeserializer
    {
        public object DeserializeObject(MsgPackValue value)
        {
            return Deserialize(value);
        }
        public abstract T Deserialize(MsgPackValue value);
    }
}
