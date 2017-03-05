using System;


namespace ObjectStructure.Serialization.Deserializers
{
    public class RawDeserializer : IDeserializerBase<Byte[]>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Byte[] outValue)
            where PARSER: IParser<PARSER>
        {
            var bytesSize = parser.GetBytesSize();
            if (outValue==null || outValue.Length!=bytesSize)
            {
                outValue = new Byte[bytesSize];
            }

            parser.GetBytes(outValue);            
        }
    }
}
