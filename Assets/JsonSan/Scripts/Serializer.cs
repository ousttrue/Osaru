using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JsonSan
{
    public interface ISerializer
    {
    }
    public abstract class SerializerBase<T> : ISerializer
    {
        public abstract string Serialize(T t);
    }
    public class LambdaSerializer<T> : SerializerBase<T>
    {
        Func<T, string> m_serializer;
        public LambdaSerializer(Func<T, string> serializer)
        {
            m_serializer = serializer;
        }
        public override string Serialize(T t)
        {
            return m_serializer(t);
        }
    }


    public interface IDeserializer
    {
    }
    public abstract class DeserializerBase<T> : IDeserializer
    {
        public abstract T Deserialize(string json);
    }
    public class LambdaDeserializer<T>: DeserializerBase<T>
    {
        Func<string, T> m_deserializer;
        public LambdaDeserializer(Func<string, T> deserializer)
        {
            m_deserializer = deserializer;
        }

        public override T Deserialize(string json)
        {
            return m_deserializer(json);
        }
    }


    public class TypeRegistory
    {
        #region Serialize
        Dictionary<Type, ISerializer> m_serializerMap = new Dictionary<Type, ISerializer>()
        {
            {typeof(int), new LambdaSerializer<int>(x=> x.ToString()) },
        };

        SerializerBase<T> GetSerializer<T>()
        {
            return (SerializerBase<T>)m_serializerMap[typeof(T)];
        }

        public string Serialize<T>(T t)
        {
            var serializer = GetSerializer<T>();
            return serializer.Serialize(t);
        }
        #endregion

        #region Deserialize
        Dictionary<Type, IDeserializer> m_deserializerMap = new Dictionary<Type, IDeserializer>
        {
            {typeof(int), new LambdaDeserializer<int>(x => (int)Node.Parse(x).GetNumber() )},
        };

        DeserializerBase<T> GetDeserializer<T>()
        {
            return (DeserializerBase<T>)m_deserializerMap[typeof(T)];
        }

        public T Deserialize<T>(string json)
        {
            var deserializer = GetDeserializer<T>();
            return deserializer.Deserialize(json);
        }
        #endregion
    }
}
