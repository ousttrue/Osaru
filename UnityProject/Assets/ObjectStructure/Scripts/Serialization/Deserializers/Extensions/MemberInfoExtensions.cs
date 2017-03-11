using System.Reflection;


namespace ObjectStructure.Serialization.Deserializers
{
    public static class MemberInfoExtensions
    {
        /*
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
        */
        /*
        public delegate void RefSetterFunc<T>(ref T ojb, object value);
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
        /*
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
        */
        static MemberDeserializer<T, U> CreateFromFieldInfo<T, U>(FieldInfo fi
            , TypeRegistory r)
        {
            return new MemberDeserializer<T, U>(fi.Name
                , r.GetDeserializer<U>()
                , (ref T t, ref U u) => fi.SetValue(t, u)
                , (object o, ref U u) => fi.SetValue(o, u)
                );
        }
        public static IMemberDeserializer<T> CreateMemberDeserializer<T>(
            this FieldInfo fi, TypeRegistory r)
        {
            var genericMethod = typeof(MemberInfoExtensions).GetMethod("CreateFromFieldInfo"
                , BindingFlags.Static | BindingFlags.NonPublic);
            var method = genericMethod.MakeGenericMethod(typeof(T), fi.FieldType);
            return (IMemberDeserializer<T>)method.Invoke(null, new object[] { fi, r });
        }

        static MemberDeserializer<T, U> CreateFromPropertyInfo<T, U>(PropertyInfo pi
            , TypeRegistory r)
        {
            return new MemberDeserializer<T, U>(pi.Name
                , r.GetDeserializer<U>()
                , (ref T t, ref U u) => pi.SetValue(t, u, null)
                , (object o, ref U u) => pi.SetValue(o, u, null)
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
    }
}
