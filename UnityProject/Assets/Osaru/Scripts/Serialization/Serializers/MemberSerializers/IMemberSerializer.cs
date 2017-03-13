using System;


namespace Osaru.Serialization.Serializers
{
    public interface IMemberSerializer<T>
    {
        String MemberName { get; }
        void Serialize(ref T t, IFormatter f);
    }
}
