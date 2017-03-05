using System;
using System.Linq;
using System.Collections.Generic;


namespace ObjectStructure
{
    public class CharArrayStream : IWriteStream
    {
        ArraySegment<Char> m_buffer;
        int m_pos;

        public CharArrayStream(Char[] buffer) : this(buffer, 0)
        {
        }
        public CharArrayStream(Char[] buffer, int offset) : this(buffer, 0, buffer.Length)
        {
        }
        public CharArrayStream(Char[] buffer, int offset, int count) : this(new ArraySegment<char>(buffer, offset, count))
        {
        }
        public CharArrayStream(ArraySegment<Char> buffer)
        {
            m_buffer = buffer;
        }

        public void Clear()
        {
            m_pos = 0;
        }
        public void Write(char c)
        {
            m_buffer.Set(m_pos++, c);
        }
        public void Write(IEnumerable<char> src)
        {
            foreach (var c in src)
            {
                Write(c);
            }
        }
        public void Write(string src)
        {
            Write((IEnumerable<char>)src);
        }

        public string Buffer()
        {
            return new String(m_buffer.Array.Skip(m_buffer.Offset).Take(m_buffer.Count).ToArray());
        }
    }
}
