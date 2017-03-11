using System;

namespace ObjectStructure.Serialization.Deserializers
{
    public interface IMemberDeserializer<T>
    {
        String MemberName { get; }
        void Deserialize<PARSER>(PARSER parser, ref T memberOwner)
            where PARSER : IParser<PARSER>
            ;
        void DeserializeBoxed<PARSER>(PARSER parser, object boxed)
            where PARSER : IParser<PARSER>
            ;
    }
}
