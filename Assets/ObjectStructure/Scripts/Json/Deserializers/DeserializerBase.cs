namespace ObjectStructure.Json.Deserializers
{
    public interface IDeserializer<PARSER, T>: Serialization.ITypeInitializer
        where PARSER: IParser<PARSER>
    {
        void Deserialize(PARSER json, ref T outValue);
    }

    public static class IDeserializerExtensions
    {
        public static object Deserialize<PARSER, T>(this IDeserializer<PARSER, T> d, PARSER json)
            where PARSER: IParser<PARSER>
        {
            var value = default(T);
            d.Deserialize(json, ref value);
            return value;
        }
    }
}
