using System;

namespace ObjectStructure.Json.Deserializers
{
    public class LambdaDeserializer<PARSER, T> : IDeserializer<PARSER, T>
        where PARSER : IParser<PARSER>
    {
        public delegate void DeserializeFunc(PARSER parser, ref T outValue);
        DeserializeFunc m_deserializer;
        public LambdaDeserializer(DeserializeFunc deserializer)
        {
            m_deserializer = deserializer;
        }

        public void Setup(ITypeRegistory r)
        {
        }

        public void Deserialize(PARSER parser, ref T outValue)
        {
            m_deserializer(parser, ref outValue);
        }
    }
}
