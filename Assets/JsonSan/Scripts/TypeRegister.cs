using JsonSan.Deserializers;
using JsonSan.Serializers;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace JsonSan
{
    /// <summary>
    /// not thread safe
    /// </summary>
    public class TypeRegistory
    {
        #region Serialize
        Dictionary<Type, ISerializer> m_serializerMap = new Dictionary<Type, ISerializer>()
        {
            {typeof(int), new LambdaSerializer<int>(x=> x.ToString()) },
        };

        public ISerializer GetSerializer(Type t)
        {
            ISerializer serializer;
            if (m_serializerMap.TryGetValue(t, out serializer))
            {
                return serializer;
            }

            serializer = CreateSerializer(t);
            if (serializer != null)
            {
                serializer.Setup(this);
                m_serializerMap.Add(t, serializer);
            }

            return serializer;
        }

        public ISerializer GetSerializer<T>()
        {
            var t = typeof(T);
            var serializer= GetSerializer(t);
            return serializer;
        }

        ISerializer CreateSerializer(Type t)
        {
            if (t.IsArray && t.GetElementType() != null)
            {
                // T[]
                Type constructedType = typeof(TypedArraySerializer<>).MakeGenericType(t.GetElementType());
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.GetInterfaces().Any(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(IList<>)))
            {
                // IList<T>
                Type constructedType = typeof(GenericListSerializer<>).MakeGenericType(t.GetGenericArguments());
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if (typeof(ICollection).IsAssignableFrom(t))
            {
                // Array
                Type constructedType = typeof(CollectionSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Deserialize
        Dictionary<Type, IDeserializer> m_deserializerMap = new Dictionary<Type, IDeserializer>
        {
            {typeof(int), new LambdaDeserializer<int>((Node json, ref int outValue, TypeRegistory r) => outValue=(int)json.GetNumber() )},
        };

        public DeserializerBase<T> GetDeserializer<T>()
        {
            IDeserializer deserializer;
            var t = typeof(T);
            if (m_deserializerMap.TryGetValue(t, out deserializer))
            {
                return (DeserializerBase<T>)deserializer;
            }

            deserializer = CreateDeserializer(t);
            if (deserializer != null)
            {
                deserializer.Setup(this);
                m_deserializerMap.Add(t, deserializer);
            }
            return (DeserializerBase<T>)deserializer;
        }

        IDeserializer CreateDeserializer(Type t)
        {
            if (t.IsArray && t.GetElementType() != null)
            {
                // T[]
                Type constructedType = typeof(TypedArrayDeserializer<>).MakeGenericType(t.GetElementType());
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.GetInterfaces().Any(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(IList<>)))
            {
                // IList<T>
                Type constructedType = typeof(GenericListDeserializer<>).MakeGenericType(t.GetGenericArguments());
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        public void AddType<T>(SerializerBase<T> serializer, DeserializerBase<T> deserializer)
        {
            m_serializerMap.Add(typeof(T), serializer);
            m_deserializerMap.Add(typeof(T), deserializer);
        }
    }
}
