using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;


namespace NMessagePack
{
    public class MsgPackWriter: EndianByteWriter
    {
        public MsgPackWriter(Stream s) : base(s)
        { }

        public void MsgPackNil()
        {
            WriteByte((Byte)MsgPackType.NIL);
        }

        public void MsgPack(Boolean b)
        {
            if (b)
            {
                WriteByte((Byte)MsgPackType.TRUE);
            }
            else
            {
                WriteByte((Byte)MsgPackType.FALSE);
            }
        }

        #region unsigned integer
        public void MsgPack(Byte n)
        {
            if (n <= 0x7F)
            {
                // FormatType.POSITIVE_FIXNUM
                WriteByte(n);
            }
            else
            {
                WriteByte((Byte)MsgPackType.UINT8);
                WriteByte(n);
            }
        }

        public void MsgPack(UInt16 n)
        {
            if (n <= 0xFF)
            {
                MsgPack((Byte)n);
            }
            else
            {
                WriteByte((Byte)MsgPackType.UINT16);
                WriteUInt16_NBO(n);
            }
        }

        public void MsgPack(UInt32 n)
        {
            if (n <= 0xFF)
            {
                MsgPack((Byte)n);
            }
            else if (n <= 0xFFFF)
            {
                MsgPack((UInt16)n);
            }
            else
            {
                WriteByte((Byte)MsgPackType.UINT32);
                WriteUInt32_NBO(n);
            }
        }

        public void MsgPack(UInt64 n)
        {
            if (n <= 0xFF)
            {
                MsgPack((Byte)n);
            }
            else if (n <= 0xFFFF)
            {
                MsgPack((UInt16)n);
            }
            else if (n <= 0xFFFFFFFF)
            {
                MsgPack((UInt32)n);
            }
            else
            {
                WriteByte((Byte)MsgPackType.UINT64);
                WriteUInt64_NBO(n);
            }
        }
        #endregion

        #region signed integer
        public void MsgPack(SByte n)
        {
            if (n >= 0)
            {
                // positive
                MsgPack((Byte)n);
            }
            else if (n >= -32)
            {
                var value = (MsgPackType)((n + 32) + (Byte)MsgPackType.NEGATIVE_FIXNUM);
                WriteByte((Byte)value);
            }
            else
            {
                WriteByte((Byte)MsgPackType.INT8);
                WriteByte((Byte)n);
            }
        }

        public void MsgPack(Int16 n)
        {
            if (n >= 0)
            {
                // positive
                if (n <= 0xFF)
                {
                    MsgPack((Byte)n);
                }
                else
                {
                    MsgPack((UInt16)n);
                }
            }
            else
            {
                // negative
                if (n >= -128)
                {
                    MsgPack((SByte)n);
                }
                else
                {
                    WriteByte((Byte)MsgPackType.INT16);
                    WriteInt16_NBO(n);
                }
            }
        }

        public void MsgPack(Int32 n)
        {
            if (n >= 0)
            {
                // positive
                if (n <= 0xFF)
                {
                    MsgPack((Byte)n);
                }
                else if (n <= 0xFFFF)
                {
                    MsgPack((UInt16)n);
                }
                else
                {
                    MsgPack((UInt32)n);
                }
            }
            else
            {
                // negative
                if (n >= -128)
                {
                    MsgPack((SByte)n);
                }
                else if (n >= -32768)
                {
                    MsgPack((Int16)n);
                }
                else
                {
                    WriteByte((Byte)MsgPackType.INT32);
                    WriteInt32_NBO(n);
                }
            }
        }

        public void MsgPack(Int64 n)
        {
            if (n >= 0)
            {
                // positive
                if (n <= 0xFF)
                {
                    MsgPack((Byte)n);
                }
                else if (n <= 0xFFFF)
                {
                    MsgPack((UInt16)n);
                }
                else if (n <= 0xFFFFFFFF)
                {
                    MsgPack((UInt32)n);
                }
                else
                {
                    MsgPack((UInt64)n);
                }
            }
            else
            {
                // negative
                if (n >= -128)
                {
                    MsgPack((SByte)n);
                }
                else if (n >= -32768)
                {
                    MsgPack((Int16)n);
                }
                else if (n >= -2147483648)
                {
                    MsgPack((Int32)n);
                }
                else
                {
                    WriteByte((Byte)MsgPackType.INT64);
                    WriteInt64_NBO(n);
                }
            }
        }
        #endregion

        #region float
        public void MsgPack(Single n)
        {
            WriteByte((Byte)MsgPackType.FLOAT);
            WriteSingle_NBO(n);
        }

