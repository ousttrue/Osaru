using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace ObjectStructure.Json.Deserializers
{
    public class ReflectionClassDeserializer<T> : DeserializerBase<T>
        where T: class
    {
        delegate void DeserializeFunc(JsonParser json, ref T outValue, TypeRegistory r);
        Dictionary<string, DeserializeFunc> m_deserializers=new Dictionary<string, DeserializeFunc>();

        static DeserializeFunc CreateFunc<U>(TypeRegistory r, FieldInfo x)
        {
            var deserializer = r.GetDeserializer<U>();

            return new DeserializeFunc(
            (JsonParser json, ref T outValue, TypeRegistory rr) =>
            {
                var value = default(U);
                deserializer.Deserialize(json, ref value, rr);
                x.SetValue(outValue, value); 
            });
        }

        public override void Setup(TypeRegistory r)
        {
            var genericMethod = GetType().GetMethod("CreateFunc", BindingFlags.Static|BindingFlags.NonPublic);

            foreach(var x in typeof(T).GetFields(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.FieldType, typeof(SerializableAttribute))))
            {
                var method = genericMethod.MakeGenericMethod(x.FieldType);
                m_deserializers.Add(x.Name, (DeserializeFunc)method.Invoke(null, new object[] { r, x }));
            }
        }

        public override void Deserialize(JsonParser json, ref T outValue, TypeRegistory r)
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
