using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NMessagePack.Serializers;


namespace NMessagePack
{
    public static class Serializer
    {
        public static void Clear()
        {
            s_serializerMap = null;
            s_extendedSerializers = null;
        }

        static Dictionary<Type, ISerializer> s_serializerMap;
        static Dictionary<Type, ISerializer> SerializerMap
        {
            get
            {
                if (s_serializerMap == null)
                {
                    s_serializerMap = new Dictionary<Type, ISerializer>
                    {
                        {typeof(Boolean), new LambdaSerializer<Boolean>((w, x)=> w.MsgPack(x)) },
                        {typeof(Byte), new LambdaSerializer<Byte>((w, x)=> w.MsgPack(x))},
                        {typeof(UInt16), new LambdaSerializer<UInt16>((w, x)=>w.MsgPack(x))},
                        {typeof(UInt32), new LambdaSerializer<UInt32>((w, x)=>w.MsgPack(x))},
                        {typeof(UInt64), new LambdaSerializer<UInt64>((w, x)=>w.MsgPack(x))},
                        {typeof(SByte), new LambdaSerializer<SByte>((w, x)=>w.MsgPack(x))},
                        {typeof(Int16), new LambdaSerializer<Int16>((w, x)=>w.MsgPack(x))},
                        {typeof(Int32), new LambdaSerializer<Int32>((w, x)=>w.MsgPack(x))},
                        {typeof(Int64), new LambdaSerializer<Int64>((w, x)=>w.MsgPack(x))},
                        {typeof(Single), new LambdaSerializer<Single>((w, x)=>w.MsgPack(x))},
                        {typeof(Double), new LambdaSerializer<Double>((w, x)=>w.MsgPack(x))},
                        {typeof(String), new LambdaSerializer<String>((w, x)=>w.MsgPack(x))},
                    };
                }
                return s_serializerMap;
            }
        }

        public static SerializerBase<Object> NilSerializer = new NullSerializer();

        public static List<Func<Type, ISerializer>> s_extendedSerializers;
        public static List<Func<Type, ISerializer>> ExtendedSerializers
        {
            get {
                if (s_extendedSerializers==null)
                {
                     s_extendedSerializers = new List<Func<Type, ISerializer>>();
                }
                return s_extendedSerializers;
            }
        }

        public static ISerializer GetSerializer(Type t)
        {
            var s = default(ISerializer);
            if (SerializerMap.TryGetValue(t, out s))
            {
                return s;
            }

            s = BuildSerializer(t);
#if true
            SerializerMap.Add(t, s);
#endif
            return s;
        }

        /*
        public static SerializerBase<T> GetSerializer<T>(T value)
        {
            return GetSerializer<T>();
        }
        */

        public static SerializerBase<T> GetSerializer<T>()
        {
            var t = typeof(T);
            var s = default(ISerializer);
            if (SerializerMap.TryGetValue(t, out s))
            {
                return (SerializerBase<T>)s;
            }

            s = BuildSerializer<T>();
#if true
            SerializerMap.Add(t, s);
#endif
            return (SerializerBase<T>)s;
        }

        static SerializerBase<T> BuildSerializer<T>()
        {
            return (SerializerBase<T>)BuildSerializer(typeof(T));
        }

        static ISerializer BuildSerializer(Type t)
        {
            if (t.IsEnum)
            {
                // enum
                Type constructedType = typeof(EnumSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if (typeof(IList<Byte>).IsAssignableFrom(t))
            {
                // raw
                Type constructedType = typeof(RawSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>))
            {
                // IDictionary<T>
                Type constructedType = typeof(GenericMapSerializer<,>).MakeGenericType(t.GetGenericArguments());
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if (typeof(IDictionary).IsAssignableFrom(t))
            {
                // dictionary
                Type constructedType = typeof(MapSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>))
            {
                // IList<T>
                Type constructedType = typeof(GenericListSerializer<>).MakeGenericType(t.GetGenericArguments());
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if (typeof(ICollection).IsAssignableFrom(t))
            {
                // list
                Type constructedType = typeof(CollectionSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }

            foreach (var ex in ExtendedSerializers)
            {
                var s = ex(t);
                if (s != null)
                {
                    return s;
                }
            }

            if (t.IsClass)
            {
                // Object
                return NilSerializer;
            }

            throw new NotImplementedException("no serialzier for " + t);
        }

        /// <summary>
        /// MsgPackでシリアライスする。1引数
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Byte[] Serialize<T>(T t)
        {
            var s = GetSerializer<T>();
            using (var ms = new MemoryStream())
            {
                var w = new MsgPackWriter(ms);
                s.Serialize(w, t);
                return ms.ToArray();
            }
        }

        public static Byte[] SerializeArray<T0, T1, T2>(T0 t0, T1 t1, T2 t2)
        {
            var s0 = GetSerializer<T0>();
            var s1 = GetSerializer<T1>();
            var s2 = GetSerializer<T2>();
            using (var ms = new MemoryStream())
            {
                var w = new MsgPackWriter(ms);
                w.MsgPackArray(3);
                s0.Serialize(w, t0);
                s1.Serialize(w, t1);
                s2.Serialize(w, t2);
                return ms.ToArray();
            }
        }
        public static Byte[] SerializeArray<T0, T1, T2, T3>(T0 t0, T1 t1, T2 t2, T3 t3)
        {
            var s0 = GetSerializer<T0>();
            var s1 = GetSerializer<T1>();
            var s2 = GetSerializer<T2>();
            var s3 = GetSerializer<T3>();
            using (var ms = new MemoryStream())
            {
                var w = new MsgPackWriter(ms);
                w.MsgPackArray(4);
                s0.Serialize(w, t0);
                s1.Serialize(w, t1);
                s2.Serialize(w, t2);
                s3.Serialize(w, t3);
                return ms.ToArray();
            }
        }

        public static Byte[] SerializeMap<K0, V0, K1, V1>(K0 k0, V0 v0, K1 k1, V1 v1)
        {
            var s0 = GetSerializer<K0>();
            var s1 = GetSerializer<V0>();
            var s2 = GetSerializer<K1>();
            var s3 = GetSerializer<V1>();
            using (var ms = new MemoryStream())
            {
                var w = new MsgPackWriter(ms);
                w.MsgPackMap(2);
                s0.Serialize(w, k0);
                s1.Serialize(w, v0);
                s2.Serialize(w, k1);
                s3.Serialize(w, v1);
                return ms.ToArray();
            }
        }
    }
}

