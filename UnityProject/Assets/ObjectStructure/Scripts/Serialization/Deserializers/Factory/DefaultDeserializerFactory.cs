using System;
using System.Collections.Generic;
using System.Linq;


namespace ObjectStructure.Serialization.Deserializers
{
    public class DefaultDeserializerFactory : IDeserializerFactory
    {
        public IDeserializer Create(Type t)
        {
            if (t.IsEnum())
            {
                // enum
                var constructedType = typeof(EnumStringDeserializer<>).MakeGenericType(t);
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsArray && t.GetElementType() != null)
            {
                // T[]
                var constructedType = typeof(TypedArrayDeserializer<>).MakeGenericType(t.GetElementType());
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.GetInterfaces().Any(x =>
            x.IsGenericType() &&
            x.GetGenericTypeDefinition() == typeof(IList<>)))
            {
                // IList<T>
                var constructedType = typeof(GenericListDeserializer<,>).MakeGenericType(t.GetGenericArguments().First(), t);
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsInterface() && t.GetGenericTypeDefinition() == typeof(IList<>))
            {
                // IList<T>
                var constructedType = typeof(GenericListDeserializer<,>).MakeGenericType(t.GetGenericArguments().First(), t);
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsClass())
            {
                // class
                var constructedType = typeof(ClassReflectionDeserializer<>).MakeGenericType(t);
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else
            {
                // struct
                Type constructedType = typeof(StructReflectionDeserializer<>).MakeGenericType(t);
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
        }
    }
}
