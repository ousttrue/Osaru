using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace ObjectStructure.Serialization.Serializers
{
    public class ClassReflectionSerializer<T> : ClassSerializerBase<T>
        where T: class
    {
        protected delegate void SerializeFunc(T value, IFormatter f);
        SerializeFunc[] m_serializers;

        public override void Setup(TypeRegistory r)
        {
            var genericFieldMethod = GetType().GetMethod("CreateFieldSerializer"
                , BindingFlags.Static | BindingFlags.NonPublic|BindingFlags.FlattenHierarchy);
            var genericPropertyMethod = GetType().GetMethod("CreatePropertySerializer"
                , BindingFlags.Static | BindingFlags.NonPublic|BindingFlags.FlattenHierarchy);

            /*
            foreach(var n in GetType().GetMethods())
            {
                int a = 0;
            }

            var genericFieldMethod = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy).FirstOrDefault(x => x.Name=="CreateFieldSerializer");
            var genericPropertyMethod = GetType().GetMethods().FirstOrDefault(x => x.Name=="CreatePropertySerializer");
            */

            m_serializers =
                FieldsSerializers(genericFieldMethod, r)
                .Concat(PropertiesSerializers(genericPropertyMethod, r))
                .ToArray()
                ;
        }

        /*
                .Where(pi =>
                        {
            var nsAttrs = pi.GetCustomAttributes(typeof(NonSerializedAttribute), false);
            return nsAttrs.Length == 0;
        })
        */

        #region Field
        static IEnumerable<SerializeFunc> FieldsSerializers(
            MethodInfo genericMethod
            , TypeRegistory r)
        {
            return typeof(T).GetFields(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => x.FieldType.IsSerializable())
                .Select(x =>
                {
                    var method = genericMethod.MakeGenericMethod(x.FieldType);
                    return (SerializeFunc)method.Invoke(null, new object[] { r, x });
                })
                ;
        }

        protected static SerializeFunc CreateFieldSerializer<U>(
            TypeRegistory r
            , FieldInfo x)
        {
            var serializer = (SerializerBase<U>)r.GetSerializer<U>();
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
            , TypeRegistory r)
        {
            return typeof(T).GetProperties(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance)
                .Where(x => x.CanRead && x.CanWrite && x.GetIndexParameters().Length==0)
                .Where(x => x.PropertyType.IsSerializable())
                .Select(x =>
                {
                    var method = genericMethod.MakeGenericMethod(x.PropertyType);
                    return (SerializeFunc)method.Invoke(null, new object[] { r, x });
                })
                ;
        }

        protected static SerializeFunc CreatePropertySerializer<U>(
            TypeRegistory r
            , PropertyInfo x)
        {
            var serializer = (SerializerBase<U>)r.GetSerializer<U>();
            return new SerializeFunc((value, f) =>
            {
                f.Key(x.Name);
                serializer.Serialize((U)x.GetValue(value, null), f);
            });
        }
        #endregion

        public override void NonNullSerialize(T t, IFormatter f)
        {
            f.BeginMap(m_serializers.Count());
            foreach (var serializer in m_serializers)
            {
                serializer(t, f);
            }
            f.EndMap();
        }
    }
}
