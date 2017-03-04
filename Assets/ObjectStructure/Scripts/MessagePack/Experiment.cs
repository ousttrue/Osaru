using ObjectStructure.MessagePack.Deserializers;
using ObjectStructure.MessagePack.Serializers;
using System;


namespace ObjectStructure.MessagePack
{
    public static class Experiment
    {
        public static ISerializer GetSerializer(Type t)
        {
            if (t.IsClass)
            {
                // class
                var constructedType = typeof(ClassReflectionSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else
            {
                // struct
                var constructedType = typeof(StructReflectionSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
        }

        public static IDeserializer GetDeserializer(Type t)
        {
            if (t.IsClass)
            {
                // class
                var constructedType = typeof(ClassReflectionDeserializer<>).MakeGenericType(t);
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else
            {
                // struct
                var constructedType = typeof(StructReflectionDeserializer<>).MakeGenericType(t);
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
        }

        public static void Register()
        {
            Serializer.ExtendedSerializers.Add(GetSerializer);
            Deserializer.ExtendedDeserializers.Add(GetDeserializer);
        }
    }
}
