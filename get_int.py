def create_body(tt):
    t=tt[0]
    t1=tt[1]
    return f'''
        public {t} Get{t}()
        {{
            unchecked{{
            switch(FormatType)
            {{
                case MsgPackType.POSITIVE_FIXNUM: return ({t1})0;
                case MsgPackType.POSITIVE_FIXNUM_0x01: return ({t1})1;
                case MsgPackType.POSITIVE_FIXNUM_0x02: return ({t1})2;
                case MsgPackType.POSITIVE_FIXNUM_0x03: return ({t1})3;
                case MsgPackType.POSITIVE_FIXNUM_0x04: return ({t1})4;
                case MsgPackType.POSITIVE_FIXNUM_0x05: return ({t1})5;
                case MsgPackType.POSITIVE_FIXNUM_0x06: return ({t1})6;
                case MsgPackType.POSITIVE_FIXNUM_0x07: return ({t1})7;
                case MsgPackType.POSITIVE_FIXNUM_0x08: return ({t1})8;
                case MsgPackType.POSITIVE_FIXNUM_0x09: return ({t1})9;
                case MsgPackType.POSITIVE_FIXNUM_0x0A: return ({t1})10;
                case MsgPackType.POSITIVE_FIXNUM_0x0B: return ({t1})11;
                case MsgPackType.POSITIVE_FIXNUM_0x0C: return ({t1})12;
                case MsgPackType.POSITIVE_FIXNUM_0x0D: return ({t1})13;
                case MsgPackType.POSITIVE_FIXNUM_0x0E: return ({t1})14;
                case MsgPackType.POSITIVE_FIXNUM_0x0F: return ({t1})15;

                case MsgPackType.POSITIVE_FIXNUM_0x10: return ({t1})16;
                case MsgPackType.POSITIVE_FIXNUM_0x11: return ({t1})17;
                case MsgPackType.POSITIVE_FIXNUM_0x12: return ({t1})18;
                case MsgPackType.POSITIVE_FIXNUM_0x13: return ({t1})19;
                case MsgPackType.POSITIVE_FIXNUM_0x14: return ({t1})20;
                case MsgPackType.POSITIVE_FIXNUM_0x15: return ({t1})21;
                case MsgPackType.POSITIVE_FIXNUM_0x16: return ({t1})22;
                case MsgPackType.POSITIVE_FIXNUM_0x17: return ({t1})23;
                case MsgPackType.POSITIVE_FIXNUM_0x18: return ({t1})24;
                case MsgPackType.POSITIVE_FIXNUM_0x19: return ({t1})25;
                case MsgPackType.POSITIVE_FIXNUM_0x1A: return ({t1})26;
                case MsgPackType.POSITIVE_FIXNUM_0x1B: return ({t1})27;
                case MsgPackType.POSITIVE_FIXNUM_0x1C: return ({t1})28;
                case MsgPackType.POSITIVE_FIXNUM_0x1D: return ({t1})29;
                case MsgPackType.POSITIVE_FIXNUM_0x1E: return ({t1})30;
                case MsgPackType.POSITIVE_FIXNUM_0x1F: return ({t1})31;

                case MsgPackType.POSITIVE_FIXNUM_0x20: return ({t1})32;
                case MsgPackType.POSITIVE_FIXNUM_0x21: return ({t1})33;
                case MsgPackType.POSITIVE_FIXNUM_0x22: return ({t1})34;
                case MsgPackType.POSITIVE_FIXNUM_0x23: return ({t1})35;
                case MsgPackType.POSITIVE_FIXNUM_0x24: return ({t1})36;
                case MsgPackType.POSITIVE_FIXNUM_0x25: return ({t1})37;
                case MsgPackType.POSITIVE_FIXNUM_0x26: return ({t1})38;
                case MsgPackType.POSITIVE_FIXNUM_0x27: return ({t1})39;
                case MsgPackType.POSITIVE_FIXNUM_0x28: return ({t1})40;
                case MsgPackType.POSITIVE_FIXNUM_0x29: return ({t1})41;
                case MsgPackType.POSITIVE_FIXNUM_0x2A: return ({t1})42;
                case MsgPackType.POSITIVE_FIXNUM_0x2B: return ({t1})43;
                case MsgPackType.POSITIVE_FIXNUM_0x2C: return ({t1})44;
                case MsgPackType.POSITIVE_FIXNUM_0x2D: return ({t1})45;
                case MsgPackType.POSITIVE_FIXNUM_0x2E: return ({t1})46;
                case MsgPackType.POSITIVE_FIXNUM_0x2F: return ({t1})47;

                case MsgPackType.POSITIVE_FIXNUM_0x30: return ({t1})48;
                case MsgPackType.POSITIVE_FIXNUM_0x31: return ({t1})49;
                case MsgPackType.POSITIVE_FIXNUM_0x32: return ({t1})50;
                case MsgPackType.POSITIVE_FIXNUM_0x33: return ({t1})51;
                case MsgPackType.POSITIVE_FIXNUM_0x34: return ({t1})52;
                case MsgPackType.POSITIVE_FIXNUM_0x35: return ({t1})53;
                case MsgPackType.POSITIVE_FIXNUM_0x36: return ({t1})54;
                case MsgPackType.POSITIVE_FIXNUM_0x37: return ({t1})55;
                case MsgPackType.POSITIVE_FIXNUM_0x38: return ({t1})56;
                case MsgPackType.POSITIVE_FIXNUM_0x39: return ({t1})57;
                case MsgPackType.POSITIVE_FIXNUM_0x3A: return ({t1})58;
                case MsgPackType.POSITIVE_FIXNUM_0x3B: return ({t1})59;
                case MsgPackType.POSITIVE_FIXNUM_0x3C: return ({t1})60;
                case MsgPackType.POSITIVE_FIXNUM_0x3D: return ({t1})61;
                case MsgPackType.POSITIVE_FIXNUM_0x3E: return ({t1})62;
                case MsgPackType.POSITIVE_FIXNUM_0x3F: return ({t1})63;

                case MsgPackType.POSITIVE_FIXNUM_0x40: return ({t1})64;
                case MsgPackType.POSITIVE_FIXNUM_0x41: return ({t1})65;
                case MsgPackType.POSITIVE_FIXNUM_0x42: return ({t1})66;
                case MsgPackType.POSITIVE_FIXNUM_0x43: return ({t1})67;
                case MsgPackType.POSITIVE_FIXNUM_0x44: return ({t1})68;
                case MsgPackType.POSITIVE_FIXNUM_0x45: return ({t1})69;
                case MsgPackType.POSITIVE_FIXNUM_0x46: return ({t1})70;
                case MsgPackType.POSITIVE_FIXNUM_0x47: return ({t1})71;
                case MsgPackType.POSITIVE_FIXNUM_0x48: return ({t1})72;
                case MsgPackType.POSITIVE_FIXNUM_0x49: return ({t1})73;
                case MsgPackType.POSITIVE_FIXNUM_0x4A: return ({t1})74; 
                case MsgPackType.POSITIVE_FIXNUM_0x4B: return ({t1})75;
                case MsgPackType.POSITIVE_FIXNUM_0x4C: return ({t1})76;
                case MsgPackType.POSITIVE_FIXNUM_0x4D: return ({t1})77;
                case MsgPackType.POSITIVE_FIXNUM_0x4E: return ({t1})78;
                case MsgPackType.POSITIVE_FIXNUM_0x4F: return ({t1})79;
                     
                case MsgPackType.POSITIVE_FIXNUM_0x50: return ({t1})80;
                case MsgPackType.POSITIVE_FIXNUM_0x51: return ({t1})81;
                case MsgPackType.POSITIVE_FIXNUM_0x52: return ({t1})82;
                case MsgPackType.POSITIVE_FIXNUM_0x53: return ({t1})83;
                case MsgPackType.POSITIVE_FIXNUM_0x54: return ({t1})84;
                case MsgPackType.POSITIVE_FIXNUM_0x55: return ({t1})85;
                case MsgPackType.POSITIVE_FIXNUM_0x56: return ({t1})86;
                case MsgPackType.POSITIVE_FIXNUM_0x57: return ({t1})87;
                case MsgPackType.POSITIVE_FIXNUM_0x58: return ({t1})88;
                case MsgPackType.POSITIVE_FIXNUM_0x59: return ({t1})89;
                case MsgPackType.POSITIVE_FIXNUM_0x5A: return ({t1})90;
                case MsgPackType.POSITIVE_FIXNUM_0x5B: return ({t1})91;
                case MsgPackType.POSITIVE_FIXNUM_0x5C: return ({t1})92;
                case MsgPackType.POSITIVE_FIXNUM_0x5D: return ({t1})93;
                case MsgPackType.POSITIVE_FIXNUM_0x5E: return ({t1})94;
                case MsgPackType.POSITIVE_FIXNUM_0x5F: return ({t1})95;

                case MsgPackType.POSITIVE_FIXNUM_0x60: return ({t1})96;
                case MsgPackType.POSITIVE_FIXNUM_0x61: return ({t1})97;
                case MsgPackType.POSITIVE_FIXNUM_0x62: return ({t1})98;
                case MsgPackType.POSITIVE_FIXNUM_0x63: return ({t1})99;
                case MsgPackType.POSITIVE_FIXNUM_0x64: return ({t1})100;
                case MsgPackType.POSITIVE_FIXNUM_0x65: return ({t1})101;
                case MsgPackType.POSITIVE_FIXNUM_0x66: return ({t1})102;
                case MsgPackType.POSITIVE_FIXNUM_0x67: return ({t1})103;
                case MsgPackType.POSITIVE_FIXNUM_0x68: return ({t1})104;
                case MsgPackType.POSITIVE_FIXNUM_0x69: return ({t1})105;
                case MsgPackType.POSITIVE_FIXNUM_0x6A: return ({t1})106;
                case MsgPackType.POSITIVE_FIXNUM_0x6B: return ({t1})107;
                case MsgPackType.POSITIVE_FIXNUM_0x6C: return ({t1})108;
                case MsgPackType.POSITIVE_FIXNUM_0x6D: return ({t1})109;
                case MsgPackType.POSITIVE_FIXNUM_0x6E: return ({t1})110;
                case MsgPackType.POSITIVE_FIXNUM_0x6F: return ({t1})111;

                case MsgPackType.POSITIVE_FIXNUM_0x70: return ({t1})112;
                case MsgPackType.POSITIVE_FIXNUM_0x71: return ({t1})113;
                case MsgPackType.POSITIVE_FIXNUM_0x72: return ({t1})114;
                case MsgPackType.POSITIVE_FIXNUM_0x73: return ({t1})115;
                case MsgPackType.POSITIVE_FIXNUM_0x74: return ({t1})116;
                case MsgPackType.POSITIVE_FIXNUM_0x75: return ({t1})117;
                case MsgPackType.POSITIVE_FIXNUM_0x76: return ({t1})118;
                case MsgPackType.POSITIVE_FIXNUM_0x77: return ({t1})119;
                case MsgPackType.POSITIVE_FIXNUM_0x78: return ({t1})120;
                case MsgPackType.POSITIVE_FIXNUM_0x79: return ({t1})121;
                case MsgPackType.POSITIVE_FIXNUM_0x7A: return ({t1})122;
                case MsgPackType.POSITIVE_FIXNUM_0x7B: return ({t1})123;
                case MsgPackType.POSITIVE_FIXNUM_0x7C: return ({t1})124;
                case MsgPackType.POSITIVE_FIXNUM_0x7D: return ({t1})125;
                case MsgPackType.POSITIVE_FIXNUM_0x7E: return ({t1})126;
                case MsgPackType.POSITIVE_FIXNUM_0x7F: return ({t1})127;

                case MsgPackType.NEGATIVE_FIXNUM: return ({t1})-32;
                case MsgPackType.NEGATIVE_FIXNUM_0x01: return ({t1})-1;
                case MsgPackType.NEGATIVE_FIXNUM_0x02: return ({t1})-2;
                case MsgPackType.NEGATIVE_FIXNUM_0x03: return ({t1})-3;
                case MsgPackType.NEGATIVE_FIXNUM_0x04: return ({t1})-4;
                case MsgPackType.NEGATIVE_FIXNUM_0x05: return ({t1})-5;
                case MsgPackType.NEGATIVE_FIXNUM_0x06: return ({t1})-6;
                case MsgPackType.NEGATIVE_FIXNUM_0x07: return ({t1})-7;
                case MsgPackType.NEGATIVE_FIXNUM_0x08: return ({t1})-8;
                case MsgPackType.NEGATIVE_FIXNUM_0x09: return ({t1})-9;
                case MsgPackType.NEGATIVE_FIXNUM_0x0A: return ({t1})-10;
                case MsgPackType.NEGATIVE_FIXNUM_0x0B: return ({t1})-11;
                case MsgPackType.NEGATIVE_FIXNUM_0x0C: return ({t1})-12;
                case MsgPackType.NEGATIVE_FIXNUM_0x0D: return ({t1})-13;
                case MsgPackType.NEGATIVE_FIXNUM_0x0E: return ({t1})-14;
                case MsgPackType.NEGATIVE_FIXNUM_0x0F: return ({t1})-15;
                case MsgPackType.NEGATIVE_FIXNUM_0x10: return ({t1})-16;
                case MsgPackType.NEGATIVE_FIXNUM_0x11: return ({t1})-17;
                case MsgPackType.NEGATIVE_FIXNUM_0x12: return ({t1})-18;
                case MsgPackType.NEGATIVE_FIXNUM_0x13: return ({t1})-19;
                case MsgPackType.NEGATIVE_FIXNUM_0x14: return ({t1})-20;
                case MsgPackType.NEGATIVE_FIXNUM_0x15: return ({t1})-21;
                case MsgPackType.NEGATIVE_FIXNUM_0x16: return ({t1})-22;
                case MsgPackType.NEGATIVE_FIXNUM_0x17: return ({t1})-23;
                case MsgPackType.NEGATIVE_FIXNUM_0x18: return ({t1})-24;
                case MsgPackType.NEGATIVE_FIXNUM_0x19: return ({t1})-25;
                case MsgPackType.NEGATIVE_FIXNUM_0x1A: return ({t1})-26;
                case MsgPackType.NEGATIVE_FIXNUM_0x1B: return ({t1})-27;
                case MsgPackType.NEGATIVE_FIXNUM_0x1C: return ({t1})-28;
                case MsgPackType.NEGATIVE_FIXNUM_0x1D: return ({t1})-29;
                case MsgPackType.NEGATIVE_FIXNUM_0x1E: return ({t1})-30;
                case MsgPackType.NEGATIVE_FIXNUM_0x1F: return ({t1})-31;

                case MsgPackType.INT8: return ({t1})(SByte)GetBody().Get(0);
                case MsgPackType.INT16: return ({t1})EndianConverter.NetworkByteWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT32: return ({t1})EndianConverter.NetworkByteDWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT64: return ({t1})EndianConverter.NetworkByteQWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.UINT8: return ({t1})GetBody().Get(0);
                case MsgPackType.UINT16: return ({t1})EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT32: return ({t1})EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT64: return ({t1})EndianConverter.NetworkByteQWordToUnsignedNativeByteOrder(GetBody());

                default: throw new MessagePackValueException("is not {t1} " + Bytes);
            }}
            }}
        }}
'''


if __name__=="__main__":
    body ="".join([create_body(x) for x in [
        ("Byte", "byte"), ("UInt16", "ushort"), ("UInt32", "uint"), ("UInt64", "ulong"),
        ("SByte", "sbyte"), ("Int16", "short"), ("Int32", "int"), ("Int64", "long"),
        ]])

    code=f'''
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Osaru.MessagePack
{{
    public partial struct MessagePackParser: IParser<MessagePackParser>
    {{
        {body}
    }}
}}
'''

    print(code)

