using System;
using System.Collections.Generic;
using System.IO;

namespace ObjectStructure.MessagePack
{
    public class MessagePackFormatter : IFormatter<Byte[]>
    {
        public class ByteStore: IStore<Byte[]>
        {
            MemoryStream m_s=new MemoryStream();
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
        }
        ByteStore m_s;
        MsgPackWriter m_w;

        public MessagePackFormatter()
        {
            m_s = new ByteStore();
            m_w = new MsgPackWriter(m_s.Stream);
        }

        public IStore<Byte[]> GetStore()
        {
            return m_s;
        }

        public void Clear()
        {
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

        public void Bytes(IEnumerable<byte> raw, int count)
        {
            m_w.MsgPack(raw, count);
        }

        public void Dump(object o)
        {
            m_w.WriteBytes((Byte[])o);
        }
    }
}
