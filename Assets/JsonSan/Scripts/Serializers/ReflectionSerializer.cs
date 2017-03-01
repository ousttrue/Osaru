using System;
using System.Linq;
using System.Text;


namespace JsonSan.Serializers
{
    public class ReflectionSerializer<T> : SerializerBase<T>
    {
        delegate string SerializeFunc(T value, TypeRegistory r);
        SerializeFunc[] m_serializers;
        public override void Setup(TypeRegistory r)
        {
            var keySerializer = r.GetSerializer(typeof(String));

            m_serializers = typeof(T).GetFields(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.FieldType, typeof(SerializableAttribute)))
                .Select(x =>
                {
                    var key = keySerializer.Serialize(x.Name, r) + ":";
                    var serializer = r.GetSerializer(x.FieldType);
                    return new SerializeFunc((value, rr) =>
                    {
                        return key + serializer.Serialize(x.GetValue(value), rr)
                        ;
                    });
                })
                .ToArray()
                ;
        }

        public override string Serialize(T t, TypeRegistory r)
        {
            var sb = new StringBuilder();
            sb.Append('{');
            bool isFirst = true;
            foreach (var serializer in m_serializers)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sb.Append(',');
                }
                sb.Append(serializer(t, r));
            }
            sb.Append('}');
            return sb.ToString();
        }
    }
}
