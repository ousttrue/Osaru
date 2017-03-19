using System;
using System.Collections.Generic;

namespace Osaru.Serialization.Deserializers
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

    public class GenericRawDeserializer<T> : IDeserializerBase<T>
        where T : class, IList<Byte>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref T outValue) 
            where PARSER : IParser<PARSER>
        {
            var bytesSize = parser.GetBytesSize();
            var bytes = new Byte[bytesSize];
            parser.GetBytes(bytes);

            if (outValue == null)
            {
                outValue = Activator.CreateInstance<T>();
            }
            outValue.Clear();
            foreach(var b in bytes)
            {
                outValue.Add(b);
            }
        }
    }
}
