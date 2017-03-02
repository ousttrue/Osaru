using System;


namespace NMessagePack.Deserializers
{
    public class EnumDeserializer<T, U> : DeserializerBase<T>
    {
        public override T Deserialize(MsgPackValue value)
        {
            return (T)(Object)value.GetValue<U>();
        }
    }
}
