using System;
using System.IO;

namespace Osaru
{
    public class StreamStore: IStore
    {
        Stream m_s;
        BinaryWriter m_w;

        public StreamStore(Stream s)
        {
            m_s = s;
            m_w = new BinaryWriter(m_s);
        }

        public ArraySegment<byte> Bytes
        {
            get
            {
                var ms = m_s as MemoryStream;
                if (ms == null)
                {
                    throw new NotImplementedException();
                }
                return new ArraySegment<byte>(ms.GetBuffer(), 0, (int)ms.Position);
            }
        }

        public void Clear()
        {
            m_s.SetLength(0);
        }

        public void Write(long value)
        {
            m_w.Write(value);
        }

        public void Write(uint value)
        {
            m_w.Write(value);
        }

        public void Write(sbyte value)
        {
            m_w.Write(value);
        }

        public void Write(short value)
        {
            m_w.Write(value);
        }

        public void Write(ulong value)
        {
            m_w.Write(value);
        }

        public void Write(double value)
        {
            m_w.Write(value);
        }

        public void Write(float value)
        {
            m_w.Write(value);
        }

        public void Write(int value)
        {
            m_w.Write(value);
        }

        public void Write(char c)
        {
            m_w.Write(c);
        }

        public void Write(ushort value)
        {
            m_w.Write(value);
        }

        public void Write(byte value)
        {
            m_w.Write(value);
        }

        public void Write(string src)
        {
            m_w.Write(src);
        }

        public void Write(ArraySegment<byte> bytes)
        {
            m_w.Write(bytes.Array, bytes.Offset, bytes.Count);
        }
    }
}
