using System;


namespace Osaru.Serialization.Deserializers
{
    public class EnumStringDeserializer<T> : IDeserializerBase<T>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref T outValue)
            where PARSER: IParser<PARSER>
        {
            outValue = (T)Enum.Parse(typeof(T), parser.GetString());
        }
    }
}
