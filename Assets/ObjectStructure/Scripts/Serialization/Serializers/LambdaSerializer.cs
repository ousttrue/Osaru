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

        public override void Setup(TypeRegistory r)
        {
        }

        public override void Serialize(T t, IFormatter f)
        {
            m_serializer(t, f);
        }
    }
}
