using System;
using System.Linq;
using System.Text;


namespace JsonSan.Serializers
{
    public class ReflectionSerializer<T> : SerializerBase<T>
    {
        delegate void SerializeFunc(T value, TypeRegistory r, IWriteStream<char> w);
        SerializeFunc[] m_serializers;
        public override void Setup(TypeRegistory r)
        {
            var keySerializer = r.GetSerializer(typeof(String));
            var sb = new StringBuilder();
            var keyWriter = new StringBuilderStream(sb);

            m_serializers = typeof(T).GetFields(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.FieldType, typeof(SerializableAttribute)))
                .Select(x =>
                {
                    keyWriter.Clear();
                    keySerializer.Serialize(x.Name, r, keyWriter);
                    keyWriter.Write(":");
                    var key = sb.ToString();

                    var serializer = r.GetSerializer(x.FieldType);
                    return new SerializeFunc((value, rr, w) =>
                    {
                        w.Write(key);
                        serializer.Serialize(x.GetValue(value), rr, w);
                    });
                })
                .ToArray()
                ;
        }

        public override void Serialize(T t, TypeRegistory r, IWriteStream<char> w)
        {
            w.Write('{');
            bool isFirst = true;
            foreach (var serializer in m_serializers)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    w.Write(',');
                }
                serializer(t, r, w);
            }
            w.Write('}');
        }
    }
}
