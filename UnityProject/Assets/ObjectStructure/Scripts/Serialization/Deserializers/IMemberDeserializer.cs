using System;

namespace ObjectStructure.Serialization.Deserializers
{
    public interface IMemberDeserializer<T>
    {
        String MemberName { get; }
        void Deserialize<PARSER>(PARSER json, ref T memberOwner)
            where PARSER : IParser<PARSER>
            ;
    }
}
