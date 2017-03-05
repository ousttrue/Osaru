using ObjectStructure.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace ObjectStructure.Serialization.Deserializers
{
    public class StructReflectionDeserializer<T> : IDeserializer<T>
        where T: struct
    {
        class DeserializerCaller
        {
            delegate void BoxingDeserializeFunc<PARSER>(PARSER json, object outValue);
            static BoxingDeserializeFunc<PARSER> CreateFunc<PARSER, U>(IDeserializer<U> deserializer, Setter<U> setter)
                where PARSER: IParser<PARSER>
            {
                return new BoxingDeserializeFunc<PARSER>(
                (PARSER json, object boxedValue) =>
                {
                    var value = default(U);
                    deserializer.Deserialize(json, ref value);
                    //x.SetValueDirect(__makeref(outValue), value); Unity not implemented
                    setter(boxedValue, value);
                });
            }

            delegate void Setter<U>(object outValue, U value);
            static Setter<U> CreateFieldSetter<U>(FieldInfo fi)
            {
                return (o, v) => fi.SetValue(o, v);
            }
            static Setter<U> CreatePropertySetter<U>(PropertyInfo pi)
            {
                return (o, v) => pi.SetValue(o, v, null);
            }

            Type m_type;
            ITypeInitializer m_deserializer;
            object m_setter;
            public DeserializerCaller(FieldInfo fi, ITypeInitializer deserializer)
            {
                m_type = fi.FieldType;
                m_deserializer = deserializer;
                var genericFieldSetter = GetType().GetMethod("CreateFieldSetter", BindingFlags.Static | BindingFlags.NonPublic);
                m_setter = genericFieldSetter.MakeGenericMethod(m_type).Invoke(null, new object[] { fi });
            }
            public DeserializerCaller(PropertyInfo pi, ITypeInitializer deserializer)
            {
                m_type = pi.PropertyType;
                m_deserializer = deserializer;
                var genericPropertySetter = GetType().GetMethod("CreatePropertySetter", BindingFlags.Static | BindingFlags.NonPublic);
                //var method = genericMethod.MakeGenericMethod(x.PropertyType);
                m_setter = genericPropertySetter.MakeGenericMethod(m_type).Invoke(null, new object[] { pi });
            }

            object m_deserializerFunc;
            public void Deserializer<PARSER>(PARSER parser, object outValue)
            {
                if (m_deserializerFunc == null)
                {
                    var genericMethod = GetType().GetMethod("CreateFunc", BindingFlags.Static | BindingFlags.NonPublic);
                    var method = genericMethod.MakeGenericMethod(typeof(PARSER), m_type);
                    m_deserializerFunc = method.Invoke(null, new object[] { m_deserializer, m_setter });
                }
                var deserializer = (BoxingDeserializeFunc<PARSER>)m_deserializerFunc;
                deserializer(parser, outValue);
            }
        }
        Dictionary<string, DeserializerCaller> m_deserializers=new Dictionary<string, DeserializerCaller>();


        public void Setup(TypeRegistory r)
        {
            var fieldDeserializers = typeof(T).GetFields(BindingFlags.Public
                | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.FieldType, typeof(SerializableAttribute)))
                .Select(x =>
                {
                    return new
                    {
                        Name = x.Name,
                        Deserializer = new DeserializerCaller(x, r.GetDeserializer(x.FieldType))
                    };
                });

            var propertyDeserializers = typeof(T).GetProperties(BindingFlags.Public
                | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.PropertyType, typeof(SerializableAttribute)))
                .Where(x => x.CanRead && x.CanWrite && x.GetIndexParameters().Length == 0)
                .Select(x =>
                {
                    return new
                    {
                        Name = x.Name,
                        Deserializer = new DeserializerCaller(x, r.GetDeserializer(x.PropertyType))
                    };
                });

            m_deserializers = 
                fieldDeserializers
                .Concat(propertyDeserializers)
                .ToDictionary(x => String.Intern(x.Name), x => x.Deserializer);
        }

        public void Deserialize<PARSER>(PARSER json, ref T outValue)
            where PARSER: IParser<PARSER>
        {
            var boxed = (object)outValue;
            foreach(var kv in json.ObjectItems)
            {
                DeserializerCaller d;
                if(m_deserializers.TryGetValue(kv.Key, out d))
                {
                    d.Deserializer(kv.Value, boxed);
                }
            }
            // unboxing
            outValue = (T)boxed;
        }
    }
}
