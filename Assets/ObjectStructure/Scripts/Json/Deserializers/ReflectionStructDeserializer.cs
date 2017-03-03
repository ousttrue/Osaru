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

        static BoxingDeserializeFunc CreateFunc<U>(TypeRegistory r, FieldInfo x)
        {
            return new BoxingDeserializeFunc(
            (JsonParser json, object boxedValue, TypeRegistory rr) =>
            {
                var deserializer = r.GetDeserializer<U>();
                var value = default(U);
                deserializer.Deserialize(json, ref value, rr);
                //x.SetValueDirect(__makeref(outValue), value); Unity not implemented
                x.SetValue(boxedValue, value); 
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
                m_deserializers.Add(x.Name, (BoxingDeserializeFunc)method.Invoke(null, new object[] { r, x }));
            }
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
