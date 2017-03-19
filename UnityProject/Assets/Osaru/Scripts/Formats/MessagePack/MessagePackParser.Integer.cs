
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Osaru.MessagePack
{
    public partial struct MessagePackParser: IParser<MessagePackParser>
    {
        
        public Byte GetByte()
        {
            unchecked{
            switch(FormatType)
            {
                case MsgPackType.POSITIVE_FIXNUM: return (byte)0;
                case MsgPackType.POSITIVE_FIXNUM_0x01: return (byte)1;
                case MsgPackType.POSITIVE_FIXNUM_0x02: return (byte)2;
                case MsgPackType.POSITIVE_FIXNUM_0x03: return (byte)3;
                case MsgPackType.POSITIVE_FIXNUM_0x04: return (byte)4;
                case MsgPackType.POSITIVE_FIXNUM_0x05: return (byte)5;
                case MsgPackType.POSITIVE_FIXNUM_0x06: return (byte)6;
                case MsgPackType.POSITIVE_FIXNUM_0x07: return (byte)7;
                case MsgPackType.POSITIVE_FIXNUM_0x08: return (byte)8;
                case MsgPackType.POSITIVE_FIXNUM_0x09: return (byte)9;
                case MsgPackType.POSITIVE_FIXNUM_0x0A: return (byte)10;
                case MsgPackType.POSITIVE_FIXNUM_0x0B: return (byte)11;
                case MsgPackType.POSITIVE_FIXNUM_0x0C: return (byte)12;
                case MsgPackType.POSITIVE_FIXNUM_0x0D: return (byte)13;
                case MsgPackType.POSITIVE_FIXNUM_0x0E: return (byte)14;
                case MsgPackType.POSITIVE_FIXNUM_0x0F: return (byte)15;

                case MsgPackType.POSITIVE_FIXNUM_0x10: return (byte)16;
                case MsgPackType.POSITIVE_FIXNUM_0x11: return (byte)17;
                case MsgPackType.POSITIVE_FIXNUM_0x12: return (byte)18;
                case MsgPackType.POSITIVE_FIXNUM_0x13: return (byte)19;
                case MsgPackType.POSITIVE_FIXNUM_0x14: return (byte)20;
                case MsgPackType.POSITIVE_FIXNUM_0x15: return (byte)21;
                case MsgPackType.POSITIVE_FIXNUM_0x16: return (byte)22;
                case MsgPackType.POSITIVE_FIXNUM_0x17: return (byte)23;
                case MsgPackType.POSITIVE_FIXNUM_0x18: return (byte)24;
                case MsgPackType.POSITIVE_FIXNUM_0x19: return (byte)25;
                case MsgPackType.POSITIVE_FIXNUM_0x1A: return (byte)26;
                case MsgPackType.POSITIVE_FIXNUM_0x1B: return (byte)27;
                case MsgPackType.POSITIVE_FIXNUM_0x1C: return (byte)28;
                case MsgPackType.POSITIVE_FIXNUM_0x1D: return (byte)29;
                case MsgPackType.POSITIVE_FIXNUM_0x1E: return (byte)30;
                case MsgPackType.POSITIVE_FIXNUM_0x1F: return (byte)31;

                case MsgPackType.POSITIVE_FIXNUM_0x20: return (byte)32;
                case MsgPackType.POSITIVE_FIXNUM_0x21: return (byte)33;
                case MsgPackType.POSITIVE_FIXNUM_0x22: return (byte)34;
                case MsgPackType.POSITIVE_FIXNUM_0x23: return (byte)35;
                case MsgPackType.POSITIVE_FIXNUM_0x24: return (byte)36;
                case MsgPackType.POSITIVE_FIXNUM_0x25: return (byte)37;
                case MsgPackType.POSITIVE_FIXNUM_0x26: return (byte)38;
                case MsgPackType.POSITIVE_FIXNUM_0x27: return (byte)39;
                case MsgPackType.POSITIVE_FIXNUM_0x28: return (byte)40;
                case MsgPackType.POSITIVE_FIXNUM_0x29: return (byte)41;
                case MsgPackType.POSITIVE_FIXNUM_0x2A: return (byte)42;
                case MsgPackType.POSITIVE_FIXNUM_0x2B: return (byte)43;
                case MsgPackType.POSITIVE_FIXNUM_0x2C: return (byte)44;
                case MsgPackType.POSITIVE_FIXNUM_0x2D: return (byte)45;
                case MsgPackType.POSITIVE_FIXNUM_0x2E: return (byte)46;
                case MsgPackType.POSITIVE_FIXNUM_0x2F: return (byte)47;

                case MsgPackType.POSITIVE_FIXNUM_0x30: return (byte)48;
                case MsgPackType.POSITIVE_FIXNUM_0x31: return (byte)49;
                case MsgPackType.POSITIVE_FIXNUM_0x32: return (byte)50;
                case MsgPackType.POSITIVE_FIXNUM_0x33: return (byte)51;
                case MsgPackType.POSITIVE_FIXNUM_0x34: return (byte)52;
                case MsgPackType.POSITIVE_FIXNUM_0x35: return (byte)53;
                case MsgPackType.POSITIVE_FIXNUM_0x36: return (byte)54;
                case MsgPackType.POSITIVE_FIXNUM_0x37: return (byte)55;
                case MsgPackType.POSITIVE_FIXNUM_0x38: return (byte)56;
                case MsgPackType.POSITIVE_FIXNUM_0x39: return (byte)57;
                case MsgPackType.POSITIVE_FIXNUM_0x3A: return (byte)58;
                case MsgPackType.POSITIVE_FIXNUM_0x3B: return (byte)59;
                case MsgPackType.POSITIVE_FIXNUM_0x3C: return (byte)60;
                case MsgPackType.POSITIVE_FIXNUM_0x3D: return (byte)61;
                case MsgPackType.POSITIVE_FIXNUM_0x3E: return (byte)62;
                case MsgPackType.POSITIVE_FIXNUM_0x3F: return (byte)63;

                case MsgPackType.POSITIVE_FIXNUM_0x40: return (byte)64;
                case MsgPackType.POSITIVE_FIXNUM_0x41: return (byte)65;
                case MsgPackType.POSITIVE_FIXNUM_0x42: return (byte)66;
                case MsgPackType.POSITIVE_FIXNUM_0x43: return (byte)67;
                case MsgPackType.POSITIVE_FIXNUM_0x44: return (byte)68;
                case MsgPackType.POSITIVE_FIXNUM_0x45: return (byte)69;
                case MsgPackType.POSITIVE_FIXNUM_0x46: return (byte)70;
                case MsgPackType.POSITIVE_FIXNUM_0x47: return (byte)71;
                case MsgPackType.POSITIVE_FIXNUM_0x48: return (byte)72;
                case MsgPackType.POSITIVE_FIXNUM_0x49: return (byte)73;
                case MsgPackType.POSITIVE_FIXNUM_0x4A: return (byte)74; 
                case MsgPackType.POSITIVE_FIXNUM_0x4B: return (byte)75;
                case MsgPackType.POSITIVE_FIXNUM_0x4C: return (byte)76;
                case MsgPackType.POSITIVE_FIXNUM_0x4D: return (byte)77;
                case MsgPackType.POSITIVE_FIXNUM_0x4E: return (byte)78;
                case MsgPackType.POSITIVE_FIXNUM_0x4F: return (byte)79;
                     
                case MsgPackType.POSITIVE_FIXNUM_0x50: return (byte)80;
                case MsgPackType.POSITIVE_FIXNUM_0x51: return (byte)81;
                case MsgPackType.POSITIVE_FIXNUM_0x52: return (byte)82;
                case MsgPackType.POSITIVE_FIXNUM_0x53: return (byte)83;
                case MsgPackType.POSITIVE_FIXNUM_0x54: return (byte)84;
                case MsgPackType.POSITIVE_FIXNUM_0x55: return (byte)85;
                case MsgPackType.POSITIVE_FIXNUM_0x56: return (byte)86;
                case MsgPackType.POSITIVE_FIXNUM_0x57: return (byte)87;
                case MsgPackType.POSITIVE_FIXNUM_0x58: return (byte)88;
                case MsgPackType.POSITIVE_FIXNUM_0x59: return (byte)89;
                case MsgPackType.POSITIVE_FIXNUM_0x5A: return (byte)90;
                case MsgPackType.POSITIVE_FIXNUM_0x5B: return (byte)91;
                case MsgPackType.POSITIVE_FIXNUM_0x5C: return (byte)92;
                case MsgPackType.POSITIVE_FIXNUM_0x5D: return (byte)93;
                case MsgPackType.POSITIVE_FIXNUM_0x5E: return (byte)94;
                case MsgPackType.POSITIVE_FIXNUM_0x5F: return (byte)95;

                case MsgPackType.POSITIVE_FIXNUM_0x60: return (byte)96;
                case MsgPackType.POSITIVE_FIXNUM_0x61: return (byte)97;
                case MsgPackType.POSITIVE_FIXNUM_0x62: return (byte)98;
                case MsgPackType.POSITIVE_FIXNUM_0x63: return (byte)99;
                case MsgPackType.POSITIVE_FIXNUM_0x64: return (byte)100;
                case MsgPackType.POSITIVE_FIXNUM_0x65: return (byte)101;
                case MsgPackType.POSITIVE_FIXNUM_0x66: return (byte)102;
                case MsgPackType.POSITIVE_FIXNUM_0x67: return (byte)103;
                case MsgPackType.POSITIVE_FIXNUM_0x68: return (byte)104;
                case MsgPackType.POSITIVE_FIXNUM_0x69: return (byte)105;
                case MsgPackType.POSITIVE_FIXNUM_0x6A: return (byte)106;
                case MsgPackType.POSITIVE_FIXNUM_0x6B: return (byte)107;
                case MsgPackType.POSITIVE_FIXNUM_0x6C: return (byte)108;
                case MsgPackType.POSITIVE_FIXNUM_0x6D: return (byte)109;
                case MsgPackType.POSITIVE_FIXNUM_0x6E: return (byte)110;
                case MsgPackType.POSITIVE_FIXNUM_0x6F: return (byte)111;

                case MsgPackType.POSITIVE_FIXNUM_0x70: return (byte)112;
                case MsgPackType.POSITIVE_FIXNUM_0x71: return (byte)113;
                case MsgPackType.POSITIVE_FIXNUM_0x72: return (byte)114;
                case MsgPackType.POSITIVE_FIXNUM_0x73: return (byte)115;
                case MsgPackType.POSITIVE_FIXNUM_0x74: return (byte)116;
                case MsgPackType.POSITIVE_FIXNUM_0x75: return (byte)117;
                case MsgPackType.POSITIVE_FIXNUM_0x76: return (byte)118;
                case MsgPackType.POSITIVE_FIXNUM_0x77: return (byte)119;
                case MsgPackType.POSITIVE_FIXNUM_0x78: return (byte)120;
                case MsgPackType.POSITIVE_FIXNUM_0x79: return (byte)121;
                case MsgPackType.POSITIVE_FIXNUM_0x7A: return (byte)122;
                case MsgPackType.POSITIVE_FIXNUM_0x7B: return (byte)123;
                case MsgPackType.POSITIVE_FIXNUM_0x7C: return (byte)124;
                case MsgPackType.POSITIVE_FIXNUM_0x7D: return (byte)125;
                case MsgPackType.POSITIVE_FIXNUM_0x7E: return (byte)126;
                case MsgPackType.POSITIVE_FIXNUM_0x7F: return (byte)127;

                case MsgPackType.NEGATIVE_FIXNUM: return (byte)-32;
                case MsgPackType.NEGATIVE_FIXNUM_0x01: return (byte)-1;
                case MsgPackType.NEGATIVE_FIXNUM_0x02: return (byte)-2;
                case MsgPackType.NEGATIVE_FIXNUM_0x03: return (byte)-3;
                case MsgPackType.NEGATIVE_FIXNUM_0x04: return (byte)-4;
                case MsgPackType.NEGATIVE_FIXNUM_0x05: return (byte)-5;
                case MsgPackType.NEGATIVE_FIXNUM_0x06: return (byte)-6;
                case MsgPackType.NEGATIVE_FIXNUM_0x07: return (byte)-7;
                case MsgPackType.NEGATIVE_FIXNUM_0x08: return (byte)-8;
                case MsgPackType.NEGATIVE_FIXNUM_0x09: return (byte)-9;
                case MsgPackType.NEGATIVE_FIXNUM_0x0A: return (byte)-10;
                case MsgPackType.NEGATIVE_FIXNUM_0x0B: return (byte)-11;
                case MsgPackType.NEGATIVE_FIXNUM_0x0C: return (byte)-12;
                case MsgPackType.NEGATIVE_FIXNUM_0x0D: return (byte)-13;
                case MsgPackType.NEGATIVE_FIXNUM_0x0E: return (byte)-14;
                case MsgPackType.NEGATIVE_FIXNUM_0x0F: return (byte)-15;
                case MsgPackType.NEGATIVE_FIXNUM_0x10: return (byte)-16;
                case MsgPackType.NEGATIVE_FIXNUM_0x11: return (byte)-17;
                case MsgPackType.NEGATIVE_FIXNUM_0x12: return (byte)-18;
                case MsgPackType.NEGATIVE_FIXNUM_0x13: return (byte)-19;
                case MsgPackType.NEGATIVE_FIXNUM_0x14: return (byte)-20;
                case MsgPackType.NEGATIVE_FIXNUM_0x15: return (byte)-21;
                case MsgPackType.NEGATIVE_FIXNUM_0x16: return (byte)-22;
                case MsgPackType.NEGATIVE_FIXNUM_0x17: return (byte)-23;
                case MsgPackType.NEGATIVE_FIXNUM_0x18: return (byte)-24;
                case MsgPackType.NEGATIVE_FIXNUM_0x19: return (byte)-25;
                case MsgPackType.NEGATIVE_FIXNUM_0x1A: return (byte)-26;
                case MsgPackType.NEGATIVE_FIXNUM_0x1B: return (byte)-27;
                case MsgPackType.NEGATIVE_FIXNUM_0x1C: return (byte)-28;
                case MsgPackType.NEGATIVE_FIXNUM_0x1D: return (byte)-29;
                case MsgPackType.NEGATIVE_FIXNUM_0x1E: return (byte)-30;
                case MsgPackType.NEGATIVE_FIXNUM_0x1F: return (byte)-31;

                case MsgPackType.INT8: return (byte)(SByte)GetBody().Get(0);
                case MsgPackType.INT16: return (byte)EndianConverter.NetworkByteWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT32: return (byte)EndianConverter.NetworkByteDWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT64: return (byte)EndianConverter.NetworkByteQWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.UINT8: return (byte)GetBody().Get(0);
                case MsgPackType.UINT16: return (byte)EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT32: return (byte)EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT64: return (byte)EndianConverter.NetworkByteQWordToUnsignedNativeByteOrder(GetBody());

                default: throw new MessagePackValueException("is not byte" + Bytes);
            }
            }
        }

        public UInt16 GetUInt16()
        {
            unchecked{
            switch(FormatType)
            {
                case MsgPackType.POSITIVE_FIXNUM: return (ushort)0;
                case MsgPackType.POSITIVE_FIXNUM_0x01: return (ushort)1;
                case MsgPackType.POSITIVE_FIXNUM_0x02: return (ushort)2;
                case MsgPackType.POSITIVE_FIXNUM_0x03: return (ushort)3;
                case MsgPackType.POSITIVE_FIXNUM_0x04: return (ushort)4;
                case MsgPackType.POSITIVE_FIXNUM_0x05: return (ushort)5;
                case MsgPackType.POSITIVE_FIXNUM_0x06: return (ushort)6;
                case MsgPackType.POSITIVE_FIXNUM_0x07: return (ushort)7;
                case MsgPackType.POSITIVE_FIXNUM_0x08: return (ushort)8;
                case MsgPackType.POSITIVE_FIXNUM_0x09: return (ushort)9;
                case MsgPackType.POSITIVE_FIXNUM_0x0A: return (ushort)10;
                case MsgPackType.POSITIVE_FIXNUM_0x0B: return (ushort)11;
                case MsgPackType.POSITIVE_FIXNUM_0x0C: return (ushort)12;
                case MsgPackType.POSITIVE_FIXNUM_0x0D: return (ushort)13;
                case MsgPackType.POSITIVE_FIXNUM_0x0E: return (ushort)14;
                case MsgPackType.POSITIVE_FIXNUM_0x0F: return (ushort)15;

                case MsgPackType.POSITIVE_FIXNUM_0x10: return (ushort)16;
                case MsgPackType.POSITIVE_FIXNUM_0x11: return (ushort)17;
                case MsgPackType.POSITIVE_FIXNUM_0x12: return (ushort)18;
                case MsgPackType.POSITIVE_FIXNUM_0x13: return (ushort)19;
                case MsgPackType.POSITIVE_FIXNUM_0x14: return (ushort)20;
                case MsgPackType.POSITIVE_FIXNUM_0x15: return (ushort)21;
                case MsgPackType.POSITIVE_FIXNUM_0x16: return (ushort)22;
                case MsgPackType.POSITIVE_FIXNUM_0x17: return (ushort)23;
                case MsgPackType.POSITIVE_FIXNUM_0x18: return (ushort)24;
                case MsgPackType.POSITIVE_FIXNUM_0x19: return (ushort)25;
                case MsgPackType.POSITIVE_FIXNUM_0x1A: return (ushort)26;
                case MsgPackType.POSITIVE_FIXNUM_0x1B: return (ushort)27;
                case MsgPackType.POSITIVE_FIXNUM_0x1C: return (ushort)28;
                case MsgPackType.POSITIVE_FIXNUM_0x1D: return (ushort)29;
                case MsgPackType.POSITIVE_FIXNUM_0x1E: return (ushort)30;
                case MsgPackType.POSITIVE_FIXNUM_0x1F: return (ushort)31;

                case MsgPackType.POSITIVE_FIXNUM_0x20: return (ushort)32;
                case MsgPackType.POSITIVE_FIXNUM_0x21: return (ushort)33;
                case MsgPackType.POSITIVE_FIXNUM_0x22: return (ushort)34;
                case MsgPackType.POSITIVE_FIXNUM_0x23: return (ushort)35;
                case MsgPackType.POSITIVE_FIXNUM_0x24: return (ushort)36;
                case MsgPackType.POSITIVE_FIXNUM_0x25: return (ushort)37;
                case MsgPackType.POSITIVE_FIXNUM_0x26: return (ushort)38;
                case MsgPackType.POSITIVE_FIXNUM_0x27: return (ushort)39;
                case MsgPackType.POSITIVE_FIXNUM_0x28: return (ushort)40;
                case MsgPackType.POSITIVE_FIXNUM_0x29: return (ushort)41;
                case MsgPackType.POSITIVE_FIXNUM_0x2A: return (ushort)42;
                case MsgPackType.POSITIVE_FIXNUM_0x2B: return (ushort)43;
                case MsgPackType.POSITIVE_FIXNUM_0x2C: return (ushort)44;
                case MsgPackType.POSITIVE_FIXNUM_0x2D: return (ushort)45;
                case MsgPackType.POSITIVE_FIXNUM_0x2E: return (ushort)46;
                case MsgPackType.POSITIVE_FIXNUM_0x2F: return (ushort)47;

                case MsgPackType.POSITIVE_FIXNUM_0x30: return (ushort)48;
                case MsgPackType.POSITIVE_FIXNUM_0x31: return (ushort)49;
                case MsgPackType.POSITIVE_FIXNUM_0x32: return (ushort)50;
                case MsgPackType.POSITIVE_FIXNUM_0x33: return (ushort)51;
                case MsgPackType.POSITIVE_FIXNUM_0x34: return (ushort)52;
                case MsgPackType.POSITIVE_FIXNUM_0x35: return (ushort)53;
                case MsgPackType.POSITIVE_FIXNUM_0x36: return (ushort)54;
                case MsgPackType.POSITIVE_FIXNUM_0x37: return (ushort)55;
                case MsgPackType.POSITIVE_FIXNUM_0x38: return (ushort)56;
                case MsgPackType.POSITIVE_FIXNUM_0x39: return (ushort)57;
                case MsgPackType.POSITIVE_FIXNUM_0x3A: return (ushort)58;
                case MsgPackType.POSITIVE_FIXNUM_0x3B: return (ushort)59;
                case MsgPackType.POSITIVE_FIXNUM_0x3C: return (ushort)60;
                case MsgPackType.POSITIVE_FIXNUM_0x3D: return (ushort)61;
                case MsgPackType.POSITIVE_FIXNUM_0x3E: return (ushort)62;
                case MsgPackType.POSITIVE_FIXNUM_0x3F: return (ushort)63;

                case MsgPackType.POSITIVE_FIXNUM_0x40: return (ushort)64;
                case MsgPackType.POSITIVE_FIXNUM_0x41: return (ushort)65;
                case MsgPackType.POSITIVE_FIXNUM_0x42: return (ushort)66;
                case MsgPackType.POSITIVE_FIXNUM_0x43: return (ushort)67;
                case MsgPackType.POSITIVE_FIXNUM_0x44: return (ushort)68;
                case MsgPackType.POSITIVE_FIXNUM_0x45: return (ushort)69;
                case MsgPackType.POSITIVE_FIXNUM_0x46: return (ushort)70;
                case MsgPackType.POSITIVE_FIXNUM_0x47: return (ushort)71;
                case MsgPackType.POSITIVE_FIXNUM_0x48: return (ushort)72;
                case MsgPackType.POSITIVE_FIXNUM_0x49: return (ushort)73;
                case MsgPackType.POSITIVE_FIXNUM_0x4A: return (ushort)74; 
                case MsgPackType.POSITIVE_FIXNUM_0x4B: return (ushort)75;
                case MsgPackType.POSITIVE_FIXNUM_0x4C: return (ushort)76;
                case MsgPackType.POSITIVE_FIXNUM_0x4D: return (ushort)77;
                case MsgPackType.POSITIVE_FIXNUM_0x4E: return (ushort)78;
                case MsgPackType.POSITIVE_FIXNUM_0x4F: return (ushort)79;
                     
                case MsgPackType.POSITIVE_FIXNUM_0x50: return (ushort)80;
                case MsgPackType.POSITIVE_FIXNUM_0x51: return (ushort)81;
                case MsgPackType.POSITIVE_FIXNUM_0x52: return (ushort)82;
                case MsgPackType.POSITIVE_FIXNUM_0x53: return (ushort)83;
                case MsgPackType.POSITIVE_FIXNUM_0x54: return (ushort)84;
                case MsgPackType.POSITIVE_FIXNUM_0x55: return (ushort)85;
                case MsgPackType.POSITIVE_FIXNUM_0x56: return (ushort)86;
                case MsgPackType.POSITIVE_FIXNUM_0x57: return (ushort)87;
                case MsgPackType.POSITIVE_FIXNUM_0x58: return (ushort)88;
                case MsgPackType.POSITIVE_FIXNUM_0x59: return (ushort)89;
                case MsgPackType.POSITIVE_FIXNUM_0x5A: return (ushort)90;
                case MsgPackType.POSITIVE_FIXNUM_0x5B: return (ushort)91;
                case MsgPackType.POSITIVE_FIXNUM_0x5C: return (ushort)92;
                case MsgPackType.POSITIVE_FIXNUM_0x5D: return (ushort)93;
                case MsgPackType.POSITIVE_FIXNUM_0x5E: return (ushort)94;
                case MsgPackType.POSITIVE_FIXNUM_0x5F: return (ushort)95;

                case MsgPackType.POSITIVE_FIXNUM_0x60: return (ushort)96;
                case MsgPackType.POSITIVE_FIXNUM_0x61: return (ushort)97;
                case MsgPackType.POSITIVE_FIXNUM_0x62: return (ushort)98;
                case MsgPackType.POSITIVE_FIXNUM_0x63: return (ushort)99;
                case MsgPackType.POSITIVE_FIXNUM_0x64: return (ushort)100;
                case MsgPackType.POSITIVE_FIXNUM_0x65: return (ushort)101;
                case MsgPackType.POSITIVE_FIXNUM_0x66: return (ushort)102;
                case MsgPackType.POSITIVE_FIXNUM_0x67: return (ushort)103;
                case MsgPackType.POSITIVE_FIXNUM_0x68: return (ushort)104;
                case MsgPackType.POSITIVE_FIXNUM_0x69: return (ushort)105;
                case MsgPackType.POSITIVE_FIXNUM_0x6A: return (ushort)106;
                case MsgPackType.POSITIVE_FIXNUM_0x6B: return (ushort)107;
                case MsgPackType.POSITIVE_FIXNUM_0x6C: return (ushort)108;
                case MsgPackType.POSITIVE_FIXNUM_0x6D: return (ushort)109;
                case MsgPackType.POSITIVE_FIXNUM_0x6E: return (ushort)110;
                case MsgPackType.POSITIVE_FIXNUM_0x6F: return (ushort)111;

                case MsgPackType.POSITIVE_FIXNUM_0x70: return (ushort)112;
                case MsgPackType.POSITIVE_FIXNUM_0x71: return (ushort)113;
                case MsgPackType.POSITIVE_FIXNUM_0x72: return (ushort)114;
                case MsgPackType.POSITIVE_FIXNUM_0x73: return (ushort)115;
                case MsgPackType.POSITIVE_FIXNUM_0x74: return (ushort)116;
                case MsgPackType.POSITIVE_FIXNUM_0x75: return (ushort)117;
                case MsgPackType.POSITIVE_FIXNUM_0x76: return (ushort)118;
                case MsgPackType.POSITIVE_FIXNUM_0x77: return (ushort)119;
                case MsgPackType.POSITIVE_FIXNUM_0x78: return (ushort)120;
                case MsgPackType.POSITIVE_FIXNUM_0x79: return (ushort)121;
                case MsgPackType.POSITIVE_FIXNUM_0x7A: return (ushort)122;
                case MsgPackType.POSITIVE_FIXNUM_0x7B: return (ushort)123;
                case MsgPackType.POSITIVE_FIXNUM_0x7C: return (ushort)124;
                case MsgPackType.POSITIVE_FIXNUM_0x7D: return (ushort)125;
                case MsgPackType.POSITIVE_FIXNUM_0x7E: return (ushort)126;
                case MsgPackType.POSITIVE_FIXNUM_0x7F: return (ushort)127;

                case MsgPackType.NEGATIVE_FIXNUM: return (ushort)-32;
                case MsgPackType.NEGATIVE_FIXNUM_0x01: return (ushort)-1;
                case MsgPackType.NEGATIVE_FIXNUM_0x02: return (ushort)-2;
                case MsgPackType.NEGATIVE_FIXNUM_0x03: return (ushort)-3;
                case MsgPackType.NEGATIVE_FIXNUM_0x04: return (ushort)-4;
                case MsgPackType.NEGATIVE_FIXNUM_0x05: return (ushort)-5;
                case MsgPackType.NEGATIVE_FIXNUM_0x06: return (ushort)-6;
                case MsgPackType.NEGATIVE_FIXNUM_0x07: return (ushort)-7;
                case MsgPackType.NEGATIVE_FIXNUM_0x08: return (ushort)-8;
                case MsgPackType.NEGATIVE_FIXNUM_0x09: return (ushort)-9;
                case MsgPackType.NEGATIVE_FIXNUM_0x0A: return (ushort)-10;
                case MsgPackType.NEGATIVE_FIXNUM_0x0B: return (ushort)-11;
                case MsgPackType.NEGATIVE_FIXNUM_0x0C: return (ushort)-12;
                case MsgPackType.NEGATIVE_FIXNUM_0x0D: return (ushort)-13;
                case MsgPackType.NEGATIVE_FIXNUM_0x0E: return (ushort)-14;
                case MsgPackType.NEGATIVE_FIXNUM_0x0F: return (ushort)-15;
                case MsgPackType.NEGATIVE_FIXNUM_0x10: return (ushort)-16;
                case MsgPackType.NEGATIVE_FIXNUM_0x11: return (ushort)-17;
                case MsgPackType.NEGATIVE_FIXNUM_0x12: return (ushort)-18;
                case MsgPackType.NEGATIVE_FIXNUM_0x13: return (ushort)-19;
                case MsgPackType.NEGATIVE_FIXNUM_0x14: return (ushort)-20;
                case MsgPackType.NEGATIVE_FIXNUM_0x15: return (ushort)-21;
                case MsgPackType.NEGATIVE_FIXNUM_0x16: return (ushort)-22;
                case MsgPackType.NEGATIVE_FIXNUM_0x17: return (ushort)-23;
                case MsgPackType.NEGATIVE_FIXNUM_0x18: return (ushort)-24;
                case MsgPackType.NEGATIVE_FIXNUM_0x19: return (ushort)-25;
                case MsgPackType.NEGATIVE_FIXNUM_0x1A: return (ushort)-26;
                case MsgPackType.NEGATIVE_FIXNUM_0x1B: return (ushort)-27;
                case MsgPackType.NEGATIVE_FIXNUM_0x1C: return (ushort)-28;
                case MsgPackType.NEGATIVE_FIXNUM_0x1D: return (ushort)-29;
                case MsgPackType.NEGATIVE_FIXNUM_0x1E: return (ushort)-30;
                case MsgPackType.NEGATIVE_FIXNUM_0x1F: return (ushort)-31;

                case MsgPackType.INT8: return (ushort)(SByte)GetBody().Get(0);
                case MsgPackType.INT16: return (ushort)EndianConverter.NetworkByteWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT32: return (ushort)EndianConverter.NetworkByteDWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT64: return (ushort)EndianConverter.NetworkByteQWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.UINT8: return (ushort)GetBody().Get(0);
                case MsgPackType.UINT16: return (ushort)EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT32: return (ushort)EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT64: return (ushort)EndianConverter.NetworkByteQWordToUnsignedNativeByteOrder(GetBody());

                default: throw new MessagePackValueException("is not ushort" + Bytes);
            }
            }
        }

        public UInt32 GetUInt32()
        {
            unchecked{
            switch(FormatType)
            {
                case MsgPackType.POSITIVE_FIXNUM: return (uint)0;
                case MsgPackType.POSITIVE_FIXNUM_0x01: return (uint)1;
                case MsgPackType.POSITIVE_FIXNUM_0x02: return (uint)2;
                case MsgPackType.POSITIVE_FIXNUM_0x03: return (uint)3;
                case MsgPackType.POSITIVE_FIXNUM_0x04: return (uint)4;
                case MsgPackType.POSITIVE_FIXNUM_0x05: return (uint)5;
                case MsgPackType.POSITIVE_FIXNUM_0x06: return (uint)6;
                case MsgPackType.POSITIVE_FIXNUM_0x07: return (uint)7;
                case MsgPackType.POSITIVE_FIXNUM_0x08: return (uint)8;
                case MsgPackType.POSITIVE_FIXNUM_0x09: return (uint)9;
                case MsgPackType.POSITIVE_FIXNUM_0x0A: return (uint)10;
                case MsgPackType.POSITIVE_FIXNUM_0x0B: return (uint)11;
                case MsgPackType.POSITIVE_FIXNUM_0x0C: return (uint)12;
                case MsgPackType.POSITIVE_FIXNUM_0x0D: return (uint)13;
                case MsgPackType.POSITIVE_FIXNUM_0x0E: return (uint)14;
                case MsgPackType.POSITIVE_FIXNUM_0x0F: return (uint)15;

                case MsgPackType.POSITIVE_FIXNUM_0x10: return (uint)16;
                case MsgPackType.POSITIVE_FIXNUM_0x11: return (uint)17;
                case MsgPackType.POSITIVE_FIXNUM_0x12: return (uint)18;
                case MsgPackType.POSITIVE_FIXNUM_0x13: return (uint)19;
                case MsgPackType.POSITIVE_FIXNUM_0x14: return (uint)20;
                case MsgPackType.POSITIVE_FIXNUM_0x15: return (uint)21;
                case MsgPackType.POSITIVE_FIXNUM_0x16: return (uint)22;
                case MsgPackType.POSITIVE_FIXNUM_0x17: return (uint)23;
                case MsgPackType.POSITIVE_FIXNUM_0x18: return (uint)24;
                case MsgPackType.POSITIVE_FIXNUM_0x19: return (uint)25;
                case MsgPackType.POSITIVE_FIXNUM_0x1A: return (uint)26;
                case MsgPackType.POSITIVE_FIXNUM_0x1B: return (uint)27;
                case MsgPackType.POSITIVE_FIXNUM_0x1C: return (uint)28;
                case MsgPackType.POSITIVE_FIXNUM_0x1D: return (uint)29;
                case MsgPackType.POSITIVE_FIXNUM_0x1E: return (uint)30;
                case MsgPackType.POSITIVE_FIXNUM_0x1F: return (uint)31;

                case MsgPackType.POSITIVE_FIXNUM_0x20: return (uint)32;
                case MsgPackType.POSITIVE_FIXNUM_0x21: return (uint)33;
                case MsgPackType.POSITIVE_FIXNUM_0x22: return (uint)34;
                case MsgPackType.POSITIVE_FIXNUM_0x23: return (uint)35;
                case MsgPackType.POSITIVE_FIXNUM_0x24: return (uint)36;
                case MsgPackType.POSITIVE_FIXNUM_0x25: return (uint)37;
                case MsgPackType.POSITIVE_FIXNUM_0x26: return (uint)38;
                case MsgPackType.POSITIVE_FIXNUM_0x27: return (uint)39;
                case MsgPackType.POSITIVE_FIXNUM_0x28: return (uint)40;
                case MsgPackType.POSITIVE_FIXNUM_0x29: return (uint)41;
                case MsgPackType.POSITIVE_FIXNUM_0x2A: return (uint)42;
                case MsgPackType.POSITIVE_FIXNUM_0x2B: return (uint)43;
                case MsgPackType.POSITIVE_FIXNUM_0x2C: return (uint)44;
                case MsgPackType.POSITIVE_FIXNUM_0x2D: return (uint)45;
                case MsgPackType.POSITIVE_FIXNUM_0x2E: return (uint)46;
                case MsgPackType.POSITIVE_FIXNUM_0x2F: return (uint)47;

                case MsgPackType.POSITIVE_FIXNUM_0x30: return (uint)48;
                case MsgPackType.POSITIVE_FIXNUM_0x31: return (uint)49;
                case MsgPackType.POSITIVE_FIXNUM_0x32: return (uint)50;
                case MsgPackType.POSITIVE_FIXNUM_0x33: return (uint)51;
                case MsgPackType.POSITIVE_FIXNUM_0x34: return (uint)52;
                case MsgPackType.POSITIVE_FIXNUM_0x35: return (uint)53;
                case MsgPackType.POSITIVE_FIXNUM_0x36: return (uint)54;
                case MsgPackType.POSITIVE_FIXNUM_0x37: return (uint)55;
                case MsgPackType.POSITIVE_FIXNUM_0x38: return (uint)56;
                case MsgPackType.POSITIVE_FIXNUM_0x39: return (uint)57;
                case MsgPackType.POSITIVE_FIXNUM_0x3A: return (uint)58;
                case MsgPackType.POSITIVE_FIXNUM_0x3B: return (uint)59;
                case MsgPackType.POSITIVE_FIXNUM_0x3C: return (uint)60;
                case MsgPackType.POSITIVE_FIXNUM_0x3D: return (uint)61;
                case MsgPackType.POSITIVE_FIXNUM_0x3E: return (uint)62;
                case MsgPackType.POSITIVE_FIXNUM_0x3F: return (uint)63;

                case MsgPackType.POSITIVE_FIXNUM_0x40: return (uint)64;
                case MsgPackType.POSITIVE_FIXNUM_0x41: return (uint)65;
                case MsgPackType.POSITIVE_FIXNUM_0x42: return (uint)66;
                case MsgPackType.POSITIVE_FIXNUM_0x43: return (uint)67;
                case MsgPackType.POSITIVE_FIXNUM_0x44: return (uint)68;
                case MsgPackType.POSITIVE_FIXNUM_0x45: return (uint)69;
                case MsgPackType.POSITIVE_FIXNUM_0x46: return (uint)70;
                case MsgPackType.POSITIVE_FIXNUM_0x47: return (uint)71;
                case MsgPackType.POSITIVE_FIXNUM_0x48: return (uint)72;
                case MsgPackType.POSITIVE_FIXNUM_0x49: return (uint)73;
                case MsgPackType.POSITIVE_FIXNUM_0x4A: return (uint)74; 
                case MsgPackType.POSITIVE_FIXNUM_0x4B: return (uint)75;
                case MsgPackType.POSITIVE_FIXNUM_0x4C: return (uint)76;
                case MsgPackType.POSITIVE_FIXNUM_0x4D: return (uint)77;
                case MsgPackType.POSITIVE_FIXNUM_0x4E: return (uint)78;
                case MsgPackType.POSITIVE_FIXNUM_0x4F: return (uint)79;
                     
                case MsgPackType.POSITIVE_FIXNUM_0x50: return (uint)80;
                case MsgPackType.POSITIVE_FIXNUM_0x51: return (uint)81;
                case MsgPackType.POSITIVE_FIXNUM_0x52: return (uint)82;
                case MsgPackType.POSITIVE_FIXNUM_0x53: return (uint)83;
                case MsgPackType.POSITIVE_FIXNUM_0x54: return (uint)84;
                case MsgPackType.POSITIVE_FIXNUM_0x55: return (uint)85;
                case MsgPackType.POSITIVE_FIXNUM_0x56: return (uint)86;
                case MsgPackType.POSITIVE_FIXNUM_0x57: return (uint)87;
                case MsgPackType.POSITIVE_FIXNUM_0x58: return (uint)88;
                case MsgPackType.POSITIVE_FIXNUM_0x59: return (uint)89;
                case MsgPackType.POSITIVE_FIXNUM_0x5A: return (uint)90;
                case MsgPackType.POSITIVE_FIXNUM_0x5B: return (uint)91;
                case MsgPackType.POSITIVE_FIXNUM_0x5C: return (uint)92;
                case MsgPackType.POSITIVE_FIXNUM_0x5D: return (uint)93;
                case MsgPackType.POSITIVE_FIXNUM_0x5E: return (uint)94;
                case MsgPackType.POSITIVE_FIXNUM_0x5F: return (uint)95;

                case MsgPackType.POSITIVE_FIXNUM_0x60: return (uint)96;
                case MsgPackType.POSITIVE_FIXNUM_0x61: return (uint)97;
                case MsgPackType.POSITIVE_FIXNUM_0x62: return (uint)98;
                case MsgPackType.POSITIVE_FIXNUM_0x63: return (uint)99;
                case MsgPackType.POSITIVE_FIXNUM_0x64: return (uint)100;
                case MsgPackType.POSITIVE_FIXNUM_0x65: return (uint)101;
                case MsgPackType.POSITIVE_FIXNUM_0x66: return (uint)102;
                case MsgPackType.POSITIVE_FIXNUM_0x67: return (uint)103;
                case MsgPackType.POSITIVE_FIXNUM_0x68: return (uint)104;
                case MsgPackType.POSITIVE_FIXNUM_0x69: return (uint)105;
                case MsgPackType.POSITIVE_FIXNUM_0x6A: return (uint)106;
                case MsgPackType.POSITIVE_FIXNUM_0x6B: return (uint)107;
                case MsgPackType.POSITIVE_FIXNUM_0x6C: return (uint)108;
                case MsgPackType.POSITIVE_FIXNUM_0x6D: return (uint)109;
                case MsgPackType.POSITIVE_FIXNUM_0x6E: return (uint)110;
                case MsgPackType.POSITIVE_FIXNUM_0x6F: return (uint)111;

                case MsgPackType.POSITIVE_FIXNUM_0x70: return (uint)112;
                case MsgPackType.POSITIVE_FIXNUM_0x71: return (uint)113;
                case MsgPackType.POSITIVE_FIXNUM_0x72: return (uint)114;
                case MsgPackType.POSITIVE_FIXNUM_0x73: return (uint)115;
                case MsgPackType.POSITIVE_FIXNUM_0x74: return (uint)116;
                case MsgPackType.POSITIVE_FIXNUM_0x75: return (uint)117;
                case MsgPackType.POSITIVE_FIXNUM_0x76: return (uint)118;
                case MsgPackType.POSITIVE_FIXNUM_0x77: return (uint)119;
                case MsgPackType.POSITIVE_FIXNUM_0x78: return (uint)120;
                case MsgPackType.POSITIVE_FIXNUM_0x79: return (uint)121;
                case MsgPackType.POSITIVE_FIXNUM_0x7A: return (uint)122;
                case MsgPackType.POSITIVE_FIXNUM_0x7B: return (uint)123;
                case MsgPackType.POSITIVE_FIXNUM_0x7C: return (uint)124;
                case MsgPackType.POSITIVE_FIXNUM_0x7D: return (uint)125;
                case MsgPackType.POSITIVE_FIXNUM_0x7E: return (uint)126;
                case MsgPackType.POSITIVE_FIXNUM_0x7F: return (uint)127;

                case MsgPackType.NEGATIVE_FIXNUM: return (uint)-32;
                case MsgPackType.NEGATIVE_FIXNUM_0x01: return (uint)-1;
                case MsgPackType.NEGATIVE_FIXNUM_0x02: return (uint)-2;
                case MsgPackType.NEGATIVE_FIXNUM_0x03: return (uint)-3;
                case MsgPackType.NEGATIVE_FIXNUM_0x04: return (uint)-4;
                case MsgPackType.NEGATIVE_FIXNUM_0x05: return (uint)-5;
                case MsgPackType.NEGATIVE_FIXNUM_0x06: return (uint)-6;
                case MsgPackType.NEGATIVE_FIXNUM_0x07: return (uint)-7;
                case MsgPackType.NEGATIVE_FIXNUM_0x08: return (uint)-8;
                case MsgPackType.NEGATIVE_FIXNUM_0x09: return (uint)-9;
                case MsgPackType.NEGATIVE_FIXNUM_0x0A: return (uint)-10;
                case MsgPackType.NEGATIVE_FIXNUM_0x0B: return (uint)-11;
                case MsgPackType.NEGATIVE_FIXNUM_0x0C: return (uint)-12;
                case MsgPackType.NEGATIVE_FIXNUM_0x0D: return (uint)-13;
                case MsgPackType.NEGATIVE_FIXNUM_0x0E: return (uint)-14;
                case MsgPackType.NEGATIVE_FIXNUM_0x0F: return (uint)-15;
                case MsgPackType.NEGATIVE_FIXNUM_0x10: return (uint)-16;
                case MsgPackType.NEGATIVE_FIXNUM_0x11: return (uint)-17;
                case MsgPackType.NEGATIVE_FIXNUM_0x12: return (uint)-18;
                case MsgPackType.NEGATIVE_FIXNUM_0x13: return (uint)-19;
                case MsgPackType.NEGATIVE_FIXNUM_0x14: return (uint)-20;
                case MsgPackType.NEGATIVE_FIXNUM_0x15: return (uint)-21;
                case MsgPackType.NEGATIVE_FIXNUM_0x16: return (uint)-22;
                case MsgPackType.NEGATIVE_FIXNUM_0x17: return (uint)-23;
                case MsgPackType.NEGATIVE_FIXNUM_0x18: return (uint)-24;
                case MsgPackType.NEGATIVE_FIXNUM_0x19: return (uint)-25;
                case MsgPackType.NEGATIVE_FIXNUM_0x1A: return (uint)-26;
                case MsgPackType.NEGATIVE_FIXNUM_0x1B: return (uint)-27;
                case MsgPackType.NEGATIVE_FIXNUM_0x1C: return (uint)-28;
                case MsgPackType.NEGATIVE_FIXNUM_0x1D: return (uint)-29;
                case MsgPackType.NEGATIVE_FIXNUM_0x1E: return (uint)-30;
                case MsgPackType.NEGATIVE_FIXNUM_0x1F: return (uint)-31;

                case MsgPackType.INT8: return (uint)(SByte)GetBody().Get(0);
                case MsgPackType.INT16: return (uint)EndianConverter.NetworkByteWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT32: return (uint)EndianConverter.NetworkByteDWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT64: return (uint)EndianConverter.NetworkByteQWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.UINT8: return (uint)GetBody().Get(0);
                case MsgPackType.UINT16: return (uint)EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT32: return (uint)EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT64: return (uint)EndianConverter.NetworkByteQWordToUnsignedNativeByteOrder(GetBody());

                default: throw new MessagePackValueException("is not uint" + Bytes);
            }
            }
        }

        public UInt64 GetUInt64()
        {
            unchecked{
            switch(FormatType)
            {
                case MsgPackType.POSITIVE_FIXNUM: return (ulong)0;
                case MsgPackType.POSITIVE_FIXNUM_0x01: return (ulong)1;
                case MsgPackType.POSITIVE_FIXNUM_0x02: return (ulong)2;
                case MsgPackType.POSITIVE_FIXNUM_0x03: return (ulong)3;
                case MsgPackType.POSITIVE_FIXNUM_0x04: return (ulong)4;
                case MsgPackType.POSITIVE_FIXNUM_0x05: return (ulong)5;
                case MsgPackType.POSITIVE_FIXNUM_0x06: return (ulong)6;
                case MsgPackType.POSITIVE_FIXNUM_0x07: return (ulong)7;
                case MsgPackType.POSITIVE_FIXNUM_0x08: return (ulong)8;
                case MsgPackType.POSITIVE_FIXNUM_0x09: return (ulong)9;
                case MsgPackType.POSITIVE_FIXNUM_0x0A: return (ulong)10;
                case MsgPackType.POSITIVE_FIXNUM_0x0B: return (ulong)11;
                case MsgPackType.POSITIVE_FIXNUM_0x0C: return (ulong)12;
                case MsgPackType.POSITIVE_FIXNUM_0x0D: return (ulong)13;
                case MsgPackType.POSITIVE_FIXNUM_0x0E: return (ulong)14;
                case MsgPackType.POSITIVE_FIXNUM_0x0F: return (ulong)15;

                case MsgPackType.POSITIVE_FIXNUM_0x10: return (ulong)16;
                case MsgPackType.POSITIVE_FIXNUM_0x11: return (ulong)17;
                case MsgPackType.POSITIVE_FIXNUM_0x12: return (ulong)18;
                case MsgPackType.POSITIVE_FIXNUM_0x13: return (ulong)19;
                case MsgPackType.POSITIVE_FIXNUM_0x14: return (ulong)20;
                case MsgPackType.POSITIVE_FIXNUM_0x15: return (ulong)21;
                case MsgPackType.POSITIVE_FIXNUM_0x16: return (ulong)22;
                case MsgPackType.POSITIVE_FIXNUM_0x17: return (ulong)23;
                case MsgPackType.POSITIVE_FIXNUM_0x18: return (ulong)24;
                case MsgPackType.POSITIVE_FIXNUM_0x19: return (ulong)25;
                case MsgPackType.POSITIVE_FIXNUM_0x1A: return (ulong)26;
                case MsgPackType.POSITIVE_FIXNUM_0x1B: return (ulong)27;
                case MsgPackType.POSITIVE_FIXNUM_0x1C: return (ulong)28;
                case MsgPackType.POSITIVE_FIXNUM_0x1D: return (ulong)29;
                case MsgPackType.POSITIVE_FIXNUM_0x1E: return (ulong)30;
                case MsgPackType.POSITIVE_FIXNUM_0x1F: return (ulong)31;

                case MsgPackType.POSITIVE_FIXNUM_0x20: return (ulong)32;
                case MsgPackType.POSITIVE_FIXNUM_0x21: return (ulong)33;
                case MsgPackType.POSITIVE_FIXNUM_0x22: return (ulong)34;
                case MsgPackType.POSITIVE_FIXNUM_0x23: return (ulong)35;
                case MsgPackType.POSITIVE_FIXNUM_0x24: return (ulong)36;
                case MsgPackType.POSITIVE_FIXNUM_0x25: return (ulong)37;
                case MsgPackType.POSITIVE_FIXNUM_0x26: return (ulong)38;
                case MsgPackType.POSITIVE_FIXNUM_0x27: return (ulong)39;
                case MsgPackType.POSITIVE_FIXNUM_0x28: return (ulong)40;
                case MsgPackType.POSITIVE_FIXNUM_0x29: return (ulong)41;
                case MsgPackType.POSITIVE_FIXNUM_0x2A: return (ulong)42;
                case MsgPackType.POSITIVE_FIXNUM_0x2B: return (ulong)43;
                case MsgPackType.POSITIVE_FIXNUM_0x2C: return (ulong)44;
                case MsgPackType.POSITIVE_FIXNUM_0x2D: return (ulong)45;
                case MsgPackType.POSITIVE_FIXNUM_0x2E: return (ulong)46;
                case MsgPackType.POSITIVE_FIXNUM_0x2F: return (ulong)47;

                case MsgPackType.POSITIVE_FIXNUM_0x30: return (ulong)48;
                case MsgPackType.POSITIVE_FIXNUM_0x31: return (ulong)49;
                case MsgPackType.POSITIVE_FIXNUM_0x32: return (ulong)50;
                case MsgPackType.POSITIVE_FIXNUM_0x33: return (ulong)51;
                case MsgPackType.POSITIVE_FIXNUM_0x34: return (ulong)52;
                case MsgPackType.POSITIVE_FIXNUM_0x35: return (ulong)53;
                case MsgPackType.POSITIVE_FIXNUM_0x36: return (ulong)54;
                case MsgPackType.POSITIVE_FIXNUM_0x37: return (ulong)55;
                case MsgPackType.POSITIVE_FIXNUM_0x38: return (ulong)56;
                case MsgPackType.POSITIVE_FIXNUM_0x39: return (ulong)57;
                case MsgPackType.POSITIVE_FIXNUM_0x3A: return (ulong)58;
                case MsgPackType.POSITIVE_FIXNUM_0x3B: return (ulong)59;
                case MsgPackType.POSITIVE_FIXNUM_0x3C: return (ulong)60;
                case MsgPackType.POSITIVE_FIXNUM_0x3D: return (ulong)61;
                case MsgPackType.POSITIVE_FIXNUM_0x3E: return (ulong)62;
                case MsgPackType.POSITIVE_FIXNUM_0x3F: return (ulong)63;

                case MsgPackType.POSITIVE_FIXNUM_0x40: return (ulong)64;
                case MsgPackType.POSITIVE_FIXNUM_0x41: return (ulong)65;
                case MsgPackType.POSITIVE_FIXNUM_0x42: return (ulong)66;
                case MsgPackType.POSITIVE_FIXNUM_0x43: return (ulong)67;
                case MsgPackType.POSITIVE_FIXNUM_0x44: return (ulong)68;
                case MsgPackType.POSITIVE_FIXNUM_0x45: return (ulong)69;
                case MsgPackType.POSITIVE_FIXNUM_0x46: return (ulong)70;
                case MsgPackType.POSITIVE_FIXNUM_0x47: return (ulong)71;
                case MsgPackType.POSITIVE_FIXNUM_0x48: return (ulong)72;
                case MsgPackType.POSITIVE_FIXNUM_0x49: return (ulong)73;
                case MsgPackType.POSITIVE_FIXNUM_0x4A: return (ulong)74; 
                case MsgPackType.POSITIVE_FIXNUM_0x4B: return (ulong)75;
                case MsgPackType.POSITIVE_FIXNUM_0x4C: return (ulong)76;
                case MsgPackType.POSITIVE_FIXNUM_0x4D: return (ulong)77;
                case MsgPackType.POSITIVE_FIXNUM_0x4E: return (ulong)78;
                case MsgPackType.POSITIVE_FIXNUM_0x4F: return (ulong)79;
                     
                case MsgPackType.POSITIVE_FIXNUM_0x50: return (ulong)80;
                case MsgPackType.POSITIVE_FIXNUM_0x51: return (ulong)81;
                case MsgPackType.POSITIVE_FIXNUM_0x52: return (ulong)82;
                case MsgPackType.POSITIVE_FIXNUM_0x53: return (ulong)83;
                case MsgPackType.POSITIVE_FIXNUM_0x54: return (ulong)84;
                case MsgPackType.POSITIVE_FIXNUM_0x55: return (ulong)85;
                case MsgPackType.POSITIVE_FIXNUM_0x56: return (ulong)86;
                case MsgPackType.POSITIVE_FIXNUM_0x57: return (ulong)87;
                case MsgPackType.POSITIVE_FIXNUM_0x58: return (ulong)88;
                case MsgPackType.POSITIVE_FIXNUM_0x59: return (ulong)89;
                case MsgPackType.POSITIVE_FIXNUM_0x5A: return (ulong)90;
                case MsgPackType.POSITIVE_FIXNUM_0x5B: return (ulong)91;
                case MsgPackType.POSITIVE_FIXNUM_0x5C: return (ulong)92;
                case MsgPackType.POSITIVE_FIXNUM_0x5D: return (ulong)93;
                case MsgPackType.POSITIVE_FIXNUM_0x5E: return (ulong)94;
                case MsgPackType.POSITIVE_FIXNUM_0x5F: return (ulong)95;

                case MsgPackType.POSITIVE_FIXNUM_0x60: return (ulong)96;
                case MsgPackType.POSITIVE_FIXNUM_0x61: return (ulong)97;
                case MsgPackType.POSITIVE_FIXNUM_0x62: return (ulong)98;
                case MsgPackType.POSITIVE_FIXNUM_0x63: return (ulong)99;
                case MsgPackType.POSITIVE_FIXNUM_0x64: return (ulong)100;
                case MsgPackType.POSITIVE_FIXNUM_0x65: return (ulong)101;
                case MsgPackType.POSITIVE_FIXNUM_0x66: return (ulong)102;
                case MsgPackType.POSITIVE_FIXNUM_0x67: return (ulong)103;
                case MsgPackType.POSITIVE_FIXNUM_0x68: return (ulong)104;
                case MsgPackType.POSITIVE_FIXNUM_0x69: return (ulong)105;
                case MsgPackType.POSITIVE_FIXNUM_0x6A: return (ulong)106;
                case MsgPackType.POSITIVE_FIXNUM_0x6B: return (ulong)107;
                case MsgPackType.POSITIVE_FIXNUM_0x6C: return (ulong)108;
                case MsgPackType.POSITIVE_FIXNUM_0x6D: return (ulong)109;
                case MsgPackType.POSITIVE_FIXNUM_0x6E: return (ulong)110;
                case MsgPackType.POSITIVE_FIXNUM_0x6F: return (ulong)111;

                case MsgPackType.POSITIVE_FIXNUM_0x70: return (ulong)112;
                case MsgPackType.POSITIVE_FIXNUM_0x71: return (ulong)113;
                case MsgPackType.POSITIVE_FIXNUM_0x72: return (ulong)114;
                case MsgPackType.POSITIVE_FIXNUM_0x73: return (ulong)115;
                case MsgPackType.POSITIVE_FIXNUM_0x74: return (ulong)116;
                case MsgPackType.POSITIVE_FIXNUM_0x75: return (ulong)117;
                case MsgPackType.POSITIVE_FIXNUM_0x76: return (ulong)118;
                case MsgPackType.POSITIVE_FIXNUM_0x77: return (ulong)119;
                case MsgPackType.POSITIVE_FIXNUM_0x78: return (ulong)120;
                case MsgPackType.POSITIVE_FIXNUM_0x79: return (ulong)121;
                case MsgPackType.POSITIVE_FIXNUM_0x7A: return (ulong)122;
                case MsgPackType.POSITIVE_FIXNUM_0x7B: return (ulong)123;
                case MsgPackType.POSITIVE_FIXNUM_0x7C: return (ulong)124;
                case MsgPackType.POSITIVE_FIXNUM_0x7D: return (ulong)125;
                case MsgPackType.POSITIVE_FIXNUM_0x7E: return (ulong)126;
                case MsgPackType.POSITIVE_FIXNUM_0x7F: return (ulong)127;

                case MsgPackType.NEGATIVE_FIXNUM: return (ulong)-32;
                case MsgPackType.NEGATIVE_FIXNUM_0x01: return (ulong)-1;
                case MsgPackType.NEGATIVE_FIXNUM_0x02: return (ulong)-2;
                case MsgPackType.NEGATIVE_FIXNUM_0x03: return (ulong)-3;
                case MsgPackType.NEGATIVE_FIXNUM_0x04: return (ulong)-4;
                case MsgPackType.NEGATIVE_FIXNUM_0x05: return (ulong)-5;
                case MsgPackType.NEGATIVE_FIXNUM_0x06: return (ulong)-6;
                case MsgPackType.NEGATIVE_FIXNUM_0x07: return (ulong)-7;
                case MsgPackType.NEGATIVE_FIXNUM_0x08: return (ulong)-8;
                case MsgPackType.NEGATIVE_FIXNUM_0x09: return (ulong)-9;
                case MsgPackType.NEGATIVE_FIXNUM_0x0A: return (ulong)-10;
                case MsgPackType.NEGATIVE_FIXNUM_0x0B: return (ulong)-11;
                case MsgPackType.NEGATIVE_FIXNUM_0x0C: return (ulong)-12;
                case MsgPackType.NEGATIVE_FIXNUM_0x0D: return (ulong)-13;
                case MsgPackType.NEGATIVE_FIXNUM_0x0E: return (ulong)-14;
                case MsgPackType.NEGATIVE_FIXNUM_0x0F: return (ulong)-15;
                case MsgPackType.NEGATIVE_FIXNUM_0x10: return (ulong)-16;
                case MsgPackType.NEGATIVE_FIXNUM_0x11: return (ulong)-17;
                case MsgPackType.NEGATIVE_FIXNUM_0x12: return (ulong)-18;
                case MsgPackType.NEGATIVE_FIXNUM_0x13: return (ulong)-19;
                case MsgPackType.NEGATIVE_FIXNUM_0x14: return (ulong)-20;
                case MsgPackType.NEGATIVE_FIXNUM_0x15: return (ulong)-21;
                case MsgPackType.NEGATIVE_FIXNUM_0x16: return (ulong)-22;
                case MsgPackType.NEGATIVE_FIXNUM_0x17: return (ulong)-23;
                case MsgPackType.NEGATIVE_FIXNUM_0x18: return (ulong)-24;
                case MsgPackType.NEGATIVE_FIXNUM_0x19: return (ulong)-25;
                case MsgPackType.NEGATIVE_FIXNUM_0x1A: return (ulong)-26;
                case MsgPackType.NEGATIVE_FIXNUM_0x1B: return (ulong)-27;
                case MsgPackType.NEGATIVE_FIXNUM_0x1C: return (ulong)-28;
                case MsgPackType.NEGATIVE_FIXNUM_0x1D: return (ulong)-29;
                case MsgPackType.NEGATIVE_FIXNUM_0x1E: return (ulong)-30;
                case MsgPackType.NEGATIVE_FIXNUM_0x1F: return (ulong)-31;

                case MsgPackType.INT8: return (ulong)(SByte)GetBody().Get(0);
                case MsgPackType.INT16: return (ulong)EndianConverter.NetworkByteWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT32: return (ulong)EndianConverter.NetworkByteDWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT64: return (ulong)EndianConverter.NetworkByteQWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.UINT8: return (ulong)GetBody().Get(0);
                case MsgPackType.UINT16: return (ulong)EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT32: return (ulong)EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT64: return (ulong)EndianConverter.NetworkByteQWordToUnsignedNativeByteOrder(GetBody());

                default: throw new MessagePackValueException("is not ulong" + Bytes);
            }
            }
        }

        public SByte GetSByte()
        {
            unchecked{
            switch(FormatType)
            {
                case MsgPackType.POSITIVE_FIXNUM: return (sbyte)0;
                case MsgPackType.POSITIVE_FIXNUM_0x01: return (sbyte)1;
                case MsgPackType.POSITIVE_FIXNUM_0x02: return (sbyte)2;
                case MsgPackType.POSITIVE_FIXNUM_0x03: return (sbyte)3;
                case MsgPackType.POSITIVE_FIXNUM_0x04: return (sbyte)4;
                case MsgPackType.POSITIVE_FIXNUM_0x05: return (sbyte)5;
                case MsgPackType.POSITIVE_FIXNUM_0x06: return (sbyte)6;
                case MsgPackType.POSITIVE_FIXNUM_0x07: return (sbyte)7;
                case MsgPackType.POSITIVE_FIXNUM_0x08: return (sbyte)8;
                case MsgPackType.POSITIVE_FIXNUM_0x09: return (sbyte)9;
                case MsgPackType.POSITIVE_FIXNUM_0x0A: return (sbyte)10;
                case MsgPackType.POSITIVE_FIXNUM_0x0B: return (sbyte)11;
                case MsgPackType.POSITIVE_FIXNUM_0x0C: return (sbyte)12;
                case MsgPackType.POSITIVE_FIXNUM_0x0D: return (sbyte)13;
                case MsgPackType.POSITIVE_FIXNUM_0x0E: return (sbyte)14;
                case MsgPackType.POSITIVE_FIXNUM_0x0F: return (sbyte)15;

                case MsgPackType.POSITIVE_FIXNUM_0x10: return (sbyte)16;
                case MsgPackType.POSITIVE_FIXNUM_0x11: return (sbyte)17;
                case MsgPackType.POSITIVE_FIXNUM_0x12: return (sbyte)18;
                case MsgPackType.POSITIVE_FIXNUM_0x13: return (sbyte)19;
                case MsgPackType.POSITIVE_FIXNUM_0x14: return (sbyte)20;
                case MsgPackType.POSITIVE_FIXNUM_0x15: return (sbyte)21;
                case MsgPackType.POSITIVE_FIXNUM_0x16: return (sbyte)22;
                case MsgPackType.POSITIVE_FIXNUM_0x17: return (sbyte)23;
                case MsgPackType.POSITIVE_FIXNUM_0x18: return (sbyte)24;
                case MsgPackType.POSITIVE_FIXNUM_0x19: return (sbyte)25;
                case MsgPackType.POSITIVE_FIXNUM_0x1A: return (sbyte)26;
                case MsgPackType.POSITIVE_FIXNUM_0x1B: return (sbyte)27;
                case MsgPackType.POSITIVE_FIXNUM_0x1C: return (sbyte)28;
                case MsgPackType.POSITIVE_FIXNUM_0x1D: return (sbyte)29;
                case MsgPackType.POSITIVE_FIXNUM_0x1E: return (sbyte)30;
                case MsgPackType.POSITIVE_FIXNUM_0x1F: return (sbyte)31;

                case MsgPackType.POSITIVE_FIXNUM_0x20: return (sbyte)32;
                case MsgPackType.POSITIVE_FIXNUM_0x21: return (sbyte)33;
                case MsgPackType.POSITIVE_FIXNUM_0x22: return (sbyte)34;
                case MsgPackType.POSITIVE_FIXNUM_0x23: return (sbyte)35;
                case MsgPackType.POSITIVE_FIXNUM_0x24: return (sbyte)36;
                case MsgPackType.POSITIVE_FIXNUM_0x25: return (sbyte)37;
                case MsgPackType.POSITIVE_FIXNUM_0x26: return (sbyte)38;
                case MsgPackType.POSITIVE_FIXNUM_0x27: return (sbyte)39;
                case MsgPackType.POSITIVE_FIXNUM_0x28: return (sbyte)40;
                case MsgPackType.POSITIVE_FIXNUM_0x29: return (sbyte)41;
                case MsgPackType.POSITIVE_FIXNUM_0x2A: return (sbyte)42;
                case MsgPackType.POSITIVE_FIXNUM_0x2B: return (sbyte)43;
                case MsgPackType.POSITIVE_FIXNUM_0x2C: return (sbyte)44;
                case MsgPackType.POSITIVE_FIXNUM_0x2D: return (sbyte)45;
                case MsgPackType.POSITIVE_FIXNUM_0x2E: return (sbyte)46;
                case MsgPackType.POSITIVE_FIXNUM_0x2F: return (sbyte)47;

                case MsgPackType.POSITIVE_FIXNUM_0x30: return (sbyte)48;
                case MsgPackType.POSITIVE_FIXNUM_0x31: return (sbyte)49;
                case MsgPackType.POSITIVE_FIXNUM_0x32: return (sbyte)50;
                case MsgPackType.POSITIVE_FIXNUM_0x33: return (sbyte)51;
                case MsgPackType.POSITIVE_FIXNUM_0x34: return (sbyte)52;
                case MsgPackType.POSITIVE_FIXNUM_0x35: return (sbyte)53;
                case MsgPackType.POSITIVE_FIXNUM_0x36: return (sbyte)54;
                case MsgPackType.POSITIVE_FIXNUM_0x37: return (sbyte)55;
                case MsgPackType.POSITIVE_FIXNUM_0x38: return (sbyte)56;
                case MsgPackType.POSITIVE_FIXNUM_0x39: return (sbyte)57;
                case MsgPackType.POSITIVE_FIXNUM_0x3A: return (sbyte)58;
                case MsgPackType.POSITIVE_FIXNUM_0x3B: return (sbyte)59;
                case MsgPackType.POSITIVE_FIXNUM_0x3C: return (sbyte)60;
                case MsgPackType.POSITIVE_FIXNUM_0x3D: return (sbyte)61;
                case MsgPackType.POSITIVE_FIXNUM_0x3E: return (sbyte)62;
                case MsgPackType.POSITIVE_FIXNUM_0x3F: return (sbyte)63;

                case MsgPackType.POSITIVE_FIXNUM_0x40: return (sbyte)64;
                case MsgPackType.POSITIVE_FIXNUM_0x41: return (sbyte)65;
                case MsgPackType.POSITIVE_FIXNUM_0x42: return (sbyte)66;
                case MsgPackType.POSITIVE_FIXNUM_0x43: return (sbyte)67;
                case MsgPackType.POSITIVE_FIXNUM_0x44: return (sbyte)68;
                case MsgPackType.POSITIVE_FIXNUM_0x45: return (sbyte)69;
                case MsgPackType.POSITIVE_FIXNUM_0x46: return (sbyte)70;
                case MsgPackType.POSITIVE_FIXNUM_0x47: return (sbyte)71;
                case MsgPackType.POSITIVE_FIXNUM_0x48: return (sbyte)72;
                case MsgPackType.POSITIVE_FIXNUM_0x49: return (sbyte)73;
                case MsgPackType.POSITIVE_FIXNUM_0x4A: return (sbyte)74; 
                case MsgPackType.POSITIVE_FIXNUM_0x4B: return (sbyte)75;
                case MsgPackType.POSITIVE_FIXNUM_0x4C: return (sbyte)76;
                case MsgPackType.POSITIVE_FIXNUM_0x4D: return (sbyte)77;
                case MsgPackType.POSITIVE_FIXNUM_0x4E: return (sbyte)78;
                case MsgPackType.POSITIVE_FIXNUM_0x4F: return (sbyte)79;
                     
                case MsgPackType.POSITIVE_FIXNUM_0x50: return (sbyte)80;
                case MsgPackType.POSITIVE_FIXNUM_0x51: return (sbyte)81;
                case MsgPackType.POSITIVE_FIXNUM_0x52: return (sbyte)82;
                case MsgPackType.POSITIVE_FIXNUM_0x53: return (sbyte)83;
                case MsgPackType.POSITIVE_FIXNUM_0x54: return (sbyte)84;
                case MsgPackType.POSITIVE_FIXNUM_0x55: return (sbyte)85;
                case MsgPackType.POSITIVE_FIXNUM_0x56: return (sbyte)86;
                case MsgPackType.POSITIVE_FIXNUM_0x57: return (sbyte)87;
                case MsgPackType.POSITIVE_FIXNUM_0x58: return (sbyte)88;
                case MsgPackType.POSITIVE_FIXNUM_0x59: return (sbyte)89;
                case MsgPackType.POSITIVE_FIXNUM_0x5A: return (sbyte)90;
                case MsgPackType.POSITIVE_FIXNUM_0x5B: return (sbyte)91;
                case MsgPackType.POSITIVE_FIXNUM_0x5C: return (sbyte)92;
                case MsgPackType.POSITIVE_FIXNUM_0x5D: return (sbyte)93;
                case MsgPackType.POSITIVE_FIXNUM_0x5E: return (sbyte)94;
                case MsgPackType.POSITIVE_FIXNUM_0x5F: return (sbyte)95;

                case MsgPackType.POSITIVE_FIXNUM_0x60: return (sbyte)96;
                case MsgPackType.POSITIVE_FIXNUM_0x61: return (sbyte)97;
                case MsgPackType.POSITIVE_FIXNUM_0x62: return (sbyte)98;
                case MsgPackType.POSITIVE_FIXNUM_0x63: return (sbyte)99;
                case MsgPackType.POSITIVE_FIXNUM_0x64: return (sbyte)100;
                case MsgPackType.POSITIVE_FIXNUM_0x65: return (sbyte)101;
                case MsgPackType.POSITIVE_FIXNUM_0x66: return (sbyte)102;
                case MsgPackType.POSITIVE_FIXNUM_0x67: return (sbyte)103;
                case MsgPackType.POSITIVE_FIXNUM_0x68: return (sbyte)104;
                case MsgPackType.POSITIVE_FIXNUM_0x69: return (sbyte)105;
                case MsgPackType.POSITIVE_FIXNUM_0x6A: return (sbyte)106;
                case MsgPackType.POSITIVE_FIXNUM_0x6B: return (sbyte)107;
                case MsgPackType.POSITIVE_FIXNUM_0x6C: return (sbyte)108;
                case MsgPackType.POSITIVE_FIXNUM_0x6D: return (sbyte)109;
                case MsgPackType.POSITIVE_FIXNUM_0x6E: return (sbyte)110;
                case MsgPackType.POSITIVE_FIXNUM_0x6F: return (sbyte)111;

                case MsgPackType.POSITIVE_FIXNUM_0x70: return (sbyte)112;
                case MsgPackType.POSITIVE_FIXNUM_0x71: return (sbyte)113;
                case MsgPackType.POSITIVE_FIXNUM_0x72: return (sbyte)114;
                case MsgPackType.POSITIVE_FIXNUM_0x73: return (sbyte)115;
                case MsgPackType.POSITIVE_FIXNUM_0x74: return (sbyte)116;
                case MsgPackType.POSITIVE_FIXNUM_0x75: return (sbyte)117;
                case MsgPackType.POSITIVE_FIXNUM_0x76: return (sbyte)118;
                case MsgPackType.POSITIVE_FIXNUM_0x77: return (sbyte)119;
                case MsgPackType.POSITIVE_FIXNUM_0x78: return (sbyte)120;
                case MsgPackType.POSITIVE_FIXNUM_0x79: return (sbyte)121;
                case MsgPackType.POSITIVE_FIXNUM_0x7A: return (sbyte)122;
                case MsgPackType.POSITIVE_FIXNUM_0x7B: return (sbyte)123;
                case MsgPackType.POSITIVE_FIXNUM_0x7C: return (sbyte)124;
                case MsgPackType.POSITIVE_FIXNUM_0x7D: return (sbyte)125;
                case MsgPackType.POSITIVE_FIXNUM_0x7E: return (sbyte)126;
                case MsgPackType.POSITIVE_FIXNUM_0x7F: return (sbyte)127;

                case MsgPackType.NEGATIVE_FIXNUM: return (sbyte)-32;
                case MsgPackType.NEGATIVE_FIXNUM_0x01: return (sbyte)-1;
                case MsgPackType.NEGATIVE_FIXNUM_0x02: return (sbyte)-2;
                case MsgPackType.NEGATIVE_FIXNUM_0x03: return (sbyte)-3;
                case MsgPackType.NEGATIVE_FIXNUM_0x04: return (sbyte)-4;
                case MsgPackType.NEGATIVE_FIXNUM_0x05: return (sbyte)-5;
                case MsgPackType.NEGATIVE_FIXNUM_0x06: return (sbyte)-6;
                case MsgPackType.NEGATIVE_FIXNUM_0x07: return (sbyte)-7;
                case MsgPackType.NEGATIVE_FIXNUM_0x08: return (sbyte)-8;
                case MsgPackType.NEGATIVE_FIXNUM_0x09: return (sbyte)-9;
                case MsgPackType.NEGATIVE_FIXNUM_0x0A: return (sbyte)-10;
                case MsgPackType.NEGATIVE_FIXNUM_0x0B: return (sbyte)-11;
                case MsgPackType.NEGATIVE_FIXNUM_0x0C: return (sbyte)-12;
                case MsgPackType.NEGATIVE_FIXNUM_0x0D: return (sbyte)-13;
                case MsgPackType.NEGATIVE_FIXNUM_0x0E: return (sbyte)-14;
                case MsgPackType.NEGATIVE_FIXNUM_0x0F: return (sbyte)-15;
                case MsgPackType.NEGATIVE_FIXNUM_0x10: return (sbyte)-16;
                case MsgPackType.NEGATIVE_FIXNUM_0x11: return (sbyte)-17;
                case MsgPackType.NEGATIVE_FIXNUM_0x12: return (sbyte)-18;
                case MsgPackType.NEGATIVE_FIXNUM_0x13: return (sbyte)-19;
                case MsgPackType.NEGATIVE_FIXNUM_0x14: return (sbyte)-20;
                case MsgPackType.NEGATIVE_FIXNUM_0x15: return (sbyte)-21;
                case MsgPackType.NEGATIVE_FIXNUM_0x16: return (sbyte)-22;
                case MsgPackType.NEGATIVE_FIXNUM_0x17: return (sbyte)-23;
                case MsgPackType.NEGATIVE_FIXNUM_0x18: return (sbyte)-24;
                case MsgPackType.NEGATIVE_FIXNUM_0x19: return (sbyte)-25;
                case MsgPackType.NEGATIVE_FIXNUM_0x1A: return (sbyte)-26;
                case MsgPackType.NEGATIVE_FIXNUM_0x1B: return (sbyte)-27;
                case MsgPackType.NEGATIVE_FIXNUM_0x1C: return (sbyte)-28;
                case MsgPackType.NEGATIVE_FIXNUM_0x1D: return (sbyte)-29;
                case MsgPackType.NEGATIVE_FIXNUM_0x1E: return (sbyte)-30;
                case MsgPackType.NEGATIVE_FIXNUM_0x1F: return (sbyte)-31;

                case MsgPackType.INT8: return (sbyte)(SByte)GetBody().Get(0);
                case MsgPackType.INT16: return (sbyte)EndianConverter.NetworkByteWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT32: return (sbyte)EndianConverter.NetworkByteDWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT64: return (sbyte)EndianConverter.NetworkByteQWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.UINT8: return (sbyte)GetBody().Get(0);
                case MsgPackType.UINT16: return (sbyte)EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT32: return (sbyte)EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT64: return (sbyte)EndianConverter.NetworkByteQWordToUnsignedNativeByteOrder(GetBody());

                default: throw new MessagePackValueException("is not sbyte" + Bytes);
            }
            }
        }

        public Int16 GetInt16()
        {
            unchecked{
            switch(FormatType)
            {
                case MsgPackType.POSITIVE_FIXNUM: return (short)0;
                case MsgPackType.POSITIVE_FIXNUM_0x01: return (short)1;
                case MsgPackType.POSITIVE_FIXNUM_0x02: return (short)2;
                case MsgPackType.POSITIVE_FIXNUM_0x03: return (short)3;
                case MsgPackType.POSITIVE_FIXNUM_0x04: return (short)4;
                case MsgPackType.POSITIVE_FIXNUM_0x05: return (short)5;
                case MsgPackType.POSITIVE_FIXNUM_0x06: return (short)6;
                case MsgPackType.POSITIVE_FIXNUM_0x07: return (short)7;
                case MsgPackType.POSITIVE_FIXNUM_0x08: return (short)8;
                case MsgPackType.POSITIVE_FIXNUM_0x09: return (short)9;
                case MsgPackType.POSITIVE_FIXNUM_0x0A: return (short)10;
                case MsgPackType.POSITIVE_FIXNUM_0x0B: return (short)11;
                case MsgPackType.POSITIVE_FIXNUM_0x0C: return (short)12;
                case MsgPackType.POSITIVE_FIXNUM_0x0D: return (short)13;
                case MsgPackType.POSITIVE_FIXNUM_0x0E: return (short)14;
                case MsgPackType.POSITIVE_FIXNUM_0x0F: return (short)15;

                case MsgPackType.POSITIVE_FIXNUM_0x10: return (short)16;
                case MsgPackType.POSITIVE_FIXNUM_0x11: return (short)17;
                case MsgPackType.POSITIVE_FIXNUM_0x12: return (short)18;
                case MsgPackType.POSITIVE_FIXNUM_0x13: return (short)19;
                case MsgPackType.POSITIVE_FIXNUM_0x14: return (short)20;
                case MsgPackType.POSITIVE_FIXNUM_0x15: return (short)21;
                case MsgPackType.POSITIVE_FIXNUM_0x16: return (short)22;
                case MsgPackType.POSITIVE_FIXNUM_0x17: return (short)23;
                case MsgPackType.POSITIVE_FIXNUM_0x18: return (short)24;
                case MsgPackType.POSITIVE_FIXNUM_0x19: return (short)25;
                case MsgPackType.POSITIVE_FIXNUM_0x1A: return (short)26;
                case MsgPackType.POSITIVE_FIXNUM_0x1B: return (short)27;
                case MsgPackType.POSITIVE_FIXNUM_0x1C: return (short)28;
                case MsgPackType.POSITIVE_FIXNUM_0x1D: return (short)29;
                case MsgPackType.POSITIVE_FIXNUM_0x1E: return (short)30;
                case MsgPackType.POSITIVE_FIXNUM_0x1F: return (short)31;

                case MsgPackType.POSITIVE_FIXNUM_0x20: return (short)32;
                case MsgPackType.POSITIVE_FIXNUM_0x21: return (short)33;
                case MsgPackType.POSITIVE_FIXNUM_0x22: return (short)34;
                case MsgPackType.POSITIVE_FIXNUM_0x23: return (short)35;
                case MsgPackType.POSITIVE_FIXNUM_0x24: return (short)36;
                case MsgPackType.POSITIVE_FIXNUM_0x25: return (short)37;
                case MsgPackType.POSITIVE_FIXNUM_0x26: return (short)38;
                case MsgPackType.POSITIVE_FIXNUM_0x27: return (short)39;
                case MsgPackType.POSITIVE_FIXNUM_0x28: return (short)40;
                case MsgPackType.POSITIVE_FIXNUM_0x29: return (short)41;
                case MsgPackType.POSITIVE_FIXNUM_0x2A: return (short)42;
                case MsgPackType.POSITIVE_FIXNUM_0x2B: return (short)43;
                case MsgPackType.POSITIVE_FIXNUM_0x2C: return (short)44;
                case MsgPackType.POSITIVE_FIXNUM_0x2D: return (short)45;
                case MsgPackType.POSITIVE_FIXNUM_0x2E: return (short)46;
                case MsgPackType.POSITIVE_FIXNUM_0x2F: return (short)47;

                case MsgPackType.POSITIVE_FIXNUM_0x30: return (short)48;
                case MsgPackType.POSITIVE_FIXNUM_0x31: return (short)49;
                case MsgPackType.POSITIVE_FIXNUM_0x32: return (short)50;
                case MsgPackType.POSITIVE_FIXNUM_0x33: return (short)51;
                case MsgPackType.POSITIVE_FIXNUM_0x34: return (short)52;
                case MsgPackType.POSITIVE_FIXNUM_0x35: return (short)53;
                case MsgPackType.POSITIVE_FIXNUM_0x36: return (short)54;
                case MsgPackType.POSITIVE_FIXNUM_0x37: return (short)55;
                case MsgPackType.POSITIVE_FIXNUM_0x38: return (short)56;
                case MsgPackType.POSITIVE_FIXNUM_0x39: return (short)57;
                case MsgPackType.POSITIVE_FIXNUM_0x3A: return (short)58;
                case MsgPackType.POSITIVE_FIXNUM_0x3B: return (short)59;
                case MsgPackType.POSITIVE_FIXNUM_0x3C: return (short)60;
                case MsgPackType.POSITIVE_FIXNUM_0x3D: return (short)61;
                case MsgPackType.POSITIVE_FIXNUM_0x3E: return (short)62;
                case MsgPackType.POSITIVE_FIXNUM_0x3F: return (short)63;

                case MsgPackType.POSITIVE_FIXNUM_0x40: return (short)64;
                case MsgPackType.POSITIVE_FIXNUM_0x41: return (short)65;
                case MsgPackType.POSITIVE_FIXNUM_0x42: return (short)66;
                case MsgPackType.POSITIVE_FIXNUM_0x43: return (short)67;
                case MsgPackType.POSITIVE_FIXNUM_0x44: return (short)68;
                case MsgPackType.POSITIVE_FIXNUM_0x45: return (short)69;
                case MsgPackType.POSITIVE_FIXNUM_0x46: return (short)70;
                case MsgPackType.POSITIVE_FIXNUM_0x47: return (short)71;
                case MsgPackType.POSITIVE_FIXNUM_0x48: return (short)72;
                case MsgPackType.POSITIVE_FIXNUM_0x49: return (short)73;
                case MsgPackType.POSITIVE_FIXNUM_0x4A: return (short)74; 
                case MsgPackType.POSITIVE_FIXNUM_0x4B: return (short)75;
                case MsgPackType.POSITIVE_FIXNUM_0x4C: return (short)76;
                case MsgPackType.POSITIVE_FIXNUM_0x4D: return (short)77;
                case MsgPackType.POSITIVE_FIXNUM_0x4E: return (short)78;
                case MsgPackType.POSITIVE_FIXNUM_0x4F: return (short)79;
                     
                case MsgPackType.POSITIVE_FIXNUM_0x50: return (short)80;
                case MsgPackType.POSITIVE_FIXNUM_0x51: return (short)81;
                case MsgPackType.POSITIVE_FIXNUM_0x52: return (short)82;
                case MsgPackType.POSITIVE_FIXNUM_0x53: return (short)83;
                case MsgPackType.POSITIVE_FIXNUM_0x54: return (short)84;
                case MsgPackType.POSITIVE_FIXNUM_0x55: return (short)85;
                case MsgPackType.POSITIVE_FIXNUM_0x56: return (short)86;
                case MsgPackType.POSITIVE_FIXNUM_0x57: return (short)87;
                case MsgPackType.POSITIVE_FIXNUM_0x58: return (short)88;
                case MsgPackType.POSITIVE_FIXNUM_0x59: return (short)89;
                case MsgPackType.POSITIVE_FIXNUM_0x5A: return (short)90;
                case MsgPackType.POSITIVE_FIXNUM_0x5B: return (short)91;
                case MsgPackType.POSITIVE_FIXNUM_0x5C: return (short)92;
                case MsgPackType.POSITIVE_FIXNUM_0x5D: return (short)93;
                case MsgPackType.POSITIVE_FIXNUM_0x5E: return (short)94;
                case MsgPackType.POSITIVE_FIXNUM_0x5F: return (short)95;

                case MsgPackType.POSITIVE_FIXNUM_0x60: return (short)96;
                case MsgPackType.POSITIVE_FIXNUM_0x61: return (short)97;
                case MsgPackType.POSITIVE_FIXNUM_0x62: return (short)98;
                case MsgPackType.POSITIVE_FIXNUM_0x63: return (short)99;
                case MsgPackType.POSITIVE_FIXNUM_0x64: return (short)100;
                case MsgPackType.POSITIVE_FIXNUM_0x65: return (short)101;
                case MsgPackType.POSITIVE_FIXNUM_0x66: return (short)102;
                case MsgPackType.POSITIVE_FIXNUM_0x67: return (short)103;
                case MsgPackType.POSITIVE_FIXNUM_0x68: return (short)104;
                case MsgPackType.POSITIVE_FIXNUM_0x69: return (short)105;
                case MsgPackType.POSITIVE_FIXNUM_0x6A: return (short)106;
                case MsgPackType.POSITIVE_FIXNUM_0x6B: return (short)107;
                case MsgPackType.POSITIVE_FIXNUM_0x6C: return (short)108;
                case MsgPackType.POSITIVE_FIXNUM_0x6D: return (short)109;
                case MsgPackType.POSITIVE_FIXNUM_0x6E: return (short)110;
                case MsgPackType.POSITIVE_FIXNUM_0x6F: return (short)111;

                case MsgPackType.POSITIVE_FIXNUM_0x70: return (short)112;
                case MsgPackType.POSITIVE_FIXNUM_0x71: return (short)113;
                case MsgPackType.POSITIVE_FIXNUM_0x72: return (short)114;
                case MsgPackType.POSITIVE_FIXNUM_0x73: return (short)115;
                case MsgPackType.POSITIVE_FIXNUM_0x74: return (short)116;
                case MsgPackType.POSITIVE_FIXNUM_0x75: return (short)117;
                case MsgPackType.POSITIVE_FIXNUM_0x76: return (short)118;
                case MsgPackType.POSITIVE_FIXNUM_0x77: return (short)119;
                case MsgPackType.POSITIVE_FIXNUM_0x78: return (short)120;
                case MsgPackType.POSITIVE_FIXNUM_0x79: return (short)121;
                case MsgPackType.POSITIVE_FIXNUM_0x7A: return (short)122;
                case MsgPackType.POSITIVE_FIXNUM_0x7B: return (short)123;
                case MsgPackType.POSITIVE_FIXNUM_0x7C: return (short)124;
                case MsgPackType.POSITIVE_FIXNUM_0x7D: return (short)125;
                case MsgPackType.POSITIVE_FIXNUM_0x7E: return (short)126;
                case MsgPackType.POSITIVE_FIXNUM_0x7F: return (short)127;

                case MsgPackType.NEGATIVE_FIXNUM: return (short)-32;
                case MsgPackType.NEGATIVE_FIXNUM_0x01: return (short)-1;
                case MsgPackType.NEGATIVE_FIXNUM_0x02: return (short)-2;
                case MsgPackType.NEGATIVE_FIXNUM_0x03: return (short)-3;
                case MsgPackType.NEGATIVE_FIXNUM_0x04: return (short)-4;
                case MsgPackType.NEGATIVE_FIXNUM_0x05: return (short)-5;
                case MsgPackType.NEGATIVE_FIXNUM_0x06: return (short)-6;
                case MsgPackType.NEGATIVE_FIXNUM_0x07: return (short)-7;
                case MsgPackType.NEGATIVE_FIXNUM_0x08: return (short)-8;
                case MsgPackType.NEGATIVE_FIXNUM_0x09: return (short)-9;
                case MsgPackType.NEGATIVE_FIXNUM_0x0A: return (short)-10;
                case MsgPackType.NEGATIVE_FIXNUM_0x0B: return (short)-11;
                case MsgPackType.NEGATIVE_FIXNUM_0x0C: return (short)-12;
                case MsgPackType.NEGATIVE_FIXNUM_0x0D: return (short)-13;
                case MsgPackType.NEGATIVE_FIXNUM_0x0E: return (short)-14;
                case MsgPackType.NEGATIVE_FIXNUM_0x0F: return (short)-15;
                case MsgPackType.NEGATIVE_FIXNUM_0x10: return (short)-16;
                case MsgPackType.NEGATIVE_FIXNUM_0x11: return (short)-17;
                case MsgPackType.NEGATIVE_FIXNUM_0x12: return (short)-18;
                case MsgPackType.NEGATIVE_FIXNUM_0x13: return (short)-19;
                case MsgPackType.NEGATIVE_FIXNUM_0x14: return (short)-20;
                case MsgPackType.NEGATIVE_FIXNUM_0x15: return (short)-21;
                case MsgPackType.NEGATIVE_FIXNUM_0x16: return (short)-22;
                case MsgPackType.NEGATIVE_FIXNUM_0x17: return (short)-23;
                case MsgPackType.NEGATIVE_FIXNUM_0x18: return (short)-24;
                case MsgPackType.NEGATIVE_FIXNUM_0x19: return (short)-25;
                case MsgPackType.NEGATIVE_FIXNUM_0x1A: return (short)-26;
                case MsgPackType.NEGATIVE_FIXNUM_0x1B: return (short)-27;
                case MsgPackType.NEGATIVE_FIXNUM_0x1C: return (short)-28;
                case MsgPackType.NEGATIVE_FIXNUM_0x1D: return (short)-29;
                case MsgPackType.NEGATIVE_FIXNUM_0x1E: return (short)-30;
                case MsgPackType.NEGATIVE_FIXNUM_0x1F: return (short)-31;

                case MsgPackType.INT8: return (short)(SByte)GetBody().Get(0);
                case MsgPackType.INT16: return (short)EndianConverter.NetworkByteWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT32: return (short)EndianConverter.NetworkByteDWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT64: return (short)EndianConverter.NetworkByteQWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.UINT8: return (short)GetBody().Get(0);
                case MsgPackType.UINT16: return (short)EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT32: return (short)EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT64: return (short)EndianConverter.NetworkByteQWordToUnsignedNativeByteOrder(GetBody());

                default: throw new MessagePackValueException("is not short" + Bytes);
            }
            }
        }

        public Int32 GetInt32()
        {
            unchecked{
            switch(FormatType)
            {
                case MsgPackType.POSITIVE_FIXNUM: return (int)0;
                case MsgPackType.POSITIVE_FIXNUM_0x01: return (int)1;
                case MsgPackType.POSITIVE_FIXNUM_0x02: return (int)2;
                case MsgPackType.POSITIVE_FIXNUM_0x03: return (int)3;
                case MsgPackType.POSITIVE_FIXNUM_0x04: return (int)4;
                case MsgPackType.POSITIVE_FIXNUM_0x05: return (int)5;
                case MsgPackType.POSITIVE_FIXNUM_0x06: return (int)6;
                case MsgPackType.POSITIVE_FIXNUM_0x07: return (int)7;
                case MsgPackType.POSITIVE_FIXNUM_0x08: return (int)8;
                case MsgPackType.POSITIVE_FIXNUM_0x09: return (int)9;
                case MsgPackType.POSITIVE_FIXNUM_0x0A: return (int)10;
                case MsgPackType.POSITIVE_FIXNUM_0x0B: return (int)11;
                case MsgPackType.POSITIVE_FIXNUM_0x0C: return (int)12;
                case MsgPackType.POSITIVE_FIXNUM_0x0D: return (int)13;
                case MsgPackType.POSITIVE_FIXNUM_0x0E: return (int)14;
                case MsgPackType.POSITIVE_FIXNUM_0x0F: return (int)15;

                case MsgPackType.POSITIVE_FIXNUM_0x10: return (int)16;
                case MsgPackType.POSITIVE_FIXNUM_0x11: return (int)17;
                case MsgPackType.POSITIVE_FIXNUM_0x12: return (int)18;
                case MsgPackType.POSITIVE_FIXNUM_0x13: return (int)19;
                case MsgPackType.POSITIVE_FIXNUM_0x14: return (int)20;
                case MsgPackType.POSITIVE_FIXNUM_0x15: return (int)21;
                case MsgPackType.POSITIVE_FIXNUM_0x16: return (int)22;
                case MsgPackType.POSITIVE_FIXNUM_0x17: return (int)23;
                case MsgPackType.POSITIVE_FIXNUM_0x18: return (int)24;
                case MsgPackType.POSITIVE_FIXNUM_0x19: return (int)25;
                case MsgPackType.POSITIVE_FIXNUM_0x1A: return (int)26;
                case MsgPackType.POSITIVE_FIXNUM_0x1B: return (int)27;
                case MsgPackType.POSITIVE_FIXNUM_0x1C: return (int)28;
                case MsgPackType.POSITIVE_FIXNUM_0x1D: return (int)29;
                case MsgPackType.POSITIVE_FIXNUM_0x1E: return (int)30;
                case MsgPackType.POSITIVE_FIXNUM_0x1F: return (int)31;

                case MsgPackType.POSITIVE_FIXNUM_0x20: return (int)32;
                case MsgPackType.POSITIVE_FIXNUM_0x21: return (int)33;
                case MsgPackType.POSITIVE_FIXNUM_0x22: return (int)34;
                case MsgPackType.POSITIVE_FIXNUM_0x23: return (int)35;
                case MsgPackType.POSITIVE_FIXNUM_0x24: return (int)36;
                case MsgPackType.POSITIVE_FIXNUM_0x25: return (int)37;
                case MsgPackType.POSITIVE_FIXNUM_0x26: return (int)38;
                case MsgPackType.POSITIVE_FIXNUM_0x27: return (int)39;
                case MsgPackType.POSITIVE_FIXNUM_0x28: return (int)40;
                case MsgPackType.POSITIVE_FIXNUM_0x29: return (int)41;
                case MsgPackType.POSITIVE_FIXNUM_0x2A: return (int)42;
                case MsgPackType.POSITIVE_FIXNUM_0x2B: return (int)43;
                case MsgPackType.POSITIVE_FIXNUM_0x2C: return (int)44;
                case MsgPackType.POSITIVE_FIXNUM_0x2D: return (int)45;
                case MsgPackType.POSITIVE_FIXNUM_0x2E: return (int)46;
                case MsgPackType.POSITIVE_FIXNUM_0x2F: return (int)47;

                case MsgPackType.POSITIVE_FIXNUM_0x30: return (int)48;
                case MsgPackType.POSITIVE_FIXNUM_0x31: return (int)49;
                case MsgPackType.POSITIVE_FIXNUM_0x32: return (int)50;
                case MsgPackType.POSITIVE_FIXNUM_0x33: return (int)51;
                case MsgPackType.POSITIVE_FIXNUM_0x34: return (int)52;
                case MsgPackType.POSITIVE_FIXNUM_0x35: return (int)53;
                case MsgPackType.POSITIVE_FIXNUM_0x36: return (int)54;
                case MsgPackType.POSITIVE_FIXNUM_0x37: return (int)55;
                case MsgPackType.POSITIVE_FIXNUM_0x38: return (int)56;
                case MsgPackType.POSITIVE_FIXNUM_0x39: return (int)57;
                case MsgPackType.POSITIVE_FIXNUM_0x3A: return (int)58;
                case MsgPackType.POSITIVE_FIXNUM_0x3B: return (int)59;
                case MsgPackType.POSITIVE_FIXNUM_0x3C: return (int)60;
                case MsgPackType.POSITIVE_FIXNUM_0x3D: return (int)61;
                case MsgPackType.POSITIVE_FIXNUM_0x3E: return (int)62;
                case MsgPackType.POSITIVE_FIXNUM_0x3F: return (int)63;

                case MsgPackType.POSITIVE_FIXNUM_0x40: return (int)64;
                case MsgPackType.POSITIVE_FIXNUM_0x41: return (int)65;
                case MsgPackType.POSITIVE_FIXNUM_0x42: return (int)66;
                case MsgPackType.POSITIVE_FIXNUM_0x43: return (int)67;
                case MsgPackType.POSITIVE_FIXNUM_0x44: return (int)68;
                case MsgPackType.POSITIVE_FIXNUM_0x45: return (int)69;
                case MsgPackType.POSITIVE_FIXNUM_0x46: return (int)70;
                case MsgPackType.POSITIVE_FIXNUM_0x47: return (int)71;
                case MsgPackType.POSITIVE_FIXNUM_0x48: return (int)72;
                case MsgPackType.POSITIVE_FIXNUM_0x49: return (int)73;
                case MsgPackType.POSITIVE_FIXNUM_0x4A: return (int)74; 
                case MsgPackType.POSITIVE_FIXNUM_0x4B: return (int)75;
                case MsgPackType.POSITIVE_FIXNUM_0x4C: return (int)76;
                case MsgPackType.POSITIVE_FIXNUM_0x4D: return (int)77;
                case MsgPackType.POSITIVE_FIXNUM_0x4E: return (int)78;
                case MsgPackType.POSITIVE_FIXNUM_0x4F: return (int)79;
                     
                case MsgPackType.POSITIVE_FIXNUM_0x50: return (int)80;
                case MsgPackType.POSITIVE_FIXNUM_0x51: return (int)81;
                case MsgPackType.POSITIVE_FIXNUM_0x52: return (int)82;
                case MsgPackType.POSITIVE_FIXNUM_0x53: return (int)83;
                case MsgPackType.POSITIVE_FIXNUM_0x54: return (int)84;
                case MsgPackType.POSITIVE_FIXNUM_0x55: return (int)85;
                case MsgPackType.POSITIVE_FIXNUM_0x56: return (int)86;
                case MsgPackType.POSITIVE_FIXNUM_0x57: return (int)87;
                case MsgPackType.POSITIVE_FIXNUM_0x58: return (int)88;
                case MsgPackType.POSITIVE_FIXNUM_0x59: return (int)89;
                case MsgPackType.POSITIVE_FIXNUM_0x5A: return (int)90;
                case MsgPackType.POSITIVE_FIXNUM_0x5B: return (int)91;
                case MsgPackType.POSITIVE_FIXNUM_0x5C: return (int)92;
                case MsgPackType.POSITIVE_FIXNUM_0x5D: return (int)93;
                case MsgPackType.POSITIVE_FIXNUM_0x5E: return (int)94;
                case MsgPackType.POSITIVE_FIXNUM_0x5F: return (int)95;

                case MsgPackType.POSITIVE_FIXNUM_0x60: return (int)96;
                case MsgPackType.POSITIVE_FIXNUM_0x61: return (int)97;
                case MsgPackType.POSITIVE_FIXNUM_0x62: return (int)98;
                case MsgPackType.POSITIVE_FIXNUM_0x63: return (int)99;
                case MsgPackType.POSITIVE_FIXNUM_0x64: return (int)100;
                case MsgPackType.POSITIVE_FIXNUM_0x65: return (int)101;
                case MsgPackType.POSITIVE_FIXNUM_0x66: return (int)102;
                case MsgPackType.POSITIVE_FIXNUM_0x67: return (int)103;
                case MsgPackType.POSITIVE_FIXNUM_0x68: return (int)104;
                case MsgPackType.POSITIVE_FIXNUM_0x69: return (int)105;
                case MsgPackType.POSITIVE_FIXNUM_0x6A: return (int)106;
                case MsgPackType.POSITIVE_FIXNUM_0x6B: return (int)107;
                case MsgPackType.POSITIVE_FIXNUM_0x6C: return (int)108;
                case MsgPackType.POSITIVE_FIXNUM_0x6D: return (int)109;
                case MsgPackType.POSITIVE_FIXNUM_0x6E: return (int)110;
                case MsgPackType.POSITIVE_FIXNUM_0x6F: return (int)111;

                case MsgPackType.POSITIVE_FIXNUM_0x70: return (int)112;
                case MsgPackType.POSITIVE_FIXNUM_0x71: return (int)113;
                case MsgPackType.POSITIVE_FIXNUM_0x72: return (int)114;
                case MsgPackType.POSITIVE_FIXNUM_0x73: return (int)115;
                case MsgPackType.POSITIVE_FIXNUM_0x74: return (int)116;
                case MsgPackType.POSITIVE_FIXNUM_0x75: return (int)117;
                case MsgPackType.POSITIVE_FIXNUM_0x76: return (int)118;
                case MsgPackType.POSITIVE_FIXNUM_0x77: return (int)119;
                case MsgPackType.POSITIVE_FIXNUM_0x78: return (int)120;
                case MsgPackType.POSITIVE_FIXNUM_0x79: return (int)121;
                case MsgPackType.POSITIVE_FIXNUM_0x7A: return (int)122;
                case MsgPackType.POSITIVE_FIXNUM_0x7B: return (int)123;
                case MsgPackType.POSITIVE_FIXNUM_0x7C: return (int)124;
                case MsgPackType.POSITIVE_FIXNUM_0x7D: return (int)125;
                case MsgPackType.POSITIVE_FIXNUM_0x7E: return (int)126;
                case MsgPackType.POSITIVE_FIXNUM_0x7F: return (int)127;

                case MsgPackType.NEGATIVE_FIXNUM: return (int)-32;
                case MsgPackType.NEGATIVE_FIXNUM_0x01: return (int)-1;
                case MsgPackType.NEGATIVE_FIXNUM_0x02: return (int)-2;
                case MsgPackType.NEGATIVE_FIXNUM_0x03: return (int)-3;
                case MsgPackType.NEGATIVE_FIXNUM_0x04: return (int)-4;
                case MsgPackType.NEGATIVE_FIXNUM_0x05: return (int)-5;
                case MsgPackType.NEGATIVE_FIXNUM_0x06: return (int)-6;
                case MsgPackType.NEGATIVE_FIXNUM_0x07: return (int)-7;
                case MsgPackType.NEGATIVE_FIXNUM_0x08: return (int)-8;
                case MsgPackType.NEGATIVE_FIXNUM_0x09: return (int)-9;
                case MsgPackType.NEGATIVE_FIXNUM_0x0A: return (int)-10;
                case MsgPackType.NEGATIVE_FIXNUM_0x0B: return (int)-11;
                case MsgPackType.NEGATIVE_FIXNUM_0x0C: return (int)-12;
                case MsgPackType.NEGATIVE_FIXNUM_0x0D: return (int)-13;
                case MsgPackType.NEGATIVE_FIXNUM_0x0E: return (int)-14;
                case MsgPackType.NEGATIVE_FIXNUM_0x0F: return (int)-15;
                case MsgPackType.NEGATIVE_FIXNUM_0x10: return (int)-16;
                case MsgPackType.NEGATIVE_FIXNUM_0x11: return (int)-17;
                case MsgPackType.NEGATIVE_FIXNUM_0x12: return (int)-18;
                case MsgPackType.NEGATIVE_FIXNUM_0x13: return (int)-19;
                case MsgPackType.NEGATIVE_FIXNUM_0x14: return (int)-20;
                case MsgPackType.NEGATIVE_FIXNUM_0x15: return (int)-21;
                case MsgPackType.NEGATIVE_FIXNUM_0x16: return (int)-22;
                case MsgPackType.NEGATIVE_FIXNUM_0x17: return (int)-23;
                case MsgPackType.NEGATIVE_FIXNUM_0x18: return (int)-24;
                case MsgPackType.NEGATIVE_FIXNUM_0x19: return (int)-25;
                case MsgPackType.NEGATIVE_FIXNUM_0x1A: return (int)-26;
                case MsgPackType.NEGATIVE_FIXNUM_0x1B: return (int)-27;
                case MsgPackType.NEGATIVE_FIXNUM_0x1C: return (int)-28;
                case MsgPackType.NEGATIVE_FIXNUM_0x1D: return (int)-29;
                case MsgPackType.NEGATIVE_FIXNUM_0x1E: return (int)-30;
                case MsgPackType.NEGATIVE_FIXNUM_0x1F: return (int)-31;

                case MsgPackType.INT8: return (int)(SByte)GetBody().Get(0);
                case MsgPackType.INT16: return (int)EndianConverter.NetworkByteWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT32: return (int)EndianConverter.NetworkByteDWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT64: return (int)EndianConverter.NetworkByteQWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.UINT8: return (int)GetBody().Get(0);
                case MsgPackType.UINT16: return (int)EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT32: return (int)EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT64: return (int)EndianConverter.NetworkByteQWordToUnsignedNativeByteOrder(GetBody());

                default: throw new MessagePackValueException("is not int" + Bytes);
            }
            }
        }

        public Int64 GetInt64()
        {
            unchecked{
            switch(FormatType)
            {
                case MsgPackType.POSITIVE_FIXNUM: return (long)0;
                case MsgPackType.POSITIVE_FIXNUM_0x01: return (long)1;
                case MsgPackType.POSITIVE_FIXNUM_0x02: return (long)2;
                case MsgPackType.POSITIVE_FIXNUM_0x03: return (long)3;
                case MsgPackType.POSITIVE_FIXNUM_0x04: return (long)4;
                case MsgPackType.POSITIVE_FIXNUM_0x05: return (long)5;
                case MsgPackType.POSITIVE_FIXNUM_0x06: return (long)6;
                case MsgPackType.POSITIVE_FIXNUM_0x07: return (long)7;
                case MsgPackType.POSITIVE_FIXNUM_0x08: return (long)8;
                case MsgPackType.POSITIVE_FIXNUM_0x09: return (long)9;
                case MsgPackType.POSITIVE_FIXNUM_0x0A: return (long)10;
                case MsgPackType.POSITIVE_FIXNUM_0x0B: return (long)11;
                case MsgPackType.POSITIVE_FIXNUM_0x0C: return (long)12;
                case MsgPackType.POSITIVE_FIXNUM_0x0D: return (long)13;
                case MsgPackType.POSITIVE_FIXNUM_0x0E: return (long)14;
                case MsgPackType.POSITIVE_FIXNUM_0x0F: return (long)15;

                case MsgPackType.POSITIVE_FIXNUM_0x10: return (long)16;
                case MsgPackType.POSITIVE_FIXNUM_0x11: return (long)17;
                case MsgPackType.POSITIVE_FIXNUM_0x12: return (long)18;
                case MsgPackType.POSITIVE_FIXNUM_0x13: return (long)19;
                case MsgPackType.POSITIVE_FIXNUM_0x14: return (long)20;
                case MsgPackType.POSITIVE_FIXNUM_0x15: return (long)21;
                case MsgPackType.POSITIVE_FIXNUM_0x16: return (long)22;
                case MsgPackType.POSITIVE_FIXNUM_0x17: return (long)23;
                case MsgPackType.POSITIVE_FIXNUM_0x18: return (long)24;
                case MsgPackType.POSITIVE_FIXNUM_0x19: return (long)25;
                case MsgPackType.POSITIVE_FIXNUM_0x1A: return (long)26;
                case MsgPackType.POSITIVE_FIXNUM_0x1B: return (long)27;
                case MsgPackType.POSITIVE_FIXNUM_0x1C: return (long)28;
                case MsgPackType.POSITIVE_FIXNUM_0x1D: return (long)29;
                case MsgPackType.POSITIVE_FIXNUM_0x1E: return (long)30;
                case MsgPackType.POSITIVE_FIXNUM_0x1F: return (long)31;

                case MsgPackType.POSITIVE_FIXNUM_0x20: return (long)32;
                case MsgPackType.POSITIVE_FIXNUM_0x21: return (long)33;
                case MsgPackType.POSITIVE_FIXNUM_0x22: return (long)34;
                case MsgPackType.POSITIVE_FIXNUM_0x23: return (long)35;
                case MsgPackType.POSITIVE_FIXNUM_0x24: return (long)36;
                case MsgPackType.POSITIVE_FIXNUM_0x25: return (long)37;
                case MsgPackType.POSITIVE_FIXNUM_0x26: return (long)38;
                case MsgPackType.POSITIVE_FIXNUM_0x27: return (long)39;
                case MsgPackType.POSITIVE_FIXNUM_0x28: return (long)40;
                case MsgPackType.POSITIVE_FIXNUM_0x29: return (long)41;
                case MsgPackType.POSITIVE_FIXNUM_0x2A: return (long)42;
                case MsgPackType.POSITIVE_FIXNUM_0x2B: return (long)43;
                case MsgPackType.POSITIVE_FIXNUM_0x2C: return (long)44;
                case MsgPackType.POSITIVE_FIXNUM_0x2D: return (long)45;
                case MsgPackType.POSITIVE_FIXNUM_0x2E: return (long)46;
                case MsgPackType.POSITIVE_FIXNUM_0x2F: return (long)47;

                case MsgPackType.POSITIVE_FIXNUM_0x30: return (long)48;
                case MsgPackType.POSITIVE_FIXNUM_0x31: return (long)49;
                case MsgPackType.POSITIVE_FIXNUM_0x32: return (long)50;
                case MsgPackType.POSITIVE_FIXNUM_0x33: return (long)51;
                case MsgPackType.POSITIVE_FIXNUM_0x34: return (long)52;
                case MsgPackType.POSITIVE_FIXNUM_0x35: return (long)53;
                case MsgPackType.POSITIVE_FIXNUM_0x36: return (long)54;
                case MsgPackType.POSITIVE_FIXNUM_0x37: return (long)55;
                case MsgPackType.POSITIVE_FIXNUM_0x38: return (long)56;
                case MsgPackType.POSITIVE_FIXNUM_0x39: return (long)57;
                case MsgPackType.POSITIVE_FIXNUM_0x3A: return (long)58;
                case MsgPackType.POSITIVE_FIXNUM_0x3B: return (long)59;
                case MsgPackType.POSITIVE_FIXNUM_0x3C: return (long)60;
                case MsgPackType.POSITIVE_FIXNUM_0x3D: return (long)61;
                case MsgPackType.POSITIVE_FIXNUM_0x3E: return (long)62;
                case MsgPackType.POSITIVE_FIXNUM_0x3F: return (long)63;

                case MsgPackType.POSITIVE_FIXNUM_0x40: return (long)64;
                case MsgPackType.POSITIVE_FIXNUM_0x41: return (long)65;
                case MsgPackType.POSITIVE_FIXNUM_0x42: return (long)66;
                case MsgPackType.POSITIVE_FIXNUM_0x43: return (long)67;
                case MsgPackType.POSITIVE_FIXNUM_0x44: return (long)68;
                case MsgPackType.POSITIVE_FIXNUM_0x45: return (long)69;
                case MsgPackType.POSITIVE_FIXNUM_0x46: return (long)70;
                case MsgPackType.POSITIVE_FIXNUM_0x47: return (long)71;
                case MsgPackType.POSITIVE_FIXNUM_0x48: return (long)72;
                case MsgPackType.POSITIVE_FIXNUM_0x49: return (long)73;
                case MsgPackType.POSITIVE_FIXNUM_0x4A: return (long)74; 
                case MsgPackType.POSITIVE_FIXNUM_0x4B: return (long)75;
                case MsgPackType.POSITIVE_FIXNUM_0x4C: return (long)76;
                case MsgPackType.POSITIVE_FIXNUM_0x4D: return (long)77;
                case MsgPackType.POSITIVE_FIXNUM_0x4E: return (long)78;
                case MsgPackType.POSITIVE_FIXNUM_0x4F: return (long)79;
                     
                case MsgPackType.POSITIVE_FIXNUM_0x50: return (long)80;
                case MsgPackType.POSITIVE_FIXNUM_0x51: return (long)81;
                case MsgPackType.POSITIVE_FIXNUM_0x52: return (long)82;
                case MsgPackType.POSITIVE_FIXNUM_0x53: return (long)83;
                case MsgPackType.POSITIVE_FIXNUM_0x54: return (long)84;
                case MsgPackType.POSITIVE_FIXNUM_0x55: return (long)85;
                case MsgPackType.POSITIVE_FIXNUM_0x56: return (long)86;
                case MsgPackType.POSITIVE_FIXNUM_0x57: return (long)87;
                case MsgPackType.POSITIVE_FIXNUM_0x58: return (long)88;
                case MsgPackType.POSITIVE_FIXNUM_0x59: return (long)89;
                case MsgPackType.POSITIVE_FIXNUM_0x5A: return (long)90;
                case MsgPackType.POSITIVE_FIXNUM_0x5B: return (long)91;
                case MsgPackType.POSITIVE_FIXNUM_0x5C: return (long)92;
                case MsgPackType.POSITIVE_FIXNUM_0x5D: return (long)93;
                case MsgPackType.POSITIVE_FIXNUM_0x5E: return (long)94;
                case MsgPackType.POSITIVE_FIXNUM_0x5F: return (long)95;

                case MsgPackType.POSITIVE_FIXNUM_0x60: return (long)96;
                case MsgPackType.POSITIVE_FIXNUM_0x61: return (long)97;
                case MsgPackType.POSITIVE_FIXNUM_0x62: return (long)98;
                case MsgPackType.POSITIVE_FIXNUM_0x63: return (long)99;
                case MsgPackType.POSITIVE_FIXNUM_0x64: return (long)100;
                case MsgPackType.POSITIVE_FIXNUM_0x65: return (long)101;
                case MsgPackType.POSITIVE_FIXNUM_0x66: return (long)102;
                case MsgPackType.POSITIVE_FIXNUM_0x67: return (long)103;
                case MsgPackType.POSITIVE_FIXNUM_0x68: return (long)104;
                case MsgPackType.POSITIVE_FIXNUM_0x69: return (long)105;
                case MsgPackType.POSITIVE_FIXNUM_0x6A: return (long)106;
                case MsgPackType.POSITIVE_FIXNUM_0x6B: return (long)107;
                case MsgPackType.POSITIVE_FIXNUM_0x6C: return (long)108;
                case MsgPackType.POSITIVE_FIXNUM_0x6D: return (long)109;
                case MsgPackType.POSITIVE_FIXNUM_0x6E: return (long)110;
                case MsgPackType.POSITIVE_FIXNUM_0x6F: return (long)111;

                case MsgPackType.POSITIVE_FIXNUM_0x70: return (long)112;
                case MsgPackType.POSITIVE_FIXNUM_0x71: return (long)113;
                case MsgPackType.POSITIVE_FIXNUM_0x72: return (long)114;
                case MsgPackType.POSITIVE_FIXNUM_0x73: return (long)115;
                case MsgPackType.POSITIVE_FIXNUM_0x74: return (long)116;
                case MsgPackType.POSITIVE_FIXNUM_0x75: return (long)117;
                case MsgPackType.POSITIVE_FIXNUM_0x76: return (long)118;
                case MsgPackType.POSITIVE_FIXNUM_0x77: return (long)119;
                case MsgPackType.POSITIVE_FIXNUM_0x78: return (long)120;
                case MsgPackType.POSITIVE_FIXNUM_0x79: return (long)121;
                case MsgPackType.POSITIVE_FIXNUM_0x7A: return (long)122;
                case MsgPackType.POSITIVE_FIXNUM_0x7B: return (long)123;
                case MsgPackType.POSITIVE_FIXNUM_0x7C: return (long)124;
                case MsgPackType.POSITIVE_FIXNUM_0x7D: return (long)125;
                case MsgPackType.POSITIVE_FIXNUM_0x7E: return (long)126;
                case MsgPackType.POSITIVE_FIXNUM_0x7F: return (long)127;

                case MsgPackType.NEGATIVE_FIXNUM: return (long)-32;
                case MsgPackType.NEGATIVE_FIXNUM_0x01: return (long)-1;
                case MsgPackType.NEGATIVE_FIXNUM_0x02: return (long)-2;
                case MsgPackType.NEGATIVE_FIXNUM_0x03: return (long)-3;
                case MsgPackType.NEGATIVE_FIXNUM_0x04: return (long)-4;
                case MsgPackType.NEGATIVE_FIXNUM_0x05: return (long)-5;
                case MsgPackType.NEGATIVE_FIXNUM_0x06: return (long)-6;
                case MsgPackType.NEGATIVE_FIXNUM_0x07: return (long)-7;
                case MsgPackType.NEGATIVE_FIXNUM_0x08: return (long)-8;
                case MsgPackType.NEGATIVE_FIXNUM_0x09: return (long)-9;
                case MsgPackType.NEGATIVE_FIXNUM_0x0A: return (long)-10;
                case MsgPackType.NEGATIVE_FIXNUM_0x0B: return (long)-11;
                case MsgPackType.NEGATIVE_FIXNUM_0x0C: return (long)-12;
                case MsgPackType.NEGATIVE_FIXNUM_0x0D: return (long)-13;
                case MsgPackType.NEGATIVE_FIXNUM_0x0E: return (long)-14;
                case MsgPackType.NEGATIVE_FIXNUM_0x0F: return (long)-15;
                case MsgPackType.NEGATIVE_FIXNUM_0x10: return (long)-16;
                case MsgPackType.NEGATIVE_FIXNUM_0x11: return (long)-17;
                case MsgPackType.NEGATIVE_FIXNUM_0x12: return (long)-18;
                case MsgPackType.NEGATIVE_FIXNUM_0x13: return (long)-19;
                case MsgPackType.NEGATIVE_FIXNUM_0x14: return (long)-20;
                case MsgPackType.NEGATIVE_FIXNUM_0x15: return (long)-21;
                case MsgPackType.NEGATIVE_FIXNUM_0x16: return (long)-22;
                case MsgPackType.NEGATIVE_FIXNUM_0x17: return (long)-23;
                case MsgPackType.NEGATIVE_FIXNUM_0x18: return (long)-24;
                case MsgPackType.NEGATIVE_FIXNUM_0x19: return (long)-25;
                case MsgPackType.NEGATIVE_FIXNUM_0x1A: return (long)-26;
                case MsgPackType.NEGATIVE_FIXNUM_0x1B: return (long)-27;
                case MsgPackType.NEGATIVE_FIXNUM_0x1C: return (long)-28;
                case MsgPackType.NEGATIVE_FIXNUM_0x1D: return (long)-29;
                case MsgPackType.NEGATIVE_FIXNUM_0x1E: return (long)-30;
                case MsgPackType.NEGATIVE_FIXNUM_0x1F: return (long)-31;

                case MsgPackType.INT8: return (long)(SByte)GetBody().Get(0);
                case MsgPackType.INT16: return (long)EndianConverter.NetworkByteWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT32: return (long)EndianConverter.NetworkByteDWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT64: return (long)EndianConverter.NetworkByteQWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.UINT8: return (long)GetBody().Get(0);
                case MsgPackType.UINT16: return (long)EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT32: return (long)EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT64: return (long)EndianConverter.NetworkByteQWordToUnsignedNativeByteOrder(GetBody());

                default: throw new MessagePackValueException("is not long" + Bytes);
            }
            }
        }

    }
}

