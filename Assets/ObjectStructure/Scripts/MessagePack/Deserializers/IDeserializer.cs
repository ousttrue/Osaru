namespace ObjectStructure.MessagePack.Deserializers
{
    public interface IDeserializer
    {
        object DeserializeObject(MsgPackValue value);
    }
}
