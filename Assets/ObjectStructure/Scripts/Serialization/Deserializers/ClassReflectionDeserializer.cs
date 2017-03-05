using ObjectStructure.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace ObjectStructure.Serialization.Deserializers
{
    public class ClassReflectionDeserializer<T> : IDeserializer<T>
        where T : class
    {
        class DeserializeCaller
        {
            delegate void Setter<U>(object outValue, U value);
            static Setter<U> CreateFieldSetter<U>(FieldInfo fi)
            {
                return (o, v) => fi.SetValue(o, v);
            }
            static Setter<U> CreatePropertySetter<U>(PropertyInfo pi)
            {
                return (o, v) => pi.SetValue(o, v, null);
            }

            delegate void DeserializeFunc<PARSER>(PARSER json, ref T outValue);
            static DeserializeFunc<PARSER> CreateFunc<PARSER, U>(IDeserializer<U> deserializer, Setter<U> setter)
                where PARSER : IParser<PARSER>
            {
                return new DeserializeFunc<PARSER>(
                (PARSER json, ref T outValue) =>
                {
                    var value = default(U);
                    deserializer.Deserialize(json, ref value);
                    setter(outValue, value);
                });
            }

            object m_setter;
            Type m_type;
            ITypeInitializer m_deserializer;
            public DeserializeCaller(FieldInfo fi, ITypeInitializer deserializer)
            {
                m_type = fi.FieldType;
                m_deserializer = deserializer;
                var genericFieldSetter = GetType().GetMethod("CreateFieldSetter", BindingFlags.Static | BindingFlags.NonPublic);
                m_setter = genericFieldSetter.MakeGenericMethod(m_type).Invoke(null, new object[] { fi });
            }
            public DeserializeCaller(PropertyInfo pi, ITypeInitializer deserializer)
            {
                m_type = pi.PropertyType;
                m_deserializer = deserializer;
                var genericPropertySetter = GetType().GetMethod("CreatePropertySetter", BindingFlags.Static | BindingFlags.NonPublic);
                m_setter = genericPropertySetter.MakeGenericMethod(m_type).Invoke(null, new object[] { pi });
            }

            object m_deserializerFunc;
            public void Deserialize<PARSER>(PARSER parser, ref T parentValue)
            {
                if (m_deserializerFunc == null)
                {
                    var genericMethod = GetType().GetMethod("CreateFunc", BindingFlags.Static | BindingFlags.NonPublic);
                    var method = genericMethod.MakeGenericMethod(typeof(PARSER), m_type);
                    m_deserializerFunc = method.Invoke(null, new object[] { m_deserializer, m_setter });
                }
                var func = (DeserializeFunc<PARSER>)m_deserializerFunc;
                func(parser, ref parentValue);
            }
        }
        Dictionary<string, DeserializeCaller> m_deserializers = new Dictionary<string, DeserializeCaller>();

        public void Setup(TypeRegistory r)
        {
            var fieldDeserializers = typeof(T).GetFields(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.FieldType, typeof(SerializableAttribute)))
                .Select(x =>
                {
                    return new
                    {
                        Name = x.Name,
                        Deserializer = new DeserializeCaller(x, r.GetDeserializer(x.FieldType))
                    };
                });
            var propertyDeserializers = typeof(T).GetProperties(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.PropertyType, typeof(SerializableAttribute)))
                .Where(x => x.CanRead && x.CanWrite && x.GetIndexParameters().Length == 0)
                .Select(x =>
                {
                    return new
                    {
                        Name = x.Name,
                        Deserializer = new DeserializeCaller(x, r.GetDeserializer(x.PropertyType))
                    };
                });

            m_deserializers = fieldDeserializers.Concat(propertyDeserializers)
                .ToDictionary(x => String.Intern(x.Name), x => x.Deserializer)
                ;
        }

        public void Deserialize<PARSER>(PARSER json, ref T outValue)
            where PARSER: IParser<PARSER>
        {
            if (json.IsNull)
            {
                outValue = null;
                return;
            }

            if (outValue == null)
            {
                outValue=Activator.CreateInstance<T>();
            }

            foreach(var kv in json.ObjectItems)
            {
                DeserializeCaller d;
                if(m_deserializers.TryGetValue(kv.Key, out d))
                {
                    d.Deserialize(kv.Value, ref outValue);
                }
            }
        }
    }
}
