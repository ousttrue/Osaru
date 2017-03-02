using System;


namespace NMessagePack.Deserializers
{
    public class LambdaDeserilaizer<T> : DeserializerBase<T>
    {
        delegate T DeserializeFunc(MsgPackValue value);

        DeserializeFunc m_callback;

        public LambdaDeserilaizer(Func<MsgPackValue, T> callback)
        {
            m_callback = new DeserializeFunc(callback);
        }

        public override T Deserialize(MsgPackValue value)
        {
            return m_callback(value);
        }
    }
}
