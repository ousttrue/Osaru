using System.Reflection;
using Osaru;
using System;


namespace Osaru.Serialization.Serializers
{
    public static class MemberInfoExtensions
    {
        #region Field
        static MemberSerializer<T, U> CreateFromFieldInfo<T, U>(FieldInfo fi
            , TypeRegistry r)
        {
            return new MemberSerializer<T, U>(fi.Name
                , (ref T t) => (U)fi.GetValue(t)
                , r.GetSerializer<U>()
                );
        }
        public static IMemberSerializer<T> CreateMemberSerializer<T>(this FieldInfo fi
            , TypeRegistry r)
        {
            var generic = typeof(MemberInfoExtensions).GetMethod("CreateFromFieldInfo"
                , BindingFlags.Static | BindingFlags.NonPublic);
            var method = generic.MakeGenericMethod(typeof(T), fi.FieldType);
            return (IMemberSerializer<T>)method.Invoke(null, new object[] { fi, r });
        }
        #endregion

        #region Property
        static MemberSerializer<T, U> CreateFromPropertyInfo<T, U>(PropertyInfo pi
            , TypeRegistry r)
        {
            return new MemberSerializer<T, U>(pi.Name
                , (ref T t) => (U)pi.GetValue(t, null)
                , r.GetSerializer<U>()
                );
        }
        public static IMemberSerializer<T> CreateMemberSerializer<T>(this PropertyInfo pi
            , TypeRegistry r)
        {
            var generic = typeof(MemberInfoExtensions).GetMethod("CreateFromPropertyInfo"
                , BindingFlags.Static | BindingFlags.NonPublic);
            var method = generic.MakeGenericMethod(typeof(T), pi.PropertyType);
            return (IMemberSerializer<T>)method.Invoke(null, new object[] { pi, r });
        }
        #endregion
    }
}
