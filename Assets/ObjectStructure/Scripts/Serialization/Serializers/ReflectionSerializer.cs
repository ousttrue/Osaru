using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ObjectStructure.Serialization.Serializers
{
    public class ReflectionSerializer<T> : ISerializer<T>
    {
        delegate void SerializeFunc(T value, IFormatter f);
        SerializeFunc[] m_serializers;

        public void Setup(ITypeRegistory r)
        {
            var genericFieldMethod = GetType().GetMethod("CreateFieldSerializer", BindingFlags.Static | BindingFlags.NonPublic);
            var genericPropertyMethod = GetType().GetMethod("CreatePropertySerializer", BindingFlags.Static | BindingFlags.NonPublic);

            m_serializers =
                FieldsSerializers(genericFieldMethod, r)
                .Concat(PropertiesSerializers(genericPropertyMethod, r))
                .ToArray()
                ;
        }

        #region Field
        IEnumerable<SerializeFunc> FieldsSerializers(
            MethodInfo genericMethod
            , ITypeRegistory r)
        {
            return typeof(T).GetFields(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x.FieldType, typeof(SerializableAttribute)))
                .Select(x =>
                {
                    var method = genericMethod.MakeGenericMethod(x.FieldType);
                    return (SerializeFunc)method.Invoke(null, new object[] { r, x });
                })
                ;
        }

        static SerializeFunc CreateFieldSerializer<U>(
            ITypeRegistory r
            , FieldInfo x)
        {
            var serializer = (ISerializer<U>)r.GetSerializer<U>();
            return new SerializeFunc((value, f) =>
            {
                f.Key(x.Name);
                serializer.Serialize((U)x.GetValue(value), f);
            });
        }
        #endregion

        #region Property
        static IEnumerable<SerializeFunc> PropertiesSerializers(
            MethodInfo genericMethod
            , ITypeRegistory r)
        {

            return typeof(T).GetProperties(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => x.CanRead && x.CanWrite && x.GetIndexParameters().Length==0)
                .Where(x => Attribute.IsDefined(x.PropertyType, typeof(SerializableAttribute)))
                .Select(x =>
                {
                    var method = genericMethod.MakeGenericMethod(x.PropertyType);
                    return (SerializeFunc)method.Invoke(null, new object[] { r, x });
                })
                ;
        }

        static SerializeFunc CreatePropertySerializer<U>(
            ITypeRegistory r
            , PropertyInfo x)
        {
            var serializer = (ISerializer<U>)r.GetSerializer<U>();
            return new SerializeFunc((value, f) =>
            {
                f.Key(x.Name);
                serializer.Serialize((U)x.GetValue(value, null), f);
            });
        }
        #endregion

        public void Serialize(T t, IFormatter f)
        {
            f.OpenMap(m_serializers.Count());
            foreach (var serializer in m_serializers)
            {
                serializer(t, f);
            }
            f.CloseMap();
        }
    }
}
