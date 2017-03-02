using System;


namespace ObjectStructure.Json.Serializers
{
    public class LambdaSerializer<T> : SerializerBase<T>
    {
        Action<T, IWriteStream, JsonSerializeTypeRegistory> m_serializer;
        public LambdaSerializer(Action<T, IWriteStream, JsonSerializeTypeRegistory> serializer)
        {
            m_serializer = serializer;
        }
        public override void Serialize(T t, IWriteStream w, JsonSerializeTypeRegistory r)
        {
            m_serializer(t, w, r);
        }
    }
}
