using System;


namespace Osaru.Serialization.Serializers
{
    public interface ISerializerFactory
    {
        ISerializer Create(Type t);
    }
}
