using System;


namespace ObjectStructure.Serialization.Serializers
{
    public class EnumStringSerializer<T> : ISerializer<T>
    {
        ISerializer<string> m_stringSerializer;

        public override void Setup(TypeRegistory r)
        {
            m_stringSerializer = (ISerializer<string>)r.GetSerializer<String>();
        }

        public override void Serialize(T t, IFormatter f)
        {
            m_stringSerializer.Serialize(t.ToString(), f);
        }
    }
}
