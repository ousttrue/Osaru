using System;


namespace ObjectStructure.Serialization.Serializers
{
    public class LambdaSerializer<T> : ISerializer<T>
    {
        Action<T, IWriteStream> m_serializer;
        public LambdaSerializer(Action<T, IWriteStream> serializer)
        {
            m_serializer = serializer;
        }

        public void Setup(ITypeRegistory r)
        {
        }

        public void Serialize(T t, IWriteStream w)
        {
            m_serializer(t, w);
        }
    }
}
