using System;


namespace ObjectStructure.Json.Serializers
{
    public class LambdaSerializer<T> : SerializerBase<T>
    {
        Action<T, IWriteStream, ITypeRegistory> m_serializer;
        public LambdaSerializer(Action<T, IWriteStream, ITypeRegistory> serializer)
        {
            m_serializer = serializer;
        }
        public override void Serialize(T t, IWriteStream w, ITypeRegistory r)
        {
            m_serializer(t, w, r);
        }
    }
}
