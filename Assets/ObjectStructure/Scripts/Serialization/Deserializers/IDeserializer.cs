namespace ObjectStructure.Serialization.Deserializers
{
    public interface IDeserializer: ITypeInitializer
    {
    }
    public interface IDeserializerBase<T>: IDeserializer
    {
        void Deserialize<PARSER>(PARSER parser, ref T outValue) 
            where PARSER : IParser<PARSER>;
    }
}
