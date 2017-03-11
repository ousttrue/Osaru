namespace ObjectStructure.Serialization.Deserializers
{
    public class MemberDeserializer<T, U> : IMemberDeserializer<T>
    {
        public string MemberName
        {
            get;
            private set;
        }

        IDeserializerBase<U> m_deserializer;

        public delegate void Setter(ref T memberOwner, U value);
        Setter m_setter;
        public delegate void BoxedSetter(object boxedOwner, U value);
        BoxedSetter m_boxedSetter;
        public MemberDeserializer(string name
            , IDeserializerBase<U> deserializer
            , Setter setter
            , BoxedSetter boxedSetter)
        {
            MemberName = name;
            m_deserializer = deserializer;
            m_setter = setter;
            m_boxedSetter = boxedSetter;
        }

        public void Deserialize<PARSER>(PARSER parser, ref T memberOwner)
            where PARSER : IParser<PARSER>
        {
            var value = default(U);
            m_deserializer.Deserialize(parser, ref value);
            m_setter(ref memberOwner, value);
        }

        public void DeserializeBoxed<PARSER>(PARSER parser, object boxed) where PARSER : IParser<PARSER>
        {
            var value = default(U);
            m_deserializer.Deserialize(parser, ref value);
            m_boxedSetter(boxed, value);
        }
    }
}
