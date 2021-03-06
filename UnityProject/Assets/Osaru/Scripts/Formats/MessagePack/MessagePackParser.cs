﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Osaru.MessagePack
{
    public class MessagePackValueException: ArgumentException
    {
        public MessagePackValueException(string msg) : base(msg) { }
    }

    public partial struct MessagePackParser: IParser<MessagePackParser>
    {
        public ArraySegment<Byte> Bytes { get; private set; }

        public MsgPackType FormatType
        {
            get { return (MsgPackType)Bytes.Get(0); }
        }

        public int Count
        {
            get
            {
                uint count;
                GetItemCount(out count);
                return (int)count;
            }
        }

        public ExtType ExtType
        {
            get
            {
                switch(FormatType)
                {
                    case MsgPackType.FIX_EXT_1: return (ExtType)Bytes.Get(1);
                    case MsgPackType.FIX_EXT_2: return (ExtType)Bytes.Get(1);
                    case MsgPackType.FIX_EXT_4: return (ExtType)Bytes.Get(1);
                    case MsgPackType.FIX_EXT_8: return (ExtType)Bytes.Get(1);
                    case MsgPackType.FIX_EXT_16: return (ExtType)Bytes.Get(1);
                    case MsgPackType.EXT8: return (ExtType)Bytes.Get(2);
                    case MsgPackType.EXT16: return (ExtType)Bytes.Get(3);
                    case MsgPackType.EXT32: return (ExtType)Bytes.Get(5);
                    default: return ExtType.UNKNOWN;
                }
            }
        }

        public IEnumerable<MessagePackParser> ListItems
        {
            get
            {
                if (!FormatType.IsArray())
                {
                    throw new InvalidOperationException("not array");
                }

                uint count;
                var current = GetItemCount(out count);
                for (var i = 0; i < count; ++i)
                {
                    var child = new MessagePackParser(current);
                    current = child._Parse();
                    yield return child;
                }
            }
        }

        public IEnumerable<KeyValuePair<string, MessagePackParser>> ObjectItems
        {
            get
            {
                if (!FormatType.IsMap())
                {
                    throw new InvalidOperationException("not map");
                }

                uint count;
                var current = GetItemCount(out count);
                var childCount = count * 2;
                for (var i = 0; i < childCount; i += 2)
                {
                    var key = new MessagePackParser(current);
                    current = key._Parse();
                    var value = new MessagePackParser(current);
                    current = value._Parse();
                    yield return new KeyValuePair<string, MessagePackParser>(key.GetString(), value);
                }
            }
        }

        public MessagePackParser GetValueByIntKey(int target)
        {
            if (!FormatType.IsMap())
            {
                throw new InvalidOperationException("not map");
            }

            uint count;
            var current = GetItemCount(out count);
            var childCount = count * 2;
            for (var i = 0; i < childCount; i += 2)
            {
                var key = new MessagePackParser(current);
                current = key._Parse();
                var value = new MessagePackParser(current);
                current = value._Parse();
                if (key.GetInt32() == target)
                {
                    return value;
                }
            }

            throw new KeyNotFoundException();
        }

        public ParserValueType ValueType
        {
            get
            {
                return FormatType.ToParserValueType();
            }
        }

        public bool IsNull
        {
            get
            {
                return FormatType == MsgPackType.NIL;
            }
        }

        public MessagePackParser this[int index]
        {
            get
            {
                return ListItems.Skip(index).First();
            }
        }

        public MessagePackParser this[string key]
        {
            get
            {
                foreach(var kv in ObjectItems)
                {
                    if (kv.Key == key)
                    {
                        return kv.Value;
                    }
                }

                throw new KeyNotFoundException();
            }
        }

        public static MessagePackParser Parse(Byte[] bytes)
        {
            return Parse(new ArraySegment<Byte>(bytes));
        }
        public static MessagePackParser Parse(ArraySegment<Byte> bytes)
        {
            var value = new MessagePackParser(bytes);
            value._Parse();
            return value;
        }

        MessagePackParser(Byte[] bytes) : this(new ArraySegment<Byte>(bytes))
        { }

        MessagePackParser(ArraySegment<Byte> bytes)
        {
            Bytes = bytes;
        }

        public void SetBytes(ArraySegment<Byte> bytes)
        {
            Bytes = bytes;
        }

        ArraySegment<Byte> _Parse()
        {
            ArraySegment<Byte> current;
            if (FormatType.IsArray())
            {
                uint count;
                current=GetItemCount(out count);
                //Children = new MsgPackValue[count];
                for(var i=0; i<count; ++i)
                { 
                    var child = new MessagePackParser(current);
                    current = child._Parse();
                }
            }
            else if (FormatType.IsMap())
            {
                uint count;
                current = GetItemCount(out count);
                //Children = new MsgPackValue[count * 2];
                var childCount = count * 2;
                for (var i = 0; i < childCount; ++i)
                {
                    var child = new MessagePackParser(current);
                    current = child._Parse();
                }
            }
            else
            {
                var body = GetBody();
                var header = body.Offset - Bytes.Offset;
                current = Bytes.Advance(header + body.Count);
            }
            var size = current.Offset - Bytes.Offset;
            var result= Bytes.Advance(size);
            Bytes = Bytes.Take(size);
            return result;
        }

        /// <summary>
        /// Array又はMapの子要素の数を得る
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        ArraySegment<Byte> GetItemCount(out UInt32 count)
        {
            switch (FormatType)
            {
                case MsgPackType.FIX_ARRAY: count = 0; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0x1: count = 1; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0x2: count = 2; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0x3: count = 3; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0x4: count = 4; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0x5: count = 5; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0x6: count = 6; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0x7: count = 7; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0x8: count = 8; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0x9: count = 9; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0xA: count = 10; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0xB: count = 11; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0xC: count = 12; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0xD: count = 13; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0xE: count = 14; return Bytes.Advance(1);
                case MsgPackType.FIX_ARRAY_0xF: count = 15; return Bytes.Advance(1);

                case MsgPackType.FIX_MAP: count = 0; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0x1: count = 1; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0x2: count = 2; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0x3: count = 3; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0x4: count = 4; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0x5: count = 5; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0x6: count = 6; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0x7: count = 7; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0x8: count = 8; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0x9: count = 9; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0xA: count = 10; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0xB: count = 11; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0xC: count = 12; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0xD: count = 13; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0xE: count = 14; return Bytes.Advance(1);
                case MsgPackType.FIX_MAP_0xF: count = 15; return Bytes.Advance(1);

                case MsgPackType.ARRAY16:
                case MsgPackType.MAP16:
                    count = EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(Bytes.Advance(1));
                    return Bytes.Advance(1 + 2);

                case MsgPackType.ARRAY32:
                case MsgPackType.MAP32:
                    count = EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(Bytes.Advance(1));
                    return Bytes.Advance(1 + 4);

                default:
                    throw new ArgumentException("is not collection: "+FormatType);
            }
        }

        /// <summary>
        /// ArrayとMap以外のタイプのペイロードを得る
        /// </summary>
        /// <returns></returns>
        public ArraySegment<Byte> GetBody()
        {
            switch (FormatType)
            {
                case MsgPackType.FIX_STR: return Bytes.Advance(1).Take(0);
                case MsgPackType.FIX_STR_0x01: return Bytes.Advance(1).Take(1);
                case MsgPackType.FIX_STR_0x02: return Bytes.Advance(1).Take(2);
                case MsgPackType.FIX_STR_0x03: return Bytes.Advance(1).Take(3);
                case MsgPackType.FIX_STR_0x04: return Bytes.Advance(1).Take(4);
                case MsgPackType.FIX_STR_0x05: return Bytes.Advance(1).Take(5);
                case MsgPackType.FIX_STR_0x06: return Bytes.Advance(1).Take(6);
                case MsgPackType.FIX_STR_0x07: return Bytes.Advance(1).Take(7);
                case MsgPackType.FIX_STR_0x08: return Bytes.Advance(1).Take(8);
                case MsgPackType.FIX_STR_0x09: return Bytes.Advance(1).Take(9);
                case MsgPackType.FIX_STR_0x0A: return Bytes.Advance(1).Take(10);
                case MsgPackType.FIX_STR_0x0B: return Bytes.Advance(1).Take(11);
                case MsgPackType.FIX_STR_0x0C: return Bytes.Advance(1).Take(12);
                case MsgPackType.FIX_STR_0x0D: return Bytes.Advance(1).Take(13);
                case MsgPackType.FIX_STR_0x0E: return Bytes.Advance(1).Take(14);
                case MsgPackType.FIX_STR_0x0F: return Bytes.Advance(1).Take(15);

                case MsgPackType.FIX_STR_0x10: return Bytes.Advance(1).Take(16);
                case MsgPackType.FIX_STR_0x11: return Bytes.Advance(1).Take(17);
                case MsgPackType.FIX_STR_0x12: return Bytes.Advance(1).Take(18);
                case MsgPackType.FIX_STR_0x13: return Bytes.Advance(1).Take(19);
                case MsgPackType.FIX_STR_0x14: return Bytes.Advance(1).Take(20);
                case MsgPackType.FIX_STR_0x15: return Bytes.Advance(1).Take(21);
                case MsgPackType.FIX_STR_0x16: return Bytes.Advance(1).Take(22);
                case MsgPackType.FIX_STR_0x17: return Bytes.Advance(1).Take(23);
                case MsgPackType.FIX_STR_0x18: return Bytes.Advance(1).Take(24);
                case MsgPackType.FIX_STR_0x19: return Bytes.Advance(1).Take(25);
                case MsgPackType.FIX_STR_0x1A: return Bytes.Advance(1).Take(26);
                case MsgPackType.FIX_STR_0x1B: return Bytes.Advance(1).Take(27);
                case MsgPackType.FIX_STR_0x1C: return Bytes.Advance(1).Take(28);
                case MsgPackType.FIX_STR_0x1D: return Bytes.Advance(1).Take(29);
                case MsgPackType.FIX_STR_0x1E: return Bytes.Advance(1).Take(30);
                case MsgPackType.FIX_STR_0x1F: return Bytes.Advance(1).Take(31);

                case MsgPackType.STR8:
                case MsgPackType.BIN8:
                    {
                        var count = Bytes.Get(1);
                        return Bytes.Advance(1 + 1).Take(count);
                    }

                case MsgPackType.STR16:
                case MsgPackType.BIN16:
                    {
                        var count = EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(Bytes.Advance(1));
                        return Bytes.Advance(1 + 2).Take(count);
                    }

                case MsgPackType.STR32:
                case MsgPackType.BIN32:
                    {
                        var count = EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(Bytes.Advance(1));
                        return Bytes.Advance(1 + 4).Take((int)count);
                    }

                case MsgPackType.NIL:
                case MsgPackType.TRUE:
                case MsgPackType.FALSE:
                case MsgPackType.POSITIVE_FIXNUM:
                case MsgPackType.POSITIVE_FIXNUM_0x01:
                case MsgPackType.POSITIVE_FIXNUM_0x02:
                case MsgPackType.POSITIVE_FIXNUM_0x03:
                case MsgPackType.POSITIVE_FIXNUM_0x04:
                case MsgPackType.POSITIVE_FIXNUM_0x05:
                case MsgPackType.POSITIVE_FIXNUM_0x06:
                case MsgPackType.POSITIVE_FIXNUM_0x07:
                case MsgPackType.POSITIVE_FIXNUM_0x08:
                case MsgPackType.POSITIVE_FIXNUM_0x09:
                case MsgPackType.POSITIVE_FIXNUM_0x0A:
                case MsgPackType.POSITIVE_FIXNUM_0x0B:
                case MsgPackType.POSITIVE_FIXNUM_0x0C:
                case MsgPackType.POSITIVE_FIXNUM_0x0D:
                case MsgPackType.POSITIVE_FIXNUM_0x0E:
                case MsgPackType.POSITIVE_FIXNUM_0x0F:

                case MsgPackType.POSITIVE_FIXNUM_0x10:
                case MsgPackType.POSITIVE_FIXNUM_0x11:
                case MsgPackType.POSITIVE_FIXNUM_0x12:
                case MsgPackType.POSITIVE_FIXNUM_0x13:
                case MsgPackType.POSITIVE_FIXNUM_0x14:
                case MsgPackType.POSITIVE_FIXNUM_0x15:
                case MsgPackType.POSITIVE_FIXNUM_0x16:
                case MsgPackType.POSITIVE_FIXNUM_0x17:
                case MsgPackType.POSITIVE_FIXNUM_0x18:
                case MsgPackType.POSITIVE_FIXNUM_0x19:
                case MsgPackType.POSITIVE_FIXNUM_0x1A:
                case MsgPackType.POSITIVE_FIXNUM_0x1B:
                case MsgPackType.POSITIVE_FIXNUM_0x1C:
                case MsgPackType.POSITIVE_FIXNUM_0x1D:
                case MsgPackType.POSITIVE_FIXNUM_0x1E:
                case MsgPackType.POSITIVE_FIXNUM_0x1F:

                case MsgPackType.POSITIVE_FIXNUM_0x20:
                case MsgPackType.POSITIVE_FIXNUM_0x21:
                case MsgPackType.POSITIVE_FIXNUM_0x22:
                case MsgPackType.POSITIVE_FIXNUM_0x23:
                case MsgPackType.POSITIVE_FIXNUM_0x24:
                case MsgPackType.POSITIVE_FIXNUM_0x25:
                case MsgPackType.POSITIVE_FIXNUM_0x26:
                case MsgPackType.POSITIVE_FIXNUM_0x27:
                case MsgPackType.POSITIVE_FIXNUM_0x28:
                case MsgPackType.POSITIVE_FIXNUM_0x29:
                case MsgPackType.POSITIVE_FIXNUM_0x2A:
                case MsgPackType.POSITIVE_FIXNUM_0x2B:
                case MsgPackType.POSITIVE_FIXNUM_0x2C:
                case MsgPackType.POSITIVE_FIXNUM_0x2D:
                case MsgPackType.POSITIVE_FIXNUM_0x2E:
                case MsgPackType.POSITIVE_FIXNUM_0x2F:

                case MsgPackType.POSITIVE_FIXNUM_0x30:
                case MsgPackType.POSITIVE_FIXNUM_0x31:
                case MsgPackType.POSITIVE_FIXNUM_0x32:
                case MsgPackType.POSITIVE_FIXNUM_0x33:
                case MsgPackType.POSITIVE_FIXNUM_0x34:
                case MsgPackType.POSITIVE_FIXNUM_0x35:
                case MsgPackType.POSITIVE_FIXNUM_0x36:
                case MsgPackType.POSITIVE_FIXNUM_0x37:
                case MsgPackType.POSITIVE_FIXNUM_0x38:
                case MsgPackType.POSITIVE_FIXNUM_0x39:
                case MsgPackType.POSITIVE_FIXNUM_0x3A:
                case MsgPackType.POSITIVE_FIXNUM_0x3B:
                case MsgPackType.POSITIVE_FIXNUM_0x3C:
                case MsgPackType.POSITIVE_FIXNUM_0x3D:
                case MsgPackType.POSITIVE_FIXNUM_0x3E:
                case MsgPackType.POSITIVE_FIXNUM_0x3F:

                case MsgPackType.POSITIVE_FIXNUM_0x40:
                case MsgPackType.POSITIVE_FIXNUM_0x41:
                case MsgPackType.POSITIVE_FIXNUM_0x42:
                case MsgPackType.POSITIVE_FIXNUM_0x43:
                case MsgPackType.POSITIVE_FIXNUM_0x44:
                case MsgPackType.POSITIVE_FIXNUM_0x45:
                case MsgPackType.POSITIVE_FIXNUM_0x46:
                case MsgPackType.POSITIVE_FIXNUM_0x47:
                case MsgPackType.POSITIVE_FIXNUM_0x48:
                case MsgPackType.POSITIVE_FIXNUM_0x49:
                case MsgPackType.POSITIVE_FIXNUM_0x4A:
                case MsgPackType.POSITIVE_FIXNUM_0x4B:
                case MsgPackType.POSITIVE_FIXNUM_0x4C:
                case MsgPackType.POSITIVE_FIXNUM_0x4D:
                case MsgPackType.POSITIVE_FIXNUM_0x4E:
                case MsgPackType.POSITIVE_FIXNUM_0x4F:

                case MsgPackType.POSITIVE_FIXNUM_0x50:
                case MsgPackType.POSITIVE_FIXNUM_0x51:
                case MsgPackType.POSITIVE_FIXNUM_0x52:
                case MsgPackType.POSITIVE_FIXNUM_0x53:
                case MsgPackType.POSITIVE_FIXNUM_0x54:
                case MsgPackType.POSITIVE_FIXNUM_0x55:
                case MsgPackType.POSITIVE_FIXNUM_0x56:
                case MsgPackType.POSITIVE_FIXNUM_0x57:
                case MsgPackType.POSITIVE_FIXNUM_0x58:
                case MsgPackType.POSITIVE_FIXNUM_0x59:
                case MsgPackType.POSITIVE_FIXNUM_0x5A:
                case MsgPackType.POSITIVE_FIXNUM_0x5B:
                case MsgPackType.POSITIVE_FIXNUM_0x5C:
                case MsgPackType.POSITIVE_FIXNUM_0x5D:
                case MsgPackType.POSITIVE_FIXNUM_0x5E:
                case MsgPackType.POSITIVE_FIXNUM_0x5F:

                case MsgPackType.POSITIVE_FIXNUM_0x60:
                case MsgPackType.POSITIVE_FIXNUM_0x61:
                case MsgPackType.POSITIVE_FIXNUM_0x62:
                case MsgPackType.POSITIVE_FIXNUM_0x63:
                case MsgPackType.POSITIVE_FIXNUM_0x64:
                case MsgPackType.POSITIVE_FIXNUM_0x65:
                case MsgPackType.POSITIVE_FIXNUM_0x66:
                case MsgPackType.POSITIVE_FIXNUM_0x67:
                case MsgPackType.POSITIVE_FIXNUM_0x68:
                case MsgPackType.POSITIVE_FIXNUM_0x69:
                case MsgPackType.POSITIVE_FIXNUM_0x6A:
                case MsgPackType.POSITIVE_FIXNUM_0x6B:
                case MsgPackType.POSITIVE_FIXNUM_0x6C:
                case MsgPackType.POSITIVE_FIXNUM_0x6D:
                case MsgPackType.POSITIVE_FIXNUM_0x6E:
                case MsgPackType.POSITIVE_FIXNUM_0x6F:

                case MsgPackType.POSITIVE_FIXNUM_0x70:
                case MsgPackType.POSITIVE_FIXNUM_0x71:
                case MsgPackType.POSITIVE_FIXNUM_0x72:
                case MsgPackType.POSITIVE_FIXNUM_0x73:
                case MsgPackType.POSITIVE_FIXNUM_0x74:
                case MsgPackType.POSITIVE_FIXNUM_0x75:
                case MsgPackType.POSITIVE_FIXNUM_0x76:
                case MsgPackType.POSITIVE_FIXNUM_0x77:
                case MsgPackType.POSITIVE_FIXNUM_0x78:
                case MsgPackType.POSITIVE_FIXNUM_0x79:
                case MsgPackType.POSITIVE_FIXNUM_0x7A:
                case MsgPackType.POSITIVE_FIXNUM_0x7B:
                case MsgPackType.POSITIVE_FIXNUM_0x7C:
                case MsgPackType.POSITIVE_FIXNUM_0x7D:
                case MsgPackType.POSITIVE_FIXNUM_0x7E:
                case MsgPackType.POSITIVE_FIXNUM_0x7F:

                case MsgPackType.NEGATIVE_FIXNUM:
                case MsgPackType.NEGATIVE_FIXNUM_0x01:
                case MsgPackType.NEGATIVE_FIXNUM_0x02:
                case MsgPackType.NEGATIVE_FIXNUM_0x03:
                case MsgPackType.NEGATIVE_FIXNUM_0x04:
                case MsgPackType.NEGATIVE_FIXNUM_0x05:
                case MsgPackType.NEGATIVE_FIXNUM_0x06:
                case MsgPackType.NEGATIVE_FIXNUM_0x07:
                case MsgPackType.NEGATIVE_FIXNUM_0x08:
                case MsgPackType.NEGATIVE_FIXNUM_0x09:
                case MsgPackType.NEGATIVE_FIXNUM_0x0A:
                case MsgPackType.NEGATIVE_FIXNUM_0x0B:
                case MsgPackType.NEGATIVE_FIXNUM_0x0C:
                case MsgPackType.NEGATIVE_FIXNUM_0x0D:
                case MsgPackType.NEGATIVE_FIXNUM_0x0E:
                case MsgPackType.NEGATIVE_FIXNUM_0x0F:
                case MsgPackType.NEGATIVE_FIXNUM_0x10:
                case MsgPackType.NEGATIVE_FIXNUM_0x11:
                case MsgPackType.NEGATIVE_FIXNUM_0x12:
                case MsgPackType.NEGATIVE_FIXNUM_0x13:
                case MsgPackType.NEGATIVE_FIXNUM_0x14:
                case MsgPackType.NEGATIVE_FIXNUM_0x15:
                case MsgPackType.NEGATIVE_FIXNUM_0x16:
                case MsgPackType.NEGATIVE_FIXNUM_0x17:
                case MsgPackType.NEGATIVE_FIXNUM_0x18:
                case MsgPackType.NEGATIVE_FIXNUM_0x19:
                case MsgPackType.NEGATIVE_FIXNUM_0x1A:
                case MsgPackType.NEGATIVE_FIXNUM_0x1B:
                case MsgPackType.NEGATIVE_FIXNUM_0x1C:
                case MsgPackType.NEGATIVE_FIXNUM_0x1D:
                case MsgPackType.NEGATIVE_FIXNUM_0x1E:
                case MsgPackType.NEGATIVE_FIXNUM_0x1F:
                    return Bytes.Advance(1).Take(0);

                case MsgPackType.UINT8:
                case MsgPackType.INT8:
                    return Bytes.Advance(1).Take(1);

                case MsgPackType.UINT16:
                case MsgPackType.INT16:
                    return Bytes.Advance(1).Take(2);

                case MsgPackType.UINT32:
                case MsgPackType.INT32:
                case MsgPackType.FLOAT:
                    return Bytes.Advance(1).Take(4);

                case MsgPackType.UINT64:
                case MsgPackType.INT64:
                case MsgPackType.DOUBLE:
                    return Bytes.Advance(1).Take(8);

                case MsgPackType.FIX_EXT_1:
                    return Bytes.Advance(2).Take(1);
                case MsgPackType.FIX_EXT_2:
                    return Bytes.Advance(2).Take(2);
                case MsgPackType.FIX_EXT_4:
                    return Bytes.Advance(2).Take(4);
                case MsgPackType.FIX_EXT_8:
                    return Bytes.Advance(2).Take(8);
                case MsgPackType.FIX_EXT_16:
                    return Bytes.Advance(2).Take(16);
                case MsgPackType.EXT8:
                    {
                        var count = Bytes.Get(1);
                        return Bytes.Advance(1 + 1 + 1).Take(count);
                    }
                case MsgPackType.EXT16:
                    {
                        var count = EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(Bytes.Advance(1));
                        return Bytes.Advance(1 + 2 + 1).Take(count);
                    }
                case MsgPackType.EXT32:
                    {
                        var count = EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(Bytes.Advance(1));
                        return Bytes.Advance(1 + 4 + 1).Take((int)count);
                    }
                default:
                    throw new ArgumentException("unknown type: " + FormatType);
            }
        }

        /// <summary>
        /// ArrayとMap以外のタイプの値を得る
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            switch(FormatType)
            {
                case MsgPackType.NIL: return null;
                case MsgPackType.TRUE: return true;
                case MsgPackType.FALSE: return false;
                case MsgPackType.POSITIVE_FIXNUM: return 0;
                case MsgPackType.POSITIVE_FIXNUM_0x01: return 1;
                case MsgPackType.POSITIVE_FIXNUM_0x02: return 2;
                case MsgPackType.POSITIVE_FIXNUM_0x03: return 3;
                case MsgPackType.POSITIVE_FIXNUM_0x04: return 4;
                case MsgPackType.POSITIVE_FIXNUM_0x05: return 5;
                case MsgPackType.POSITIVE_FIXNUM_0x06: return 6;
                case MsgPackType.POSITIVE_FIXNUM_0x07: return 7;
                case MsgPackType.POSITIVE_FIXNUM_0x08: return 8;
                case MsgPackType.POSITIVE_FIXNUM_0x09: return 9;
                case MsgPackType.POSITIVE_FIXNUM_0x0A: return 10;
                case MsgPackType.POSITIVE_FIXNUM_0x0B: return 11;
                case MsgPackType.POSITIVE_FIXNUM_0x0C: return 12;
                case MsgPackType.POSITIVE_FIXNUM_0x0D: return 13;
                case MsgPackType.POSITIVE_FIXNUM_0x0E: return 14;
                case MsgPackType.POSITIVE_FIXNUM_0x0F: return 15;

                case MsgPackType.POSITIVE_FIXNUM_0x10: return 16;
                case MsgPackType.POSITIVE_FIXNUM_0x11: return 17;
                case MsgPackType.POSITIVE_FIXNUM_0x12: return 18;
                case MsgPackType.POSITIVE_FIXNUM_0x13: return 19;
                case MsgPackType.POSITIVE_FIXNUM_0x14: return 20;
                case MsgPackType.POSITIVE_FIXNUM_0x15: return 21;
                case MsgPackType.POSITIVE_FIXNUM_0x16: return 22;
                case MsgPackType.POSITIVE_FIXNUM_0x17: return 23;
                case MsgPackType.POSITIVE_FIXNUM_0x18: return 24;
                case MsgPackType.POSITIVE_FIXNUM_0x19: return 25;
                case MsgPackType.POSITIVE_FIXNUM_0x1A: return 26;
                case MsgPackType.POSITIVE_FIXNUM_0x1B: return 27;
                case MsgPackType.POSITIVE_FIXNUM_0x1C: return 28;
                case MsgPackType.POSITIVE_FIXNUM_0x1D: return 29;
                case MsgPackType.POSITIVE_FIXNUM_0x1E: return 30;
                case MsgPackType.POSITIVE_FIXNUM_0x1F: return 31;

                case MsgPackType.POSITIVE_FIXNUM_0x20: return 32;
                case MsgPackType.POSITIVE_FIXNUM_0x21: return 33;
                case MsgPackType.POSITIVE_FIXNUM_0x22: return 34;
                case MsgPackType.POSITIVE_FIXNUM_0x23: return 35;
                case MsgPackType.POSITIVE_FIXNUM_0x24: return 36;
                case MsgPackType.POSITIVE_FIXNUM_0x25: return 37;
                case MsgPackType.POSITIVE_FIXNUM_0x26: return 38;
                case MsgPackType.POSITIVE_FIXNUM_0x27: return 39;
                case MsgPackType.POSITIVE_FIXNUM_0x28: return 40;
                case MsgPackType.POSITIVE_FIXNUM_0x29: return 41;
                case MsgPackType.POSITIVE_FIXNUM_0x2A: return 42;
                case MsgPackType.POSITIVE_FIXNUM_0x2B: return 43;
                case MsgPackType.POSITIVE_FIXNUM_0x2C: return 44;
                case MsgPackType.POSITIVE_FIXNUM_0x2D: return 45;
                case MsgPackType.POSITIVE_FIXNUM_0x2E: return 46;
                case MsgPackType.POSITIVE_FIXNUM_0x2F: return 47;

                case MsgPackType.POSITIVE_FIXNUM_0x30: return 48;
                case MsgPackType.POSITIVE_FIXNUM_0x31: return 49;
                case MsgPackType.POSITIVE_FIXNUM_0x32: return 50;
                case MsgPackType.POSITIVE_FIXNUM_0x33: return 51;
                case MsgPackType.POSITIVE_FIXNUM_0x34: return 52;
                case MsgPackType.POSITIVE_FIXNUM_0x35: return 53;
                case MsgPackType.POSITIVE_FIXNUM_0x36: return 54;
                case MsgPackType.POSITIVE_FIXNUM_0x37: return 55;
                case MsgPackType.POSITIVE_FIXNUM_0x38: return 56;
                case MsgPackType.POSITIVE_FIXNUM_0x39: return 57;
                case MsgPackType.POSITIVE_FIXNUM_0x3A: return 58;
                case MsgPackType.POSITIVE_FIXNUM_0x3B: return 59;
                case MsgPackType.POSITIVE_FIXNUM_0x3C: return 60;
                case MsgPackType.POSITIVE_FIXNUM_0x3D: return 61;
                case MsgPackType.POSITIVE_FIXNUM_0x3E: return 62;
                case MsgPackType.POSITIVE_FIXNUM_0x3F: return 63;

                case MsgPackType.POSITIVE_FIXNUM_0x40: return 64;
                case MsgPackType.POSITIVE_FIXNUM_0x41: return 65;
                case MsgPackType.POSITIVE_FIXNUM_0x42: return 66;
                case MsgPackType.POSITIVE_FIXNUM_0x43: return 67;
                case MsgPackType.POSITIVE_FIXNUM_0x44: return 68;
                case MsgPackType.POSITIVE_FIXNUM_0x45: return 69;
                case MsgPackType.POSITIVE_FIXNUM_0x46: return 70;
                case MsgPackType.POSITIVE_FIXNUM_0x47: return 71;
                case MsgPackType.POSITIVE_FIXNUM_0x48: return 72;
                case MsgPackType.POSITIVE_FIXNUM_0x49: return 73;
                case MsgPackType.POSITIVE_FIXNUM_0x4A: return 74; 
                case MsgPackType.POSITIVE_FIXNUM_0x4B: return 75;
                case MsgPackType.POSITIVE_FIXNUM_0x4C: return 76;
                case MsgPackType.POSITIVE_FIXNUM_0x4D: return 77;
                case MsgPackType.POSITIVE_FIXNUM_0x4E: return 78;
                case MsgPackType.POSITIVE_FIXNUM_0x4F: return 79;
                     
                case MsgPackType.POSITIVE_FIXNUM_0x50: return 80;
                case MsgPackType.POSITIVE_FIXNUM_0x51: return 81;
                case MsgPackType.POSITIVE_FIXNUM_0x52: return 82;
                case MsgPackType.POSITIVE_FIXNUM_0x53: return 83;
                case MsgPackType.POSITIVE_FIXNUM_0x54: return 84;
                case MsgPackType.POSITIVE_FIXNUM_0x55: return 85;
                case MsgPackType.POSITIVE_FIXNUM_0x56: return 86;
                case MsgPackType.POSITIVE_FIXNUM_0x57: return 87;
                case MsgPackType.POSITIVE_FIXNUM_0x58: return 88;
                case MsgPackType.POSITIVE_FIXNUM_0x59: return 89;
                case MsgPackType.POSITIVE_FIXNUM_0x5A: return 90;
                case MsgPackType.POSITIVE_FIXNUM_0x5B: return 91;
                case MsgPackType.POSITIVE_FIXNUM_0x5C: return 92;
                case MsgPackType.POSITIVE_FIXNUM_0x5D: return 93;
                case MsgPackType.POSITIVE_FIXNUM_0x5E: return 94;
                case MsgPackType.POSITIVE_FIXNUM_0x5F: return 95;

                case MsgPackType.POSITIVE_FIXNUM_0x60: return 96;
                case MsgPackType.POSITIVE_FIXNUM_0x61: return 97;
                case MsgPackType.POSITIVE_FIXNUM_0x62: return 98;
                case MsgPackType.POSITIVE_FIXNUM_0x63: return 99;
                case MsgPackType.POSITIVE_FIXNUM_0x64: return 100;
                case MsgPackType.POSITIVE_FIXNUM_0x65: return 101;
                case MsgPackType.POSITIVE_FIXNUM_0x66: return 102;
                case MsgPackType.POSITIVE_FIXNUM_0x67: return 103;
                case MsgPackType.POSITIVE_FIXNUM_0x68: return 104;
                case MsgPackType.POSITIVE_FIXNUM_0x69: return 105;
                case MsgPackType.POSITIVE_FIXNUM_0x6A: return 106;
                case MsgPackType.POSITIVE_FIXNUM_0x6B: return 107;
                case MsgPackType.POSITIVE_FIXNUM_0x6C: return 108;
                case MsgPackType.POSITIVE_FIXNUM_0x6D: return 109;
                case MsgPackType.POSITIVE_FIXNUM_0x6E: return 110;
                case MsgPackType.POSITIVE_FIXNUM_0x6F: return 111;

                case MsgPackType.POSITIVE_FIXNUM_0x70: return 112;
                case MsgPackType.POSITIVE_FIXNUM_0x71: return 113;
                case MsgPackType.POSITIVE_FIXNUM_0x72: return 114;
                case MsgPackType.POSITIVE_FIXNUM_0x73: return 115;
                case MsgPackType.POSITIVE_FIXNUM_0x74: return 116;
                case MsgPackType.POSITIVE_FIXNUM_0x75: return 117;
                case MsgPackType.POSITIVE_FIXNUM_0x76: return 118;
                case MsgPackType.POSITIVE_FIXNUM_0x77: return 119;
                case MsgPackType.POSITIVE_FIXNUM_0x78: return 120;
                case MsgPackType.POSITIVE_FIXNUM_0x79: return 121;
                case MsgPackType.POSITIVE_FIXNUM_0x7A: return 122;
                case MsgPackType.POSITIVE_FIXNUM_0x7B: return 123;
                case MsgPackType.POSITIVE_FIXNUM_0x7C: return 124;
                case MsgPackType.POSITIVE_FIXNUM_0x7D: return 125;
                case MsgPackType.POSITIVE_FIXNUM_0x7E: return 126;
                case MsgPackType.POSITIVE_FIXNUM_0x7F: return 127;

                case MsgPackType.NEGATIVE_FIXNUM: return -32;
                case MsgPackType.NEGATIVE_FIXNUM_0x01: return -1;
                case MsgPackType.NEGATIVE_FIXNUM_0x02: return -2;
                case MsgPackType.NEGATIVE_FIXNUM_0x03: return -3;
                case MsgPackType.NEGATIVE_FIXNUM_0x04: return -4;
                case MsgPackType.NEGATIVE_FIXNUM_0x05: return -5;
                case MsgPackType.NEGATIVE_FIXNUM_0x06: return -6;
                case MsgPackType.NEGATIVE_FIXNUM_0x07: return -7;
                case MsgPackType.NEGATIVE_FIXNUM_0x08: return -8;
                case MsgPackType.NEGATIVE_FIXNUM_0x09: return -9;
                case MsgPackType.NEGATIVE_FIXNUM_0x0A: return -10;
                case MsgPackType.NEGATIVE_FIXNUM_0x0B: return -11;
                case MsgPackType.NEGATIVE_FIXNUM_0x0C: return -12;
                case MsgPackType.NEGATIVE_FIXNUM_0x0D: return -13;
                case MsgPackType.NEGATIVE_FIXNUM_0x0E: return -14;
                case MsgPackType.NEGATIVE_FIXNUM_0x0F: return -15;
                case MsgPackType.NEGATIVE_FIXNUM_0x10: return -16;
                case MsgPackType.NEGATIVE_FIXNUM_0x11: return -17;
                case MsgPackType.NEGATIVE_FIXNUM_0x12: return -18;
                case MsgPackType.NEGATIVE_FIXNUM_0x13: return -19;
                case MsgPackType.NEGATIVE_FIXNUM_0x14: return -20;
                case MsgPackType.NEGATIVE_FIXNUM_0x15: return -21;
                case MsgPackType.NEGATIVE_FIXNUM_0x16: return -22;
                case MsgPackType.NEGATIVE_FIXNUM_0x17: return -23;
                case MsgPackType.NEGATIVE_FIXNUM_0x18: return -24;
                case MsgPackType.NEGATIVE_FIXNUM_0x19: return -25;
                case MsgPackType.NEGATIVE_FIXNUM_0x1A: return -26;
                case MsgPackType.NEGATIVE_FIXNUM_0x1B: return -27;
                case MsgPackType.NEGATIVE_FIXNUM_0x1C: return -28;
                case MsgPackType.NEGATIVE_FIXNUM_0x1D: return -29;
                case MsgPackType.NEGATIVE_FIXNUM_0x1E: return -30;
                case MsgPackType.NEGATIVE_FIXNUM_0x1F: return -31;

                case MsgPackType.INT8: return (SByte)GetBody().Get(0);
                case MsgPackType.INT16: return EndianConverter.NetworkByteWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT32: return EndianConverter.NetworkByteDWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.INT64: return EndianConverter.NetworkByteQWordToSignedNativeByteOrder(GetBody());
                case MsgPackType.UINT8: return GetBody().Get(0);
                case MsgPackType.UINT16: return EndianConverter.NetworkByteWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT32: return EndianConverter.NetworkByteDWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.UINT64: return EndianConverter.NetworkByteQWordToUnsignedNativeByteOrder(GetBody());
                case MsgPackType.FLOAT: return EndianConverter.NetworkByteDWordToFloatNativeByteOrder(GetBody());
                case MsgPackType.DOUBLE: return EndianConverter.NetworkByteQWordToFloatNativeByteOrder(GetBody());

                case MsgPackType.FIX_STR: return "";
                case MsgPackType.FIX_STR_0x01:
                case MsgPackType.FIX_STR_0x02:
                case MsgPackType.FIX_STR_0x03:
                case MsgPackType.FIX_STR_0x04:
                case MsgPackType.FIX_STR_0x05:
                case MsgPackType.FIX_STR_0x06:
                case MsgPackType.FIX_STR_0x07:
                case MsgPackType.FIX_STR_0x08:
                case MsgPackType.FIX_STR_0x09:
                case MsgPackType.FIX_STR_0x0A:
                case MsgPackType.FIX_STR_0x0B:
                case MsgPackType.FIX_STR_0x0C:
                case MsgPackType.FIX_STR_0x0D:
                case MsgPackType.FIX_STR_0x0E:
                case MsgPackType.FIX_STR_0x0F:
                case MsgPackType.FIX_STR_0x10:
                case MsgPackType.FIX_STR_0x11:
                case MsgPackType.FIX_STR_0x12:
                case MsgPackType.FIX_STR_0x13:
                case MsgPackType.FIX_STR_0x14:
                case MsgPackType.FIX_STR_0x15:
                case MsgPackType.FIX_STR_0x16:
                case MsgPackType.FIX_STR_0x17:
                case MsgPackType.FIX_STR_0x18:
                case MsgPackType.FIX_STR_0x19:
                case MsgPackType.FIX_STR_0x1A:
                case MsgPackType.FIX_STR_0x1B:
                case MsgPackType.FIX_STR_0x1C:
                case MsgPackType.FIX_STR_0x1D:
                case MsgPackType.FIX_STR_0x1E:
                case MsgPackType.FIX_STR_0x1F:
                case MsgPackType.STR8:
                case MsgPackType.STR16:
                case MsgPackType.STR32:
                    {
                        var body = GetBody();
                        var str = Encoding.UTF8.GetString(body.Array, body.Offset, body.Count);
                        return str;
                    }

                case MsgPackType.BIN8:
                case MsgPackType.BIN16:
                case MsgPackType.BIN32:
                    {
                        var body = GetBody();
                        return body;
                    }

                default:
                    throw new ArgumentException("GetValue to array or map: " + FormatType);
            }
        }

        public override string ToString()
        {
            if (FormatType.IsArray())
            {
                return "array(" + Count + ")";
            }
            else if (FormatType.IsMap())
            {
                return "map(" + Count + ")";
            }
            else if(FormatType.IsExt())
            {
                return "ext";
            }
            else if(FormatType.IsBinary())
            {
                return "binary";
            }
            else
            {
                return GetValue().ToString();
            }
        }

        public bool GetBoolean()
        {
            switch (FormatType)
            {
                case MsgPackType.TRUE: return true;
                case MsgPackType.FALSE: return false;
                default: throw new MessagePackValueException("is not boolean: " + Bytes);
            }
        }

        public string GetString()
        {
            switch (FormatType)
            {
                case MsgPackType.FIX_STR: return "";
                case MsgPackType.FIX_STR_0x01:
                case MsgPackType.FIX_STR_0x02:
                case MsgPackType.FIX_STR_0x03:
                case MsgPackType.FIX_STR_0x04:
                case MsgPackType.FIX_STR_0x05:
                case MsgPackType.FIX_STR_0x06:
                case MsgPackType.FIX_STR_0x07:
                case MsgPackType.FIX_STR_0x08:
                case MsgPackType.FIX_STR_0x09:
                case MsgPackType.FIX_STR_0x0A:
                case MsgPackType.FIX_STR_0x0B:
                case MsgPackType.FIX_STR_0x0C:
                case MsgPackType.FIX_STR_0x0D:
                case MsgPackType.FIX_STR_0x0E:
                case MsgPackType.FIX_STR_0x0F:
                case MsgPackType.FIX_STR_0x10:
                case MsgPackType.FIX_STR_0x11:
                case MsgPackType.FIX_STR_0x12:
                case MsgPackType.FIX_STR_0x13:
                case MsgPackType.FIX_STR_0x14:
                case MsgPackType.FIX_STR_0x15:
                case MsgPackType.FIX_STR_0x16:
                case MsgPackType.FIX_STR_0x17:
                case MsgPackType.FIX_STR_0x18:
                case MsgPackType.FIX_STR_0x19:
                case MsgPackType.FIX_STR_0x1A:
                case MsgPackType.FIX_STR_0x1B:
                case MsgPackType.FIX_STR_0x1C:
                case MsgPackType.FIX_STR_0x1D:
                case MsgPackType.FIX_STR_0x1E:
                case MsgPackType.FIX_STR_0x1F:
                case MsgPackType.STR8:
                case MsgPackType.STR16:
                case MsgPackType.STR32:
                    {
                        var body = GetBody();
                        var str = Encoding.UTF8.GetString(body.Array, body.Offset, body.Count);
                        return str;
                    }

                default: throw new MessagePackValueException("is not string type: "+Bytes);
            }
        }

        public ArraySegment<Byte> GetBytes()
        {
            return GetBody();
        }

        public void Dump(IFormatter f)
        {
            f.Dump(Bytes);
        }

        public ArraySegment<Byte> Dump()
        {
            return Bytes;
        }
    }
}
