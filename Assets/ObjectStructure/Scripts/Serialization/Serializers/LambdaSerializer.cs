using System;


namespace ObjectStructure.Serialization.Serializers
{
    public class LambdaSerializer<T> : ISerializer<T>
    {
        Action<T, IFormatter> m_serializer;
        public LambdaSerializer(Action<T, IFormatter> serializer)
        {
            m_serializer = serializer;
        }

        public void Setup(ITypeRegistory r)
        {
        }

        public void Serialize(T t, IFormatter f)
        {
            m_serializer(t, f);
        }
    }
}
