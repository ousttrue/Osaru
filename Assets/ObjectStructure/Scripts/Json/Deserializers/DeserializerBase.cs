namespace ObjectStructure.Json.Deserializers
{
    public interface IDeserializer<T>: Serialization.ITypeInitializer
    {
        void Deserialize<PARSER>(PARSER json, ref T outValue) 
            where PARSER : IParser<PARSER>;
    }

    public static class IDeserializerExtensions
    {
        public static object Deserialize<PARSER, T>(this IDeserializer<T> d, PARSER json)
            where PARSER: IParser<PARSER>
        {
            var value = default(T);
            d.Deserialize(json, ref value);
            return value;
        }
    }
}
