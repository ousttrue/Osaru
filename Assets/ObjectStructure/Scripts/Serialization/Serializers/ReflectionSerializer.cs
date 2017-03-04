using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ObjectStructure.Serialization.Serializers
{
    public class ReflectionSerializer<T> : ISerializer<T>
    {
        delegate void SerializeFunc(T value, IWriteStream w);
        SerializeFunc[] m_serializers;

        struct KeySerializer
        {
            ISerializer<String> m_serializer;
            StringBuilder m_sb;
            IWriteStream m_writer;

            public KeySerializer(ISerializer<String> serializer)
            {
                m_serializer = serializer;
                m_sb = new StringBuilder();
                m_writer = new StringBuilderStream(m_sb);
            }

            public string Serialize(string key)
            {
                m_writer.Clear();
                m_serializer.Serialize(key, m_writer);
                m_writer.Write(":");
                return m_sb.ToString();
            }
        }

        public void Setup(ITypeRegistory r)
        {
            var keyWriter = new KeySerializer((ISerializer<string>)r.GetSerializer(typeof(String)));
            var genericFieldMethod = GetType().GetMethod("CreateFieldSerializer", BindingFlags.Static | BindingFlags.NonPublic);
            var genericPropertyMethod = GetType().GetMethod("CreatePropertySerializer", BindingFlags.Static | BindingFlags.NonPublic);

            m_serializers =
                FieldsSerializers(genericFieldMethod, r, keyWriter)
                .Concat(PropertiesSerializers(genericPropertyMethod, r, keyWriter))
                .ToArray()
                ;
        }

        #region Field
        IEnumerable<SerializeFunc> FieldsSerializers(
            MethodInfo genericMethod
            , ITypeRegistory r, KeySerializer keySerializer)
        {
            return typeof(T).GetFields(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.FieldType, typeof(SerializableAttribute)))
                .Select(x =>
                {
                    var method = genericMethod.MakeGenericMethod(x.FieldType);
                    return (SerializeFunc)method.Invoke(null, new object[] { r, keySerializer, x });
                })
                ;
        }

        static SerializeFunc CreateFieldSerializer<U>(
            ITypeRegistory r
            , KeySerializer keySerializer
            , FieldInfo x)
        {
            var key = keySerializer.Serialize(x.Name);
            var serializer = (ISerializer<U>)r.GetSerializer<U>();
            return new SerializeFunc((value, w) =>
            {
                w.Write(key);
                serializer.Serialize(x.GetValue(value), w);
            });
        }
        #endregion

        #region Property
        static IEnumerable<SerializeFunc> PropertiesSerializers(
            MethodInfo genericMethod
            , ITypeRegistory r, KeySerializer keySerializer)
        {

            return typeof(T).GetProperties(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => x.CanRead && x.CanWrite && x.GetIndexParameters().Length==0)
                .Where(x => Attribute.IsDefined(x.PropertyType, typeof(SerializableAttribute)))
                .Select(x =>
                {
                    var method = genericMethod.MakeGenericMethod(x.PropertyType);
                    return (SerializeFunc)method.Invoke(null, new object[] { r, keySerializer, x });
                })
                ;
        }

        static SerializeFunc CreatePropertySerializer<U>(
            ITypeRegistory r
            , KeySerializer keySerializer
            , PropertyInfo x)
        {
            var key = keySerializer.Serialize(x.Name);
            var serializer = (ISerializer<U>)r.GetSerializer<U>();
            return new SerializeFunc((value, w) =>
            {
                w.Write(key);
                serializer.Serialize(x.GetValue(value, null), w);
            });
        }
        #endregion

        public void Serialize(T t, IWriteStream w)
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
                serializer(t, w);
            }
            w.Write('}');
        }
    }
}
