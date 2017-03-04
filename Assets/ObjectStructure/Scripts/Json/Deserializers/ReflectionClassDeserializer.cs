using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace ObjectStructure.Json.Deserializers
{
    public class ReflectionClassDeserializer<T> : DeserializerBase<T>
        where T: class
    {
        delegate void DeserializeFunc(IParser json, ref T outValue, ITypeRegistory r);
        Dictionary<string, DeserializeFunc> m_deserializers=new Dictionary<string, DeserializeFunc>();

        static DeserializeFunc CreateFunc<U>(ITypeRegistory r, Setter<U> setter)
        {
            var deserializer = r.GetDeserializer<U>();

            return new DeserializeFunc(
            (IParser json, ref T outValue, ITypeRegistory rr) =>
            {
                var value = default(U);
                deserializer.Deserialize(json, ref value, rr);
                setter(outValue, value); 
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

        public override void Setup(ITypeRegistory r)
        {
            var genericMethod = GetType().GetMethod("CreateFunc", BindingFlags.Static|BindingFlags.NonPublic);
            var genericFieldSetter = GetType().GetMethod("CreateFieldSetter", BindingFlags.Static | BindingFlags.NonPublic);
            var fieldDeserializers = typeof(T).GetFields(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.FieldType, typeof(SerializableAttribute)))
                .Select(x =>
                {
                    var method = genericMethod.MakeGenericMethod(x.FieldType);
                    var setter = genericFieldSetter.MakeGenericMethod(x.FieldType).Invoke(null, new object[] { x });
                    return new
                    {
                        Name = x.Name,
                        Deserializer = (DeserializeFunc)method.Invoke(null, new object[] { r, setter })
                    };
                });
            var genericPropertySetter = GetType().GetMethod("CreatePropertySetter", BindingFlags.Static | BindingFlags.NonPublic);
            var propertyDeserializers = typeof(T).GetProperties(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.PropertyType, typeof(SerializableAttribute)))
                .Where(x => x.CanRead && x.CanWrite && x.GetIndexParameters().Length == 0)
                .Select(x =>
                {
                    var method = genericMethod.MakeGenericMethod(x.PropertyType);
                    var setter = genericPropertySetter.MakeGenericMethod(x.PropertyType).Invoke(null, new object[] { x });
                    return new
                    {
                        Name = x.Name,
                        Deserializer = (DeserializeFunc)method.Invoke(null, new object[] { r, setter })
                    };
                });

            m_deserializers = fieldDeserializers.Concat(propertyDeserializers)
                .ToDictionary(x => String.Intern(x.Name), x => x.Deserializer)
                ;
        }

        public override void Deserialize<PARSER>(PARSER json, ref T outValue, ITypeRegistory r)
        {
            if (outValue == null)
            {
                outValue=Activator.CreateInstance<T>();
            }

            foreach(var kv in json.ObjectItems)
            {
                DeserializeFunc d;
                if(m_deserializers.TryGetValue(kv.Key, out d))
                {
                    d(kv.Value, ref outValue, r);
                }
            }
        }
    }
}
