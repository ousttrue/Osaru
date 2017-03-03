using System;


namespace ObjectStructure.Json.Serializers
{
    public class EnumStringSerializer<T> : SerializerBase<T>
    {
        SerializerBase<string> m_stringSerializer;

        public override void Setup(JsonSerializeTypeRegistory r)
        {
            m_stringSerializer = (SerializerBase<string>)r.GetSerializer<String>();
        }

        public override void Serialize(T t, IWriteStream w, JsonSerializeTypeRegistory r)
        {
            m_stringSerializer.Serialize(t.ToString(), w, r);
        }
    }
}
