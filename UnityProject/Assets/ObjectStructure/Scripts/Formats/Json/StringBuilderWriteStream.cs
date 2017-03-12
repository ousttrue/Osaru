using System;
using System.Collections.Generic;
using System.Text;


namespace ObjectStructure
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

        public void Write(ArraySegment<byte> bytes)
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
