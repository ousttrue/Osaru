
namespace NMessagePack.Serializers
{
    public abstract class StringKeySerializer<T> : SerializerBase<T>
    {
        protected SerializerBase<string> m_keySerializer;
        protected delegate void Writer(MsgPackWriter w, T t);
        protected Writer[] m_writers;

        protected StringKeySerializer()
        {
            m_keySerializer = Serializer.GetSerializer<string>();
        }
    }
}
