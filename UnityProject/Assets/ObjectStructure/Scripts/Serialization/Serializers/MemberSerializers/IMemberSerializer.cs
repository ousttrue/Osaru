using System;


namespace ObjectStructure.Serialization.Serializers
{
    public interface IMemberSerializer<T>
    {
        String MemberName { get; }
        void Serialize(ref T t, IFormatter f);
    }
}
