namespace NMessagePack.Deserializers
{
    public interface IDeserializer
    {
        object DeserializeObject(MsgPackValue value);
    }
}
