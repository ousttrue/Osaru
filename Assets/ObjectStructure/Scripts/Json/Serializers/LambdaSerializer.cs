using System;


namespace ObjectStructure.Json.Serializers
{
    public class LambdaSerializer<T> : SerializerBase<T>
    {
        Action<T, IWriteStream, TypeRegistory> m_serializer;
        public LambdaSerializer(Action<T, IWriteStream, TypeRegistory> serializer)
        {
            m_serializer = serializer;
        }
        public override void Serialize(T t, IWriteStream w, TypeRegistory r)
        {
            m_serializer(t, w, r);
        }
    }
}
