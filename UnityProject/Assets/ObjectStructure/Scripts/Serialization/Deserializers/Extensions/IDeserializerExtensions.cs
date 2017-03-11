namespace ObjectStructure.Serialization.Deserializers
{
    public static class IDeserializerExtensions
    {
        public static T Deserialize<PARSER, T>(this IDeserializerBase<T> d, PARSER json)
            where PARSER : IParser<PARSER>
        {
            var value = default(T);
            d.Deserialize(json, ref value);
            return value;
        }
    }
}
