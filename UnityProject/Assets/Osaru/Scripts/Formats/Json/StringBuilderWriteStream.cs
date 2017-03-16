using System;
using System.Collections.Generic;
using System.Text;


namespace Osaru
{
    public class StringBuilderStream: IStore
    {
        StringBuilder m_sb;

        public StringBuilderStream(StringBuilder sb)
        {
            m_sb = sb;
        }

        public ArraySegment<Byte> Bytes
        {
            get
            {
                return new ArraySegment<Byte>(
                    Encoding.UTF8.GetBytes(Buffer())
                    );
            }
        }

        public string Buffer()
        {
            return m_sb.ToString();
        }

        public void Clear()
        {
            m_sb.Length = 0;
        }

        public void Write(long value)
        {
            throw new NotImplementedException();
        }

        public void Write(double value)
        {
            throw new NotImplementedException();
        }

        public void Write(ulong value)
        {
            throw new NotImplementedException();
        }

        public void Write(short value)
        {
            throw new NotImplementedException();
        }

        public void Write(sbyte value)
        {
            throw new NotImplementedException();
        }

        public void Write(uint value)
        {
            throw new NotImplementedException();
        }

        public void Write(float value)
        {
            throw new NotImplementedException();
        }

        public void Write(int value)
        {
            throw new NotImplementedException();
        }

        public void Write(ArraySegment<byte> bytes)
        {
            throw new NotImplementedException();
        }

        public void Write(byte value)
        {
            throw new NotImplementedException();
        }

        public void Write(ushort value)
        {
            throw new NotImplementedException();
        }

        public void Write(IEnumerable<char> src)
        {
            foreach(var c in src)
            {
                m_sb.Append(c);
            }
        }
        public void Write(Char c)
        {
            m_sb.Append(c);
        }
        public void Write(string src)
        {
            m_sb.Append(src);
        }
    }
}
