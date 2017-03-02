using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NMessagePack.Serializers;


namespace NMessagePack
{
    public static class FieldInfoExtensions
    {
        public delegate void RefSetterFunc<T>(ref T ojb, object value);
        static void RefSetFieldFromObject<T>(FieldInfo fi, ref object obj, object value)
        {
            fi.SetValue(obj, value.ConvertTo<T>());
        }
        public static RefSetterFunc<T> CreateSetter<T>(this FieldInfo fi)
        {
            var generic = typeof(FieldInfoExtensions).GetMethod("RefSetFieldFromObject"
                , BindingFlags.NonPublic | BindingFlags.Static);
            var method = generic.MakeGenericMethod(new Type[] { fi.FieldType });
            return (ref T obj, object value) =>
            {
                var args = new object[] { fi, obj, value };
                method.Invoke(null, args);
                obj = (T)args[1];
            };
        }

        public delegate void FieldSerializeFunc<T>(MsgPackWriter writer, T target);
        public static FieldSerializeFunc<T> CreateFieldSerializeFunc<T>(this FieldInfo fi)
        {
            var param0 = Expression.Parameter(typeof(MsgPackWriter), "w");
            var param1 = Expression.Parameter(typeof(T), "o");
            var prop = Expression.Field(param1, fi);

            var methods = typeof(Serializer).GetMethods();
            var gmethod = methods.First(x => x.Name == "GetSerializer" && x.IsGenericMethod);
            var method = gmethod.MakeGenericMethod(fi.FieldType);

            var smethods = typeof(SerializerBase<>).MakeGenericType(fi.FieldType).GetMethods();
            var smethod = smethods.First(x => x.Name == "Serialize" && x.GetParameters().Skip(1).First().ParameterType == fi.FieldType);

            var s = Expression.Call(method);
            var body = Expression.Call(s, smethod, param0, prop);
            var lambda = Expression.Lambda<Action<MsgPackWriter, T>>(body, param0, param1);
            var action = lambda.Compile();
            return new FieldSerializeFunc<T>(action);
        }
    }
}
