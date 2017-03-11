using System.Reflection;


namespace ObjectStructure.Serialization.Deserializers
{
    public class MemberDeserializer<T, U> : IMemberDeserializer<T>
    {
        public string MemberName
        {
            get;
            private set;
        }

        IDeserializerBase<U> m_deserializer;

        public delegate void Setter(ref T memberOwner, ref U value);
        Setter m_setter;

        public MemberDeserializer(string name
            , IDeserializerBase<U> deserializer
            , Setter setter)
        {
            MemberName = name;
            m_deserializer = deserializer;
            m_setter = setter;
        }

        public void Deserialize<PARSER>(PARSER json, ref T memberOwner)
            where PARSER : IParser<PARSER>
        {
            var value = default(U);
            m_deserializer.Deserialize(json, ref value);
            m_setter(ref memberOwner, ref value);
        }
    }

    public static class FieldInfoExtensions
    {
        static MemberDeserializer<T, U> CreateFromFieldInfo<T, U>(FieldInfo fi
            , TypeRegistory r)
        {
            return new MemberDeserializer<T, U>(fi.Name
                , r.GetDeserializer<U>()
                , (ref T t, ref U u) => fi.SetValue(t, u)
                );
        }
        public static IMemberDeserializer<T> CreateMemberDeserializer<T>(
            this FieldInfo fi, TypeRegistory r)
        {
            var genericMethod = typeof(FieldInfoExtensions).GetMethod("CreateFromFieldInfo"
                , BindingFlags.Static | BindingFlags.NonPublic);
            var method = genericMethod.MakeGenericMethod(typeof(T), fi.FieldType);
            return (IMemberDeserializer<T>)method.Invoke(null, new object[] { fi, r });
        }
    }
    public static class PropertyInfoExtensions
    {
        static MemberDeserializer<T, U> CreateFromPropertyInfo<T, U>(PropertyInfo pi
            , TypeRegistory r)
        {
            return new MemberDeserializer<T, U>(pi.Name
                , r.GetDeserializer<U>()
                , (ref T t, ref U u) => pi.SetValue(t, u, null)
                );
        }
        public static IMemberDeserializer<T> CreateMemberDeserializer<T>(
            this PropertyInfo pi, TypeRegistory r)
        {
            var genericMethod = typeof(PropertyInfoExtensions).GetMethod("CreateFromPropertyInfo"
                , BindingFlags.Static | BindingFlags.NonPublic);
            var method = genericMethod.MakeGenericMethod(typeof(T), pi.PropertyType);
            return (IMemberDeserializer<T>)method.Invoke(null, new object[] { pi, r });
        }
    }
}
