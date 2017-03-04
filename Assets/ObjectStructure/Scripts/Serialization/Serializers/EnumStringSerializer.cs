using System;


namespace ObjectStructure.Serialization.Serializers
{
    public class EnumStringSerializer<T> : ISerializer<T>
    {
        ISerializer<string> m_stringSerializer;

        public void Setup(ITypeRegistory r)
        {
            m_stringSerializer = (ISerializer<string>)r.GetSerializer<String>();
        }

        public void Serialize(T t, IFormatter f)
        {
            m_stringSerializer.Serialize(t.ToString(), f);
        }
    }
}
