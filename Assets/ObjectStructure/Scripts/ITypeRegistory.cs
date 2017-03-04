using System;

namespace ObjectStructure
{
    public interface ITypeRegistory
    {
        Json.Serializers.ISerializer<T> GetSerializer<T>();
        Serialization.ITypeInitializer GetSerializer(Type type);
        Json.Deserializers.DeserializerBase<T> GetDeserializer<T>();
        Json.Deserializers.IDeserializer GetDeserializer(Type type);
    }
}
