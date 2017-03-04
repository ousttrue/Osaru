using System;


namespace ObjectStructure.Json.Deserializers
{
    public class EnumStringDeserializer<PARSER, T> : IDeserializer<PARSER, T>
        where PARSER : IParser<PARSER>
    {
        public void Setup(ITypeRegistory r)
        {
        }

        public void Deserialize(PARSER parser, ref T outValue)
        {
            outValue = (T)Enum.Parse(typeof(T), parser.GetString());
        }
    }
}
