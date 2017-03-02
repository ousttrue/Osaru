using System;


namespace JsonSan.Serializers
{
    public class LambdaSerializer<T> : SerializerBase<T>
    {
        Action<T, IWriteStream<char>> m_serializer;
        public LambdaSerializer(Action<T, IWriteStream<char>> serializer)
        {
            m_serializer = serializer;
        }
        public override void Serialize(T t, TypeRegistory _, IWriteStream<char> w)
        {
            m_serializer(t, w);
        }
    }
}
