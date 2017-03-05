using System;


namespace ObjectStructure.Serialization.Deserializers
{
    #region Byte, UInt16, 32, 64
    public class ByteDeserializer : IDeserializer<Byte>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Byte outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetByte();
        }
    }
    public class UInt16Deserializer : IDeserializer<UInt16>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref UInt16 outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetUInt16();
        }
    }
    public class UInt32Deserializer : IDeserializer<UInt32>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref UInt32 outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetUInt32();
        }
    }
    public class UInt64Deserializer : IDeserializer<UInt64>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref UInt64 outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetUInt64();
        }
    }
    #endregion

    #region SByte, Int16, 32, 64
    public class SByteDeserializer : IDeserializer<SByte>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref SByte outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetSByte();
        }
    }
    public class Int16Deserializer : IDeserializer<Int16>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Int16 outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetInt16();
        }
    }
    public class Int32Deserializer : IDeserializer<Int32>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Int32 outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetInt32();
        }
    }
    public class Int64Deserializer : IDeserializer<Int64>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Int64 outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetInt64();
        }
    }
    #endregion

    #region Single, Double
    public class SingleDeserializer : IDeserializer<Single>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Single outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetSingle();
        }
    }
    public class DoubleDeserializer : IDeserializer<Double>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Double outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetDouble();
        }
    }
    #endregion

    public class StringDeserializer : IDeserializer<String>
    {
        public void Setup(TypeRegistory r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref String outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetString();
        }
    }
}
