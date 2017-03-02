using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NMessagePack.Serializers;


namespace NMessagePack
{
    public static class PropertyInfoExtensions
    {
        public delegate void RefSetterFunc<T>(ref T ojb, object value);

        /*
        static RefSetterFunc<T> RefSetPropertyFromObject<T>(PropertyInfo pi)
        {
            var param0 = Expression.Parameter(typeof(T).MakeByRefType(), "obj");
            var param1 = Expression.Parameter(typeof(object), "value");
            var casted1 = Expression.Convert(param1, pi.PropertyType);
            var setter = pi.GetSetMethod();
            var body = Expression.Call(param0, setter, casted1);
            var lambda = Expression.Lambda<RefSetterFunc<T>>(body, param0, param1);
            return new RefSetterFunc<T>(lambda.Compile());
        }
        */
        public static RefSetterFunc<T> CreateSetter<T>(this PropertyInfo pi)
        {
            return new RefSetterFunc<T>((ref T t, object value) =>
            {
                pi.SetValue(t, value, null);
            });
        }

        public delegate void PropSerializeFunc<T>(MsgPackWriter writer, T target);
        public static PropSerializeFunc<T> CreatePropSerializeFunc<T>(this PropertyInfo pi)
        {
            var param0 = Expression.Parameter(typeof(MsgPackWriter), "w");
            var param1 = Expression.Parameter(typeof(T), "o");
            var prop = Expression.Property(param1, pi);

            var methods = typeof(Serializer).GetMethods();
            var gmethod = methods.First(x => x.Name == "GetSerializer" && x.IsGenericMethod);
            var method = gmethod.MakeGenericMethod(pi.PropertyType);

            var smethods = typeof(SerializerBase<>).MakeGenericType(pi.PropertyType).GetMethods();
            var smethod = smethods.First(x => x.Name == "Serialize" && x.GetParameters().Skip(1).First().ParameterType == pi.PropertyType);

            var s = Expression.Call(method);
            var body = Expression.Call(s, smethod, param0, prop);
            var lambda = Expression.Lambda<Action<MsgPackWriter, T>>(body, param0, param1);
            var action = lambda.Compile();
            return new PropSerializeFunc<T>(action);
        }
    }
}
