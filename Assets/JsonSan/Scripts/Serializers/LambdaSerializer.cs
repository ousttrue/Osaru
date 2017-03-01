using System;


namespace JsonSan.Serializers
{
    public class LambdaSerializer<T> : SerializerBase<T>
    {
        Func<T, string> m_serializer;
        public LambdaSerializer(Func<T, string> serializer)
        {
            m_serializer = serializer;
        }
        public override string Serialize(T t, TypeRegistory _)
        {
            return m_serializer(t);
        }
    }
}
