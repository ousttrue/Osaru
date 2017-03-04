using System;

namespace ObjectStructure
{
    public interface ITypeRegistory
    {
        Serialization.Serializers.ISerializer<T> GetSerializer<T>();
        Serialization.ITypeInitializer GetSerializer(Type type);
        Json.Deserializers.IDeserializer<Json.JsonParser, T> GetDeserializer<T>();
        Serialization.ITypeInitializer GetDeserializer(Type type);
    }
}
