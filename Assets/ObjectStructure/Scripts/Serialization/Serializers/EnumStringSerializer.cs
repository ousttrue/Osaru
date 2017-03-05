using System;


namespace ObjectStructure.Serialization.Serializers
{
    public class EnumStringSerializer<T> : SerializerBase<T>
    {
        SerializerBase<string> m_stringSerializer;

        public override void Setup(TypeRegistory r)
        {
            m_stringSerializer = (SerializerBase<string>)r.GetSerializer<String>();
        }

        public override void Serialize(T t, IFormatter f)
        {
            m_stringSerializer.Serialize(t.ToString(), f);
        }
    }
}
