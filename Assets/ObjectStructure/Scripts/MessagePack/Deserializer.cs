using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using NMessagePack.Deserializers;


namespace NMessagePack
{
    public static class Deserializer
    {
        public static void Clear()
        {
            s_deserializerMap = null;
            s_extendedDeserializers = null;
        }

        static Dictionary<Type, IDeserializer> s_deserializerMap;
        static Dictionary<Type, IDeserializer> DeserializerMap
        {
            get
            {
                if (s_deserializerMap == null)
                {
                    s_deserializerMap = new Dictionary<Type, IDeserializer>
                    {
                        {typeof(Boolean),  new LambdaDeserilaizer<Boolean>( v => v.GetValue<Boolean>() ) },
                        {typeof(Byte),  new LambdaDeserilaizer<Byte>( v => v.GetValue<Byte>() ) },
                        {typeof(UInt16),  new LambdaDeserilaizer<UInt16>( v => v.GetValue<UInt16>() ) },
                        {typeof(UInt32),  new LambdaDeserilaizer<UInt32>( v => v.GetValue<UInt32>() ) },
                        {typeof(UInt64),  new LambdaDeserilaizer<UInt64>( v => v.GetValue<UInt64>() ) },
                        {typeof(SByte),  new LambdaDeserilaizer<SByte>( v => v.GetValue<SByte>() ) },
                        {typeof(Int16),  new LambdaDeserilaizer<Int16>( v => v.GetValue<Int16>() ) },
                        {typeof(Int32),  new LambdaDeserilaizer<Int32>( v => v.GetValue<Int32>() ) },
                        {typeof(Int64),  new LambdaDeserilaizer<Int64>( v => v.GetValue<Int64>() ) },
                        {typeof(Single),  new LambdaDeserilaizer<Single>( v => v.GetValue<Single>() ) },
                        {typeof(Double),  new LambdaDeserilaizer<Double>( v => v.GetValue<Double>() ) },
                        {typeof(String),  new LambdaDeserilaizer<String>( v => v.GetValue<String>() ) },
                        {typeof(Byte[]), new LambdaDeserilaizer<Byte[]>( v=> ((BytesSegment)v.GetValue()).ToArray())},
                        {typeof(Object), new LambdaDeserilaizer<Object>( v=>v.GetValue() )},
                };
                }
                return s_deserializerMap;
            }
        }

        static List<Func<Type, IDeserializer>> s_extendedDeserializers;
        public static List<Func<Type, IDeserializer>> ExtendedDeserializers
        {
            get {
                if (s_extendedDeserializers == null)
                {
                    s_extendedDeserializers = new List<Func<Type, IDeserializer>>();
                }
                return s_extendedDeserializers;
            }
        }

        public static DeserializerBase<T> GetDeserializer<T>()
        {
            return (DeserializerBase<T>)GetDeserializer(typeof(T));
        }

        public static IDeserializer GetDeserializer(Type t)
        {
            var d = default(IDeserializer);
            if (DeserializerMap.TryGetValue(t, out d))
            {
                return d;
            }

            d = BuildDeserializer(t);

            // add
            DeserializerMap.Add(t, d);

            return d;
        }

        static IDeserializer BuildDeserializer(Type t)
        {
            if (t.IsEnum)
            {
                Type constructedType = typeof(EnumDeserializer<,>).MakeGenericType(t, Enum.GetUnderlyingType(t));
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsGenericType && typeof(IDictionary).IsAssignableFrom(t))
            {
                var types = new Type[] { t }.Concat(t.GetGenericArguments()).ToArray();
                // IDictionary<K, V>
                Type constructedType = typeof(GenericMapDeserializer<,,>).MakeGenericType(types);
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsArray && t.HasElementType)
            {
                // T[]
                Type constructedType = typeof(ArrayDeserializer<>).MakeGenericType(t.GetElementType());
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IList<>))
            {
                // IList<T>
                Type constructedType = typeof(GenericListDeserializer<,>).MakeGenericType(t, t.GetGenericArguments().First());
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }

            foreach (var ex in ExtendedDeserializers)
            {
                var d = ex(t);
                if (d != null)
                {
                    return d;
                }
            }

            throw new NotImplementedException("no deserializer for " + t);
        }

        public static T Deserialize<T>(Byte[] bytes)
        {
            return Deserialize<T>(new BytesSegment(bytes));
        }

        public static T Deserialize<T>(BytesSegment bytes)
        {
            var v = MsgPackValue.Parse(bytes);
            if (v.FormatType == MsgPackType.NIL)
            {
                if (typeof(T).IsClass)
                {
                    // null
                    return default(T);
                }
                else
                {
                    throw new InvalidOperationException("nil for struct");
                }
            }
            var d = Deserializer.GetDeserializer<T>();
            return d.Deserialize(v);
        }
    }
}
