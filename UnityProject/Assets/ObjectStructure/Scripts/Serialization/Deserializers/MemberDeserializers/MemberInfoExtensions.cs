using System.Linq.Expressions;
using System.Reflection;


namespace ObjectStructure.Serialization.Deserializers
{
    public static class MemberInfoExtensions
    {
        #region Field
        static IMemberDeserializer<T> CreateFromFieldInfo<T, U>(FieldInfo fi
            , TypeRegistory r)
        {
#if true
            return new MemberDeserializer<T, U>(fi.Name
                , r.GetDeserializer<U>()
                , (ref T t, U u) => fi.SetValue(t, u)
                , (object o, U u) => fi.SetValue(o, u)
                );
#else
            return new FieldMemberDeserializer<T, U>(fi, r.GetDeserializer<U>());
#endif
        }
        public static IMemberDeserializer<T> CreateMemberDeserializer<T>(
            this FieldInfo fi, TypeRegistory r)
        {
            var genericMethod = typeof(MemberInfoExtensions).GetMethod("CreateFromFieldInfo"
                , BindingFlags.Static | BindingFlags.NonPublic);
            var method = genericMethod.MakeGenericMethod(typeof(T), fi.FieldType);
            return (IMemberDeserializer<T>)method.Invoke(null, new object[] { fi, r });
        }
#endregion

#region Property
        delegate void PropertySetter<T, U>(ref T t, U u);
        static PropertySetter<T, U> CreatePropertySetter<T, U>(PropertyInfo pi)
        {
            var param0 = Expression.Parameter(typeof(T).MakeByRefType(), "obj");
            var param1 = Expression.Parameter(typeof(U), "value");
            var setter = pi.GetSetMethod();
            var body = Expression.Call(param0, setter, param1);
            var lambda = Expression.Lambda<PropertySetter<T, U>>(body, param0, param1);
            return new PropertySetter<T, U>(lambda.Compile());
        }
        static MemberDeserializer<T, U> CreateFromPropertyInfo<T, U>(PropertyInfo pi
            , TypeRegistory r)
        {
            var setter = CreatePropertySetter<T, U>(pi);
            return new MemberDeserializer<T, U>(pi.Name
                , r.GetDeserializer<U>()
                , (ref T t, U u) => pi.SetValue(t, u, null)
                //, new MemberDeserializer<T, U>.Setter(setter) crash
                , (object o, U u) => pi.SetValue(o, u, null)
                );
        }
        public static IMemberDeserializer<T> CreateMemberDeserializer<T>(
            this PropertyInfo pi, TypeRegistory r)
        {
            var genericMethod = typeof(MemberInfoExtensions).GetMethod("CreateFromPropertyInfo"
                , BindingFlags.Static | BindingFlags.NonPublic);
            var method = genericMethod.MakeGenericMethod(typeof(T), pi.PropertyType);
            return (IMemberDeserializer<T>)method.Invoke(null, new object[] { pi, r });
        }
#endregion
    }
}
