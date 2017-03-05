using System;
using System.Linq;
using System.Collections.Generic;
using ObjectStructure.Serialization.Serializers;
using ObjectStructure.Serialization.Deserializers;


namespace ObjectStructure.Serialization
{
    /// <summary>
    /// not thread safe. use thread local
    /// </summary>
    public class TypeRegistory
    {
        public TypeRegistory()
        {
            m_serializerMap.Add(typeof(Object), new BoxingSerializer(this));
        }

        #region Serialize
        Dictionary<Type, ISerializer> m_serializerMap = new Dictionary<Type, ISerializer>()
        {
            {typeof(Boolean), new LambdaSerializer<Boolean>((x, f)=> f.Value(x)) },
            {typeof(SByte), new LambdaSerializer<SByte>((x, f)=> f.Value(x)) },
            {typeof(Int16), new LambdaSerializer<Int16>((x, f)=> f.Value(x)) },
            {typeof(Int32), new LambdaSerializer<Int32>((x, f)=> f.Value(x)) },
            {typeof(Int64), new LambdaSerializer<Int64>((x, f)=> f.Value(x)) },
            {typeof(Byte), new LambdaSerializer<Byte>((x, f)=>  f.Value(x)) },
            {typeof(UInt16), new LambdaSerializer<UInt16>((x, f)=>  f.Value(x)) },
            {typeof(UInt32), new LambdaSerializer<UInt32>((x, f)=>  f.Value(x)) },
            {typeof(UInt64), new LambdaSerializer<UInt64>((x, f)=>  f.Value(x)) },
            {typeof(Single), new LambdaSerializer<Single>((x, f)=>  f.Value(x)) },
            {typeof(Double), new LambdaSerializer<Double>((x, f)=>  f.Value(x)) },
            {typeof(String), new LambdaSerializer<String>((x, f)=> f.Value(x)) },
            {typeof(Byte[]), new RawSerializer() },
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

        public SerializerBase<T> GetSerializer<T>()
        {
            var t = typeof(T);
            var serializer= GetSerializer(t);
            return (SerializerBase<T>)serializer;
        }

        ISerializer CreateSerializer(Type t)
        {
            if (t.IsEnum)
            {
                // enum
                Type constructedType = typeof(EnumStringSerializer<>).MakeGenericType(t);
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
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.GetInterfaces().Any(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(IList<>)))
            {
                // where U: IList<T>
                Type constructedType = typeof(GenericListSerializer<,>).MakeGenericType(t.GetGenericArguments().First(), t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if(t.IsInterface && t.GetGenericTypeDefinition() == typeof(IList<>))
            {
                // IList<T>
                Type constructedType = typeof(GenericListSerializer<,>).MakeGenericType(t.GetGenericArguments().First(), t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if(!t.IsClass)
            {
                // object
                Type constructedType = typeof(StructReflectionSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else
            {
                // with nullcheck
                Type constructedType = typeof(ClassReflectionSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
        }
        #endregion

        #region Deserialize
        Dictionary<Type, IDeserializer> m_deserializerMap = new Dictionary<Type, IDeserializer>
        {
            {typeof(Boolean), new BooleanDeserializer() },
            {typeof(SByte), new SByteDeserializer() },
            {typeof(Int16), new Int16Deserializer() },
            {typeof(Int32), new Int32Deserializer() },
            {typeof(Int64), new Int64Deserializer() },
            {typeof(Byte),  new ByteDeserializer() },
            {typeof(UInt16), new UInt16Deserializer() },
            {typeof(UInt32), new UInt32Deserializer() },
            {typeof(UInt64), new UInt64Deserializer() },
            {typeof(Single), new SingleDeserializer() },
            {typeof(Double), new DoubleDeserializer() },
            {typeof(String), new StringDeserializer() },
            {typeof(Byte[]), new RawDeserializer() },
        };

        public IDeserializerBase<T> GetDeserializer<T>()
        {
            return (IDeserializerBase<T>)GetDeserializer(typeof(T));
        }

        public IDeserializer GetDeserializer(Type t)
        {
            IDeserializer deserializer;
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

        IDeserializer CreateDeserializer(Type t)
        {
            if (t.IsEnum)
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
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(IList<>)))
            {
                // IList<T>
                var constructedType = typeof(GenericListDeserializer<,>).MakeGenericType(t.GetGenericArguments().First(), t);
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsInterface && t.GetGenericTypeDefinition() == typeof(IList<>))
            {
                // IList<T>
                var constructedType = typeof(GenericListDeserializer<,>).MakeGenericType(t.GetGenericArguments().First(), t);
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsClass)
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
        #endregion

        public void AddType<T>(SerializerBase<T> serializer, IDeserializerBase<T> deserializer)
        {
            m_serializerMap.Add(typeof(T), serializer);
            m_deserializerMap.Add(typeof(T), deserializer);
        }

        public void SerializeBoxing(object o, IFormatter f)
        {
            if (o == null)
            {
                f.Null();
                return;
            }

            var s = GetSerializer(o.GetType());
            s.Setup(this);
            s.SerializeBoxing(o, f);
        }
    }
}
