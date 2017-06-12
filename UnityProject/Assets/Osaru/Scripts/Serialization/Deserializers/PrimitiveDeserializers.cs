using System;


namespace Osaru.Serialization.Deserializers
{
    public class BooleanDeserializer : IDeserializerBase<Boolean>
    {
        public void Setup(TypeRegistry r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Boolean outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetBoolean();
        }
    }

    #region Byte, UInt16, 32, 64
    public class ByteDeserializer : IDeserializerBase<Byte>
    {
        public void Setup(TypeRegistry r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Byte outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetByte();
        }
    }
    public class UInt16Deserializer : IDeserializerBase<UInt16>
    {
        public void Setup(TypeRegistry r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref UInt16 outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetUInt16();
        }
    }
    public class UInt32Deserializer : IDeserializerBase<UInt32>
    {
        public void Setup(TypeRegistry r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref UInt32 outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetUInt32();
        }
    }
    public class UInt64Deserializer : IDeserializerBase<UInt64>
    {
        public void Setup(TypeRegistry r)
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
    public class SByteDeserializer : IDeserializerBase<SByte>
    {
        public void Setup(TypeRegistry r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref SByte outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetSByte();
        }
    }
    public class Int16Deserializer : IDeserializerBase<Int16>
    {
        public void Setup(TypeRegistry r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Int16 outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetInt16();
        }
    }
    public class Int32Deserializer : IDeserializerBase<Int32>
    {
        public void Setup(TypeRegistry r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Int32 outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetInt32();
        }
    }
    public class Int64Deserializer : IDeserializerBase<Int64>
    {
        public void Setup(TypeRegistry r)
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
    public class SingleDeserializer : IDeserializerBase<Single>
    {
        public void Setup(TypeRegistry r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Single outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetSingle();
        }
    }
    public class DoubleDeserializer : IDeserializerBase<Double>
    {
        public void Setup(TypeRegistry r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref Double outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetDouble();
        }
    }
    #endregion

    public class StringDeserializer : IDeserializerBase<String>
    {
        public void Setup(TypeRegistry r)
        {
        }

        public void Deserialize<PARSER>(PARSER parser, ref String outValue)
            where PARSER : IParser<PARSER>
        {
            outValue = parser.GetString();
        }
    }
}
