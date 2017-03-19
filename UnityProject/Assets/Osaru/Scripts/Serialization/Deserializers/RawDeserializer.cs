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
            var bytes = parser.GetBytes();
            if (outValue==null || outValue.Length!=bytes.Count)
            {
                outValue = new Byte[bytes.Count];
            }

            Buffer.BlockCopy(bytes.Array, bytes.Offset
                , outValue, 0, bytes.Count);
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
            if (outValue == null)
            {
                outValue = Activator.CreateInstance<T>();
            }

            outValue.Clear();
            foreach(var b in parser.GetBytes().ToEnumerable())
            {
                outValue.Add(b);
            }
        }
    }
}
