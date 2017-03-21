namespace Osaru.Serialization.Deserializers
{
    public static class IDeserializerExtensions
    {
        public static T Deserialize<PARSER, T>(this IDeserializerBase<T> d, PARSER parser)
            where PARSER : IParser<PARSER>
        {
            var value = default(T);
            d.Deserialize(parser, ref value);
            return value;
        }
    }
}
