using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ObjectStructure.Json.Serializers
{
    public class ReflectionSerializer<T> : SerializerBase<T>
    {
        delegate void SerializeFunc(T value, IWriteStream w, TypeRegistory r);
        SerializeFunc[] m_serializers;

        struct KeySerializer
        {
            ISerializer m_serializer;
            StringBuilder m_sb;
            IWriteStream m_writer;

            public KeySerializer(ISerializer serializer)
            {
                m_serializer = serializer;
                m_sb = new StringBuilder();
                m_writer = new StringBuilderStream(m_sb);
            }

            public string Serialize(string key)
            {
                m_writer.Clear();
                m_serializer.Serialize(key, m_writer, null);
                m_writer.Write(":");
                return m_sb.ToString();
            }
        }

        public override void Setup(TypeRegistory r)
        {
            var keyWriter = new KeySerializer(r.GetSerializer(typeof(String)));

            m_serializers =
                FieldsSerializers(r, keyWriter)
                .Concat(PropertiesSerializers(r, keyWriter))
                .ToArray()
                ;
        }

        static IEnumerable<SerializeFunc> FieldsSerializers(
            TypeRegistory r, KeySerializer keySerializer)
        {
            return typeof(T).GetFields(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.FieldType, typeof(SerializableAttribute)))
                .Select(x =>
                {
                    return CreateFieldSerializer(r, keySerializer, x);
                })
                ;
        }

        static SerializeFunc CreateFieldSerializer(
            TypeRegistory r
            , KeySerializer keySerializer
            , FieldInfo x)
        {
            var key = keySerializer.Serialize(x.Name);
            var serializer = r.GetSerializer(x.FieldType);
            return new SerializeFunc((value, w, rr) =>
            {
                w.Write(key);
                serializer.Serialize(x.GetValue(value), w, rr);
            });
        }

        static IEnumerable<SerializeFunc> PropertiesSerializers(
            TypeRegistory r, KeySerializer keySerializer)
        {
            return typeof(T).GetProperties(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => x.CanRead && x.CanWrite && x.GetIndexParameters().Length==0)
                .Where(x => Attribute.IsDefined(x.PropertyType, typeof(SerializableAttribute)))
                .Select(x =>
                {
                    return CreatePropertySerializer(r, keySerializer, x);
                })
                ;
        }

        static SerializeFunc CreatePropertySerializer(
            TypeRegistory r
            , KeySerializer keySerializer
            , PropertyInfo x)
        {
            var key = keySerializer.Serialize(x.Name);
            var serializer = r.GetSerializer(x.PropertyType);
            return new SerializeFunc((value, w, rr) =>
            {
                w.Write(key);
                serializer.Serialize(x.GetValue(value, null), w, rr);
            });
        }

        public override void Serialize(T t, IWriteStream w, TypeRegistory r)
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
                serializer(t, w, r);
            }
            w.Write('}');
        }
    }
}
