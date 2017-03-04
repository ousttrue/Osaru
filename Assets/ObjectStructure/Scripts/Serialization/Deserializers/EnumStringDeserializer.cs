using System;


namespace ObjectStructure.Serialization.Deserializers
{
    public class EnumStringDeserializer<T> : IDeserializer<T>
    {
        public void Setup(ITypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref T outValue)
            where PARSER: IParser<PARSER>
        {
            outValue = (T)Enum.Parse(typeof(T), parser.GetString());
        }
    }
}
