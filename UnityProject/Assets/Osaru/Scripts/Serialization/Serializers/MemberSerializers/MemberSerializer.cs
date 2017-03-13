using System;


namespace Osaru.Serialization.Serializers
{
    class MemberSerializer<T, U> : IMemberSerializer<T>
    {
        public delegate U Getter(ref T memberOwner);
        Getter m_getter;
        SerializerBase<U> m_serializer;

        public string MemberName
        {
            get;
            private set;
        }

        public MemberSerializer(String name, Getter getter, SerializerBase<U> serializer)
        {
            MemberName = name;
            m_getter = getter;
            m_serializer = serializer;
        }
        public void Serialize(ref T t, IFormatter f)
        {
            var u = m_getter(ref t);
            m_serializer.Serialize(u, f);
        }
    }
}
