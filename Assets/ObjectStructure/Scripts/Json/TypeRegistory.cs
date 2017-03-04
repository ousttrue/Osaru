using System;
using System.Linq;
using System.Collections.Generic;
using ObjectStructure.Json.Deserializers;
using ObjectStructure.Serialization;
using ObjectStructure.Serialization.Serializers;


namespace ObjectStructure.Json
{
    /// <summary>
    /// not thread safe
    /// </summary>
    public class TypeRegistory: ITypeRegistory
    {
        #region Serialize
        Dictionary<Type, ITypeInitializer> m_serializerMap = new Dictionary<Type, ITypeInitializer>()
        {
            {typeof(Boolean), new LambdaSerializer<Boolean>((x, w)=> w.Write(x ? "true" : "false")) },
            {typeof(SByte), new LambdaSerializer<SByte>((x, w)=> w.Write(x.ToString())) },
            {typeof(Int16), new LambdaSerializer<Int16>((x, w)=> w.Write(x.ToString())) },
            {typeof(Int32), new LambdaSerializer<Int32>((x, w)=> w.Write(x.ToString())) },
            {typeof(Int64), new LambdaSerializer<Int64>((x, w)=> w.Write(x.ToString())) },
            {typeof(Byte), new LambdaSerializer<Byte>((x, w)=> w.Write(x.ToString())) },
            {typeof(UInt16), new LambdaSerializer<UInt16>((x, w)=> w.Write(x.ToString())) },
            {typeof(UInt32), new LambdaSerializer<UInt32>((x, w)=> w.Write(x.ToString())) },
            {typeof(UInt64), new LambdaSerializer<UInt64>((x, w)=> w.Write(x.ToString())) },
            {typeof(Single), new LambdaSerializer<Single>((x, w)=> w.Write(x.ToString())) },
            {typeof(Double), new LambdaSerializer<Double>((x, w)=> w.Write(x.ToString())) },
            {typeof(string), new LambdaSerializer<String>((x, w)=> w.Write(JsonString.Quote(x))) },
        };

        public ITypeInitializer GetSerializer(Type t)
        {
            ITypeInitializer serializer;
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

        public ISerializer<T> GetSerializer<T>()
        {
            var t = typeof(T);
            var serializer= GetSerializer(t);
            return (ISerializer<T>)serializer;
        }

        ITypeInitializer CreateSerializer(Type t)
        {
            if (t.IsEnum)
            {
                // enum
                Type constructedType = typeof(EnumStringSerializer<>).MakeGenericType(t);
                return (ITypeInitializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsArray && t.GetElementType() != null)
            {
                // T[]
                Type constructedType = typeof(TypedArraySerializer<>).MakeGenericType(t.GetElementType());
                return (ITypeInitializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.GetInterfaces().Any(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(IList<>)))
            {
                // where U: IList<T>
                Type constructedType = typeof(GenericListSerializer<,>).MakeGenericType(t.GetGenericArguments().First(), t);
                return (ITypeInitializer)Activator.CreateInstance(constructedType, null);
            }
            else if(t.IsInterface && t.GetGenericTypeDefinition() == typeof(IList<>))
            {
                // IList<T>
                Type constructedType = typeof(GenericListSerializer<,>).MakeGenericType(t.GetGenericArguments().First(), t);
                return (ITypeInitializer)Activator.CreateInstance(constructedType, null);
            }
            else
            {
                // object
                Type constructedType = typeof(ReflectionSerializer<>).MakeGenericType(t);
                return (ITypeInitializer)Activator.CreateInstance(constructedType, null);
            }
        }
        #endregion

        #region Deserialize
        Dictionary<Type, ITypeInitializer> m_deserializerMap = new Dictionary<Type, ITypeInitializer>
        {
            {typeof(SByte), new SByteDeserializer() },
            {typeof(Int16), new Int16Deserializer() },
            {typeof(Int32), new Int32Deserializer() },
            {typeof(Int64), new Int64Deserializer() },
            {typeof(Byte),  new ByteDeserializer() },
            {typeof(UInt16), new Int16Deserializer() },
            {typeof(UInt32), new Int32Deserializer() },
            {typeof(UInt64), new Int64Deserializer() },
            {typeof(Single), new SingleDeserializer() },
            {typeof(Double), new DoubleDeserializer() },
            {typeof(String), new StringDeserializer() },
        };

        public IDeserializer<T> GetDeserializer<T>()
        {
            return (IDeserializer<T>)GetDeserializer(typeof(T));
        }

        public ITypeInitializer GetDeserializer(Type t)
        {
            ITypeInitializer deserializer;
            if (m_deserializerMap.TryGetValue(t, out deserializer))
            {
                return deserializer;
            }

            deserializer = CreateDeserializer(t);
            if (deserializer != null)
            {
                deserializer.Setup(this);
                m_deserializerMap.Add(t, deserializer);
            }
            return deserializer;
        }

        ITypeInitializer CreateDeserializer(Type t)
        {
            if (t.IsEnum)
            {
                // enum
                var constructedType = typeof(EnumStringDeserializer<>).MakeGenericType(t);
                return (ITypeInitializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsArray && t.GetElementType() != null)
            {
                // T[]
                var constructedType = typeof(TypedArrayDeserializer<>).MakeGenericType(t.GetElementType());
                return (ITypeInitializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.GetInterfaces().Any(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(IList<>)))
            {
                // IList<T>
                var constructedType = typeof(GenericListDeserializer<,>).MakeGenericType(t.GetGenericArguments().First(), t);
                return (ITypeInitializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsInterface && t.GetGenericTypeDefinition() == typeof(IList<>))
            {
                // IList<T>
                var constructedType = typeof(GenericListDeserializer<,>).MakeGenericType(t.GetGenericArguments().First(), t);
                return (ITypeInitializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsClass)
            {
                // class
                var constructedType = typeof(ReflectionClassDeserializer<>).MakeGenericType(t);
                return (ITypeInitializer)Activator.CreateInstance(constructedType, null);
            }
            else
            {
                // struct
                Type constructedType = typeof(ReflectionStructDeserializer<>).MakeGenericType(t);
                return (ITypeInitializer)Activator.CreateInstance(constructedType, null);
            }
        }
        #endregion

        public void AddType<T>(ISerializer<T> serializer, IDeserializer<T> deserializer)
        {
            m_serializerMap.Add(typeof(T), serializer);
            m_deserializerMap.Add(typeof(T), deserializer);
        }
    }
}
