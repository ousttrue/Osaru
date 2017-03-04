using System;

namespace ObjectStructure
{
    public interface ITypeRegistory
    {
        Json.Serializers.ISerializer GetSerializer<T>();
        Json.Serializers.ISerializer GetSerializer(Type type);
        Json.Deserializers.DeserializerBase<T> GetDeserializer<T>();
        Json.Deserializers.IDeserializer GetDeserializer(Type type);
    }
}
