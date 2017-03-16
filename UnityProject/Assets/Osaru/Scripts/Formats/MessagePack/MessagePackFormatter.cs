﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace Osaru.MessagePack
{
    public class MessagePackFormatter : IFormatter
    {
        IStore m_store;

        public MessagePackFormatter() 
            //: this(new MemoryStream())
        {
            m_store = new BytesStore();
        }

        /*
        public MessagePackFormatter (MemoryStream s)
        {
            m_store = new StreamStore(s);
        }
        */

#if false
        public bool MsgPack_Ext(IList list)
        {
            var t = list.GetType();
            var et = t.GetElementType();
            if (et.IsClass())
            {
                return false;
            }
            m_store.Write((Byte)MsgPackType.EXT32);
            var itemSize = Marshal.SizeOf(et);
            WriteInt32_NBO(list.Count * itemSize);

            Action<Object> pack;
            if (et == typeof(UInt16))
            {
                m_store.Write((Byte)ExtType.UINT16_BE);
                pack = o => WriteUInt16_NBO((UInt16)o);
            }
            else if (et == typeof(UInt32))
            {
                m_store.Write((Byte)ExtType.UINT32_BE);
                pack = o => WriteUInt32_NBO((UInt32)o);
            }
            else if (et == typeof(UInt64))
            {
                m_store.Write((Byte)ExtType.UINT64_BE);
                pack = o => WriteUInt64_NBO((UInt64)o);
            }
            else if (et == typeof(Int16))
            {
                m_store.Write((Byte)ExtType.INT16_BE);
                pack = o => WriteInt16_NBO((Int16)o);
            }
            else if (et == typeof(Int32))
            {
                m_store.Write((Byte)ExtType.INT32_BE);
                pack = o => WriteInt32_NBO((Int32)o);
            }
            else if (et == typeof(Int64))
            {
                m_store.Write((Byte)ExtType.INT64_BE);
                pack = o => WriteInt64_NBO((Int64)o);
            }
            else if (et == typeof(Single))
            {
                m_store.Write((Byte)ExtType.SINGLE_BE);
                pack = o => WriteSingle_NBO((Single)o);
            }
            else if (et == typeof(Double))
            {
                m_store.Write((Byte)ExtType.DOUBLE_BE);
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
#endif

        public void BeginList(int n)
        {
            if (n < 0x0F)
            {
                m_store.Write((Byte)((Byte)MsgPackType.FIX_ARRAY | n));
            }
            else if (n < 0xFFFF)
            {
                m_store.Write((Byte)MsgPackType.ARRAY16);
                m_store.Write(((UInt16)n).ToNetworkByteOrder());
            }
            else
            {
                m_store.Write((Byte)MsgPackType.ARRAY32);
                m_store.Write(n.ToNetworkByteOrder());
            }
        }

        public void EndList()
        {
        }

        public void BeginMap(int n)
        {
            if (n < 0x0F)
            {
                m_store.Write((Byte)((Byte)MsgPackType.FIX_MAP | n));
            }
            else if (n < 0xFFFF)
            {
                m_store.Write((Byte)MsgPackType.MAP16);
                m_store.Write(((UInt16)n).ToNetworkByteOrder());
            }
            else
            {
                m_store.Write((Byte)MsgPackType.MAP32);
                m_store.Write(n.ToNetworkByteOrder());
            }
        }

        public void EndMap()
        {
        }

        public void Null()
        {
            m_store.Write((Byte)MsgPackType.NIL);
        }

        public void Key(string key)
        {
            Value(key);
        }

        public void Value(string s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            int size = bytes.Length;
            if (size < 32)
            {
                m_store.Write((Byte)((Byte)MsgPackType.FIX_STR | size));
                m_store.Write(bytes);
            }
            else if (size < 0xFF)
            {
                m_store.Write((Byte)(MsgPackType.STR8));
                m_store.Write((Byte)(size));
                m_store.Write(bytes);
            }
            else if (size < 0xFFFF)
            {
                m_store.Write((Byte)MsgPackType.STR16);
                m_store.Write(((UInt16)size).ToNetworkByteOrder());
                m_store.Write(bytes);
            }
            else
            {
                m_store.Write((Byte)MsgPackType.STR32);
                m_store.Write(size.ToNetworkByteOrder());
                m_store.Write(bytes);
            }
        }

        public void Value(bool value)
        {
            if (value)
            {
                m_store.Write((Byte)MsgPackType.TRUE);
            }
            else
            {
                m_store.Write((Byte)MsgPackType.FALSE);
            }
        }

        public void Value(sbyte n)
        {
            if (n >= 0)
            {
                // positive
                m_store.Write((Byte)n);
            }
            else if (n >= -32)
            {
                var value = (MsgPackType)((n + 32) + (Byte)MsgPackType.NEGATIVE_FIXNUM);
                m_store.Write((Byte)value);
            }
            else
            {
                m_store.Write((Byte)MsgPackType.INT8);
                m_store.Write((Byte)n);
            }
        }

        public void Value(short n)
        {
            if (n >= 0)
            {
                // positive
                if (n <= 0xFF)
                {
                    m_store.Write((Byte)n);
                }
                else
                {
                    Value((UInt16)n);
                }
            }
            else
            {
                // negative
                if (n >= -128)
                {
                    m_store.Write((SByte)n);
                }
                else
                {
                    m_store.Write((Byte)MsgPackType.INT16);
                    m_store.Write(n.ToNetworkByteOrder());
                }
            }
        }

        public void Value(int n)
        {
            if (n >= 0)
            {
                // positive
                if (n <= 0xFF)
                {
                    m_store.Write((Byte)n);
                }
                else if (n <= 0xFFFF)
                {
                    Value((UInt16)n);
                }
                else
                {
                    Value((UInt32)n);
                }
            }
            else
            {
                // negative
                if (n >= -128)
                {
                    m_store.Write((SByte)n);
                }
                else if (n >= -32768)
                {
                    Value((Int16)n);
                }
                else
                {
                    m_store.Write((Byte)MsgPackType.INT32);
                    m_store.Write(n.ToNetworkByteOrder());
                }
            }
        }

        public void Value(long n)
        {
            if (n >= 0)
            {
                // positive
                if (n <= 0xFF)
                {
                    m_store.Write((Byte)n);
                }
                else if (n <= 0xFFFF)
                {
                    Value((UInt16)n);
                }
                else if (n <= 0xFFFFFFFF)
                {
                    Value((UInt32)n);
                }
                else
                {
                    Value((UInt64)n);
                }
            }
            else
            {
                // negative
                if (n >= -128)
                {
                    m_store.Write((SByte)n);
                }
                else if (n >= -32768)
                {
                    Value((Int16)n);
                }
                else if (n >= -2147483648)
                {
                    Value((Int32)n);
                }
                else
                {
                    m_store.Write((Byte)MsgPackType.INT64);
                    m_store.Write(n.ToNetworkByteOrder());
                }
            }
        }

        public void Value(byte n)
        {
            if (n <= 0x7F)
            {
                // FormatType.POSITIVE_FIXNUM
                m_store.Write(n);
            }
            else
            {
                m_store.Write((Byte)MsgPackType.UINT8);
                m_store.Write(n);
            }
        }

        public void Value(ushort n)
        {
            if (n <= 0xFF)
            {
                m_store.Write((Byte)n);
            }
            else
            {
                m_store.Write((Byte)MsgPackType.UINT16);
                m_store.Write(n.ToNetworkByteOrder());
            }
        }

        public void Value(uint n)
        {
            if (n <= 0xFF)
            {
                m_store.Write((Byte)n);
            }
            else if (n <= 0xFFFF)
            {
                Value((UInt16)n);
            }
            else
            {
                m_store.Write((Byte)MsgPackType.UINT32);
                m_store.Write(n.ToNetworkByteOrder());
            }
        }

        public void Value(ulong n)
        {
            if (n <= 0xFF)
            {
                m_store.Write((Byte)n);
            }
            else if (n <= 0xFFFF)
            {
                Value((UInt16)n);
            }
            else if (n <= 0xFFFFFFFF)
            {
                Value((UInt32)n);
            }
            else
            {
                m_store.Write((Byte)MsgPackType.UINT64);
                m_store.Write(n.ToNetworkByteOrder());
            }
        }

        public void Value(float value)
        {
            m_store.Write((Byte)MsgPackType.FLOAT);
            m_store.Write(value.ToNetworkByteOrder());
        }

        public void Value(double value)
        {
            m_store.Write((Byte)MsgPackType.DOUBLE);
            m_store.Write(value.ToNetworkByteOrder());
        }

        public void Bytes(ArraySegment<byte> bytes)
        {
            if (bytes.Count < 0xFF)
            {
                m_store.Write((Byte)(MsgPackType.BIN8));
                m_store.Write((Byte)(bytes.Count));
                m_store.Write(bytes);
            }
            else if (bytes.Count < 0xFFFF)
            {
                m_store.Write((Byte)MsgPackType.BIN16);
                m_store.Write(((UInt16)bytes.Count).ToNetworkByteOrder());
                m_store.Write(bytes);
            }
            else
            {
                m_store.Write((Byte)MsgPackType.BIN32);
                m_store.Write(bytes.Count.ToNetworkByteOrder());
                m_store.Write(bytes);
            }
        }

        public void Bytes(IEnumerable<byte> raw, int count)
        {
            Bytes(new ArraySegment<byte>(raw.Take(count).ToArray()));
        }

        public void Dump(ArraySegment<byte> formatted)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            m_store.Clear();
        }

        public IStore GetStore()
        {
            return m_store;
        }
    }
}