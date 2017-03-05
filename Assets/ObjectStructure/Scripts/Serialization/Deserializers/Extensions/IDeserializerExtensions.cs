﻿namespace ObjectStructure.Serialization.Deserializers
{
    public static class IDeserializerExtensions
    {
        public static object Deserialize<PARSER, T>(this IDeserializer<T> d, PARSER json)
            where PARSER : IParser<PARSER>
        {
            var value = default(T);
            d.Deserialize(json, ref value);
            return value;
        }
    }
}