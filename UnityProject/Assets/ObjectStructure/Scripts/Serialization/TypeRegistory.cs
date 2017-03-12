using System;
using System.Linq;
using System.Collections.Generic;
using ObjectStructure.Serialization.Serializers;
using ObjectStructure.Serialization.Deserializers;
using System.Reflection;

namespace ObjectStructure.Serialization
{
    public delegate void SerializeFunc<T, F>(T t, F f)
        where F : IFormatter
        ;

    public struct TypeSerialization
    {
        public Type Type;
        public ISerializer Serializer;
        public IDeserializer Deserializer;

        public static TypeSerialization Create<T>(
            SerializerBase<T> serializer
            , IDeserializerBase<T> deserializer)
        {
            return new TypeSerialization
            {
                Type=typeof(T),
                Serializer=serializer,
                Deserializer=deserializer
            };
        }

        public static TypeSerialization Create<T>(
            Action<T, IFormatter> serializeFunc
            , IDeserializerBase<T> deserializer)
        {
            return new TypeSerialization
            {
                Type = typeof(T),
                Serializer = new LambdaSerializer<T>(serializeFunc),
                Deserializer = deserializer
            };
        }
    }

    /// <summary>
    /// not thread safe. use thread local
    /// </summary>
    public class TypeRegistory
    {
        public TypeRegistory()
        {
            foreach(var s in EmbededTypeSerializations.Serializations)
            {
                AddSerialization(s);
            }
#if UNITY_EDITOR || UNITY_WSA || UNITY_STANDALONE
            foreach(var s in UnityTypeSerializations.Serializations)
            {
                AddSerialization(s);
            }
#endif
            m_serializerMap.Add(typeof(Object), new BoxingSerializer(this));
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

        #region Serialize
        Dictionary<Type, ISerializer> m_serializerMap
            = new Dictionary<Type, ISerializer>();
        public SerializerBase<T> GetSerializer<T>(bool orCreate = true)
        {
            var t = typeof(T);
            var serializer = GetSerializer(t, orCreate);
            return (SerializerBase<T>)serializer;
        }

        public ISerializer GetSerializer(Type t, bool orCreate = true)
        {
            ISerializer serializer;
            if (m_serializerMap.TryGetValue(t, out serializer))
            {
                return serializer;
            }
            if (!orCreate) return null;

            serializer = CreateSerializer(t);
            if (serializer != null)
            {
                serializer.Setup(this);
                m_serializerMap.Add(t, serializer);
            }

            return serializer;
        }

        ISerializer CreateSerializer(Type t)
        {
            if (t.IsEnum())
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

            if (!t.IsClass())
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
        List<IDeserializerFactory> m_deserializerFactories
            = new List<IDeserializerFactory>()
            {
                new DefaultDeserializerFactory(),
            };

        Dictionary<Type, IDeserializer> m_deserializerMap
            = new Dictionary<Type, IDeserializer>();
        public IDeserializerBase<T> GetDeserializer<T>(bool orCreate = true)
        {
            return (IDeserializerBase<T>)GetDeserializer(typeof(T), orCreate);
        }

        public IDeserializer GetDeserializer(Type t, bool orCreate = true)
        {
            IDeserializer deserializer;
            if (m_deserializerMap.TryGetValue(t, out deserializer))
            {
                return deserializer;
            }
            if (!orCreate) return null;

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
            if (t == typeof(object))
            {
                throw new ArgumentException("no deserializer for object");
            }

            foreach(var f in m_deserializerFactories)
            {
                var d = f.Create(t);
                if (d != null)
                {
                    return d;
                }
            }
            return null;
        }
        #endregion

        #region MemberDeserializer
        public IEnumerable<IMemberDeserializer<T>> GetMemberDeserializers<T>()
        {
            foreach (var fi in
            typeof(T).GetFields(System.Reflection.BindingFlags.Public
                            | System.Reflection.BindingFlags.Instance))
            {
                if (m_deserializerMap.ContainsKey(fi.FieldType)
                    || fi.FieldType.IsSerializable())
                {
                    yield return fi.CreateMemberDeserializer<T>(this);
                }
            }
            foreach (var pi in
            typeof(T).GetProperties(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance))
            {
                if (pi.CanRead && pi.CanWrite && pi.GetIndexParameters().Length == 0
                    &&
                    (pi.PropertyType.IsSerializable()
                    || pi.PropertyType.IsSerializable()))
                {
                    yield return pi.CreateMemberDeserializer<T>(this);
                }
            }
        }
        #endregion

        public void AddSerialization(TypeSerialization serialization)
        {
            m_serializerMap.Add(serialization.Type, serialization.Serializer);
            m_deserializerMap.Add(serialization.Type, serialization.Deserializer);

            serialization.Serializer.Setup(this);
            serialization.Deserializer.Setup(this);
        }

        public void AddDeserializerFactory(IDeserializerFactory f)
        {
            m_deserializerFactories.Insert(0, f);
        }
    }
}
