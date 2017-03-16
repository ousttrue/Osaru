using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Osaru.MessagePack
{
#if false
    public class MessagePackFormatter : IFormatter
    {
        public class ByteStore: IStore
        {
            MemoryStream m_s=new MemoryStream();

            public ArraySegment<Byte> Bytes
            {
                get
                {
                    return new ArraySegment<Byte>(Buffer());
                }
            }

            public Stream Stream
            {
                get { return m_s; }
            }
            public byte[] Buffer()
            {
                return m_s.ToArray();
            }
            public void Clear()
            {
                m_s.SetLength(0);
            }

            public void Write(ArraySegment<Byte> bytes)
            {
                m_s.Write(bytes.Array, bytes.Offset, bytes.Count);
            }

            public void Write(char c)
            {
                throw new NotImplementedException();
            }

            public void Write(string src)
            {
                throw new NotImplementedException();
            }
        }
        ByteStore m_s;
        MsgPackWriter m_w;

        public MessagePackFormatter()
        {
            m_s = new ByteStore();
            m_w = new MsgPackWriter(m_s.Stream);
        }

        public IStore GetStore()
        {
            return m_s;
        }

        public void Clear()
        {
            m_s.Clear();
        }

        public void Null()
        {
            m_w.MsgPackNil();
        }

        public void BeginList(int n)
        {
            m_w.MsgPackArray(n);
        }

        public void EndList()
        {
            //throw new NotImplementedException();
        }

        public void BeginMap(int n)
        {
            m_w.MsgPackMap(n);
        }

        public void EndMap()
        {
            //throw new NotImplementedException();
        }

        public void Key(string key)
        {
            m_w.MsgPack(key);
        }

        public void Value(ushort value)
        {
            m_w.MsgPack(value);
        }

        public void Value(ulong value)
        {
            m_w.MsgPack(value);
        }

        public void Value(double value)
        {
            m_w.MsgPack(value);
        }

        public void Value(float value)
        {
            m_w.MsgPack(value);
        }

        public void Value(uint value)
        {
            m_w.MsgPack(value);
        }

        public void Value(byte value)
        {
            m_w.MsgPack(value);
        }

        public void Value(short value)
        {
            m_w.MsgPack(value);
        }

        public void Value(int value)
        {
            m_w.MsgPack(value);
        }

        public void Value(long value)
        {
            m_w.MsgPack(value);
        }

        public void Value(sbyte value)
        {
            m_w.MsgPack(value);
        }

        public void Value(bool value)
        {
            m_w.MsgPack(value);
        }

        public void Value(string value)
        {
            m_w.MsgPack(value);
        }

        public void Bytes(ArraySegment<Byte> bytes)
        {
            m_w.MsgPack(bytes.Array.Skip(bytes.Offset), bytes.Count);
        }
        public void Bytes(IEnumerable<byte> raw, int count)
        {
            m_w.MsgPack(raw, count);
        }

        public void Dump(ArraySegment<Byte> bytes)
        {
            m_w.WriteBytes(bytes.ToEnumerable());
        }
    }
#endif
}