        public void MsgPack(Double n)
        {
            WriteByte((Byte)MsgPackType.DOUBLE);
            WriteDouble_NBO(n);
        }
        #endregion

        public void MsgPack(String s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            int size = bytes.Length;
            if (size < 32)
            {
                WriteByte((Byte)((Byte)MsgPackType.FIX_STR | size));
                WriteBytes(bytes);
            }
            else if (size < 0xFF)
            {
                WriteByte((Byte)(MsgPackType.STR8));
                WriteByte((Byte)(size));
                WriteBytes(bytes);
            }
            else if (size < 0xFFFF)
            {
                WriteByte((Byte)MsgPackType.STR16);
                WriteUInt16_NBO((UInt16)size);
                WriteBytes(bytes);
            }
            else
            {
                WriteByte((Byte)MsgPackType.STR32);
                WriteInt32_NBO(size);
                WriteBytes(bytes);
            }
        }

        public void MsgPack(IList<Byte> bytes)
        {
            MsgPack(bytes, bytes.Count);
        }

        public void MsgPack(IEnumerable<byte> bytes, int size)
        {
            if (size < 0xFF)
            {
                WriteByte((Byte)(MsgPackType.BIN8));
                WriteByte((Byte)(size));
                WriteBytes(bytes);
            }
            else if (size < 0xFFFF)
            {
                WriteByte((Byte)MsgPackType.BIN16);
                WriteUInt16_NBO((UInt16)size);
                WriteBytes(bytes);
            }
            else
            {
                WriteByte((Byte)MsgPackType.BIN32);
                WriteInt32_NBO(size);
                WriteBytes(bytes);
            }
        }

        public void MsgPackArray(Int32 n)
        {
            if (n < 0x0F)
            {
                WriteByte((Byte)((Byte)MsgPackType.FIX_ARRAY | n));
            }
            else if (n < 0xFFFF)
            {
                WriteByte((Byte)MsgPackType.ARRAY16);
                WriteUInt16_NBO((UInt16)n);
            }
            else
            {
                WriteByte((Byte)MsgPackType.ARRAY32);
                WriteInt32_NBO(n);
            }
        }

        public void MsgPackMap(Int32 n)
        {
            if (n < 0x0F)
            {
                WriteByte((Byte)((Byte)MsgPackType.FIX_MAP | n));
            }
            else if (n < 0xFFFF)
            {
                WriteByte((Byte)MsgPackType.MAP16);
                WriteUInt16_NBO((UInt16)n);
            }
            else
            {
                WriteByte((Byte)MsgPackType.MAP32);
                WriteInt32_NBO(n);
            }
        }

        public bool MsgPack_Ext(IList list)
        {
            var t = list.GetType();
            var et = t.GetElementType();
            if (et.IsClass)
            {
                return false;
            }
            WriteByte((Byte)MsgPackType.EXT32);
            var itemSize = Marshal.SizeOf(et);
            WriteInt32_NBO(list.Count * itemSize);

            Action<Object> pack;
            if (et == typeof(UInt16))
            {
                WriteByte((Byte)ExtType.UINT16_BE);
                pack = o => WriteUInt16_NBO((UInt16)o);
            }
            else if (et == typeof(UInt32))
            {
                WriteByte((Byte)ExtType.UINT32_BE);
                pack = o => WriteUInt32_NBO((UInt32)o);
            }
            else if (et == typeof(UInt64))
            {
                WriteByte((Byte)ExtType.UINT64_BE);
                pack = o => WriteUInt64_NBO((UInt64)o);
            }
            else if (et == typeof(Int16))
            {
                WriteByte((Byte)ExtType.INT16_BE);
                pack = o => WriteInt16_NBO((Int16)o);
            }
            else if (et == typeof(Int32))
            {
                WriteByte((Byte)ExtType.INT32_BE);
                pack = o => WriteInt32_NBO((Int32)o);
            }
            else if (et == typeof(Int64))
            {
                WriteByte((Byte)ExtType.INT64_BE);
                pack = o => WriteInt64_NBO((Int64)o);
            }
            else if (et == typeof(Single))
            {
                WriteByte((Byte)ExtType.SINGLE_BE);
                pack = o => WriteSingle_NBO((Single)o);
            }
            else if (et == typeof(Double))
            {
                WriteByte((Byte)ExtType.DOUBLE_BE);
                pack = o => WriteDouble_NBO((Double)o);
            }
            else
            {
                return false;
            }

            foreach (var i in list)
            {
                pack(i);
            }
            return true;
        }
    }
}
