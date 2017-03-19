using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Osaru.Serialization.Serializers
{
    public class DefaultSerializerFactory : ISerializerFactory
    {
        public ISerializer Create(Type t)
        {
            if (t.IsEnum())
            {
                // enum
                Type constructedType = typeof(EnumStringSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.GetInterfaces().Any(x =>
            x.IsGenericType()
            && x.GetGenericTypeDefinition() == typeof(IList<>)
            && x.GetGenericArguments().SequenceEqual(new Type[] { typeof(Byte)})))
            {
                // IList<Byte>
                var constructedType = typeof(GenericRawSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            /*
            else if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            {
                // IDictionary<T>
                Type constructedType = typeof(GenericMapSerializer<,>).MakeGenericType(t.GetGenericArguments());
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if (typeof(IDictionary).IsAssignableFrom(t))
            {
                // dictionary
                Type constructedType = typeof(MapSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            */
            else if (t.IsArray && t.GetElementType() != null)
            {
                // T[]
                Type constructedType = typeof(TypedArraySerializer<>).MakeGenericType(t.GetElementType());
                return (ISerializer)Activator.CreateInstance(constructedType);
            }
            else if (t.GetInterfaces().Any(x =>
            x.IsGenericType() &&
            x.GetGenericTypeDefinition() == typeof(IList<>)))
            {
                // where U: IList<T>
                Type constructedType = typeof(GenericListSerializer<,>).MakeGenericType(t.GetGenericArguments().First(), t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsInterface() && t.GetGenericTypeDefinition() == typeof(IList<>))
            {
                // IList<T>
                Type constructedType = typeof(GenericListSerializer<,>).MakeGenericType(t.GetGenericArguments().First(), t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }

            // search custom serializer
            {
                var mi = t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault(
                    x => x.GetCustomAttributes(true).Any(y => y is SerializerAttribute));
                if (mi != null)
                {
                    // Lambda
                    Type constructedType = typeof(LambdaSerializer<>).MakeGenericType(t);
                    var create = constructedType.GetMethod("Create", BindingFlags.Static | BindingFlags.Public);
                    return (ISerializer)create.Invoke(null, new object[] { mi });
                }
            }

            //if (!t.IsClass())
            {
                // object
                Type constructedType = typeof(ReflectionSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            /*
            else
            {
                // with nullcheck
                Type constructedType = typeof(ClassReflectionSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            */
        }
    }
}
