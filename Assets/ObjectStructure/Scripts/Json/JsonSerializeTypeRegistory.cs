using ObjectStructure.Json.Deserializers;
using ObjectStructure.Json.Serializers;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace ObjectStructure.Json
{
    /// <summary>
    /// not thread safe
    /// </summary>
    public class JsonSerializeTypeRegistory
    {
        #region Serialize
        Dictionary<Type, ISerializer> m_serializerMap = new Dictionary<Type, ISerializer>()
        {
            {typeof(Boolean), new LambdaSerializer<Boolean>((x, w, r)=> w.Write(x ? "true" : "false")) },
            {typeof(SByte), new LambdaSerializer<SByte>((x, w, r)=> w.Write(x.ToString())) },
            {typeof(Int16), new LambdaSerializer<Int16>((x, w, r)=> w.Write(x.ToString())) },
            {typeof(Int32), new LambdaSerializer<Int32>((x, w, r)=> w.Write(x.ToString())) },
            {typeof(Int64), new LambdaSerializer<Int64>((x, w, r)=> w.Write(x.ToString())) },
            {typeof(Byte), new LambdaSerializer<Byte>((x, w, r)=> w.Write(x.ToString())) },
            {typeof(UInt16), new LambdaSerializer<UInt16>((x, w, r)=> w.Write(x.ToString())) },
            {typeof(UInt32), new LambdaSerializer<UInt32>((x, w, r)=> w.Write(x.ToString())) },
            {typeof(UInt64), new LambdaSerializer<UInt64>((x, w, r)=> w.Write(x.ToString())) },
            {typeof(Single), new LambdaSerializer<Single>((x, w, r)=> w.Write(x.ToString())) },
            {typeof(Double), new LambdaSerializer<Double>((x, w, r)=> w.Write(x.ToString())) },
            {typeof(string), new LambdaSerializer<string>((x, w, r) => w.Write(JsonParser.Quote(x))) },
        };

        public ISerializer GetSerializer(Type t)
        {
            ISerializer serializer;
            if (m_serializerMap.TryGetValue(t, out serializer))
            {
                return serializer;
            }

            serializer = CreateSerializer(t);
            if (serializer != null)
            {
                serializer.Setup(this);
                m_serializerMap.Add(t, serializer);
            }

            return serializer;
        }

        public ISerializer GetSerializer<T>()
        {
            var t = typeof(T);
            var serializer= GetSerializer(t);
            return serializer;
        }

        ISerializer CreateSerializer(Type t)
        {
            if (t.IsArray && t.GetElementType() != null)
            {
                // T[]
                Type constructedType = typeof(TypedArraySerializer<>).MakeGenericType(t.GetElementType());
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.GetInterfaces().Any(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(IList<>)))
            {
                // IList<T>
                Type constructedType = typeof(GenericListSerializer<>).MakeGenericType(t.GetGenericArguments());
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
            else
            {
                // object
                Type constructedType = typeof(ReflectionSerializer<>).MakeGenericType(t);
                return (ISerializer)Activator.CreateInstance(constructedType, null);
            }
        }
        #endregion

        #region Deserialize
        Dictionary<Type, IDeserializer> m_deserializerMap = new Dictionary<Type, IDeserializer>
        {
            {typeof(SByte), new LambdaDeserializer<SByte>((JsonParser json, ref SByte outValue, JsonSerializeTypeRegistory r) => outValue=(SByte)json.GetNumber() )},
            {typeof(Int16), new LambdaDeserializer<Int16>((JsonParser json, ref Int16 outValue, JsonSerializeTypeRegistory r) => outValue=(Int16)json.GetNumber() )},
            {typeof(Int32), new LambdaDeserializer<Int32>((JsonParser json, ref Int32 outValue, JsonSerializeTypeRegistory r) => outValue=(Int32)json.GetNumber() )},
            {typeof(Int64), new LambdaDeserializer<Int64>((JsonParser json, ref Int64 outValue, JsonSerializeTypeRegistory r) => outValue=(Int64)json.GetNumber() )},
            {typeof(Byte), new LambdaDeserializer<Byte>((JsonParser json, ref Byte outValue, JsonSerializeTypeRegistory r) => outValue=(Byte)json.GetNumber() )},
            {typeof(UInt16), new LambdaDeserializer<UInt16>((JsonParser json, ref UInt16 outValue, JsonSerializeTypeRegistory r) => outValue=(UInt16)json.GetNumber() )},
            {typeof(UInt32), new LambdaDeserializer<UInt32>((JsonParser json, ref UInt32 outValue, JsonSerializeTypeRegistory r) => outValue=(UInt32)json.GetNumber() )},
            {typeof(UInt64), new LambdaDeserializer<UInt64>((JsonParser json, ref UInt64 outValue, JsonSerializeTypeRegistory r) => outValue=(UInt64)json.GetNumber() )},
            {typeof(Single), new LambdaDeserializer<Single>((JsonParser json, ref Single outValue, JsonSerializeTypeRegistory r) => outValue=(Single)json.GetNumber() )},
            {typeof(Double), new LambdaDeserializer<Double>((JsonParser json, ref Double outValue, JsonSerializeTypeRegistory r) => outValue=(Double)json.GetNumber() )},
        };

        public DeserializerBase<T> GetDeserializer<T>()
        {
            IDeserializer deserializer;
            var t = typeof(T);
            if (m_deserializerMap.TryGetValue(t, out deserializer))
            {
                return (DeserializerBase<T>)deserializer;
            }

            deserializer = CreateDeserializer(t);
            if (deserializer != null)
            {
                deserializer.Setup(this);
                m_deserializerMap.Add(t, deserializer);
            }
            return (DeserializerBase<T>)deserializer;
        }

        IDeserializer CreateDeserializer(Type t)
        {
            if (t.IsArray && t.GetElementType() != null)
            {
                // T[]
                Type constructedType = typeof(TypedArrayDeserializer<>).MakeGenericType(t.GetElementType());
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else if (t.GetInterfaces().Any(x =>
            x.IsGenericType &&
            x.GetGenericTypeDefinition() == typeof(IList<>)))
            {
                // IList<T>
                Type constructedType = typeof(GenericListDeserializer<>).MakeGenericType(t.GetGenericArguments());
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
            else
            {
                // object
                Type constructedType = typeof(ReflectionStructDeserializer<>).MakeGenericType(t);
                return (IDeserializer)Activator.CreateInstance(constructedType, null);
            }
        }
        #endregion

        public void AddType<T>(SerializerBase<T> serializer, DeserializerBase<T> deserializer)
        {
            m_serializerMap.Add(typeof(T), serializer);
            m_deserializerMap.Add(typeof(T), deserializer);
        }
    }
}
