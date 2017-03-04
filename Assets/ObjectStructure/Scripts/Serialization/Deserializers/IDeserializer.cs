namespace ObjectStructure.Serialization.Deserializers
{
    public interface IDeserializer<T>: Serialization.ITypeInitializer
    {
        void Deserialize<PARSER>(PARSER parser, ref T outValue) 
            where PARSER : IParser<PARSER>;
    }
}
