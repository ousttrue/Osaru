namespace ObjectStructure.MessagePack.Deserializers
{
    public abstract class IDeserializer<T> : IDeserializer
    {
        public object DeserializeObject(MsgPackValue value)
        {
            return Deserialize(value);
        }
        public abstract T Deserialize(MsgPackValue value);
    }
}
