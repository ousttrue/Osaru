using System;
using System.IO;

namespace Osaru
{
    public class MemoryStreamStore: IStore
    {
        MemoryStream m_s;
        BinaryWriter m_w;

        public MemoryStreamStore(MemoryStream s)
        {
            m_s = s;
            m_w = new BinaryWriter(m_s);
        }

        public ArraySegment<byte> Bytes
        {
            get
            {
                return new ArraySegment<byte>(m_s.ToArray(), 0, (int)m_s.Position);
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
