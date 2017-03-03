using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace ObjectStructure.Json.Deserializers
{
    public class ReflectionStructDeserializer<T> : DeserializerBase<T>
        where T: struct
    {
        delegate void BoxingDeserializeFunc(JsonParser json, object outValue, TypeRegistory r);
        Dictionary<string, BoxingDeserializeFunc> m_deserializers=new Dictionary<string, BoxingDeserializeFunc>();

        static BoxingDeserializeFunc CreateFunc<U>(TypeRegistory r, Setter<U> setter)
        {
            var deserializer = r.GetDeserializer<U>();

            return new BoxingDeserializeFunc(
            (JsonParser json, object boxedValue, TypeRegistory rr) =>
            {
                var value = default(U);
                deserializer.Deserialize(json, ref value, rr);
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

        public override void Setup(TypeRegistory r)
        {
            var genericMethod = GetType().GetMethod("CreateFunc", BindingFlags.Static|BindingFlags.NonPublic);
            var genericFieldSetter = GetType().GetMethod("CreateFieldSetter", BindingFlags.Static | BindingFlags.NonPublic);
            var fieldDeserializers = typeof(T).GetFields(BindingFlags.Public
                | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.FieldType, typeof(SerializableAttribute)))
                .Select(x =>
                {
                    var method = genericMethod.MakeGenericMethod(x.FieldType);
                    var setter = genericFieldSetter.MakeGenericMethod(x.FieldType).Invoke(null, new object[] { x });
                    return new
                    {
                        Name = x.Name,
                        Deserializer = (BoxingDeserializeFunc)method.Invoke(null, new object[] { r, setter })
                    };
                });

            var genericPropertySetter = GetType().GetMethod("CreatePropertySetter", BindingFlags.Static | BindingFlags.NonPublic);
            var propertyDeserializers = typeof(T).GetProperties(BindingFlags.Public
                | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.PropertyType, typeof(SerializableAttribute)))
                .Where(x => x.CanRead && x.CanWrite && x.GetIndexParameters().Length == 0)
                .Select(x =>
                {
                    var method = genericMethod.MakeGenericMethod(x.PropertyType);
                    var setter = genericPropertySetter.MakeGenericMethod(x.PropertyType).Invoke(null, new object[] { x });
                    return new
                    {
                        Name = x.Name,
                        Deserializer = (BoxingDeserializeFunc)method.Invoke(null, new object[] { r, x })
                    };
                });

            m_deserializers = 
                fieldDeserializers
                .Concat(propertyDeserializers)
                .ToDictionary(x => x.Name, x => x.Deserializer);
        }

        public override void Deserialize(JsonParser json, ref T outValue, TypeRegistory r)
        {
            var boxed = (object)outValue;
            foreach(var kv in json.ObjectItems)
            {
                BoxingDeserializeFunc d;
                if(m_deserializers.TryGetValue(kv.Key, out d))
                {
                    d(kv.Value, boxed, r);
                }
            }
            // unboxing
            outValue = (T)boxed;
        }
    }
}
