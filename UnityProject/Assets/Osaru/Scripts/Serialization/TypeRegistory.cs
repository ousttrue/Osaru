using System;
using System.Linq;
using System.Collections.Generic;
using Osaru.Serialization.Serializers;
using Osaru.Serialization.Deserializers;
using System.Reflection;

namespace Osaru.Serialization
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
            foreach(var s in UnityTypeSerializations.Serializations)
            {
                AddSerialization(s);
            }
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
        List<ISerializerFactory> m_serializerFactories
            = new List<ISerializerFactory>()
            {
                new DefaultSerializerFactory(),
            };

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
            foreach (var f in m_serializerFactories)
            {
                var s = f.Create(t);
                if (s != null)
                {
                    return s;
                }
            }
            return null;
        }
        #endregion

        #region MembserSerializer
        public IEnumerable<IMemberSerializer<T>> GetMemberSerializers<T>()
        {
            foreach (var fi in typeof(T).GetFields(BindingFlags.Public
                | BindingFlags.Instance))
            {
                if (m_serializerMap.ContainsKey(fi.FieldType)
                    || fi.FieldType.IsSerializable())
                {
                    yield return fi.CreateMemberSerializer<T>(this);
                }
            }
            foreach (var pi in typeof(T).GetProperties(BindingFlags.Public
                | BindingFlags.Instance))
            {
                if ((pi.CanRead && pi.CanWrite && pi.GetIndexParameters().Length == 0)
                    &&
                    (m_serializerMap.ContainsKey(pi.PropertyType)
                    || pi.PropertyType.IsSerializable()))
                {
                    yield return pi.CreateMemberSerializer<T>(this);
                }
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
            typeof(T).GetFields(BindingFlags.Public
                            | BindingFlags.Instance))
            {
                if (m_deserializerMap.ContainsKey(fi.FieldType)
                    || fi.FieldType.IsSerializable())
                {
                    yield return fi.CreateMemberDeserializer<T>(this);
                }
            }
            foreach (var pi in
            typeof(T).GetProperties(BindingFlags.Public
                | BindingFlags.Instance))
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
