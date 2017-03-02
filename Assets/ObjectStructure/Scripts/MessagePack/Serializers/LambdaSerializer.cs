using System;


namespace NMessagePack.Serializers
{
    public class LambdaSerializer<T> : SerializerBase<T>
    {
        public delegate void SerializeFunc(MsgPackWriter w, T t);

        SerializeFunc m_callback;

        public LambdaSerializer(Action<MsgPackWriter, T> callback)
        {
            m_callback = new SerializeFunc(callback);
        }

        protected override void NonNullSerialize(MsgPackWriter w, T t)
        {
            m_callback(w, t);
        }
    }
}
