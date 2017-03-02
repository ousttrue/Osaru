using System;
using System.Collections.Generic;
using System.Text;


namespace JsonSan.Serializers
{
    public interface IWriteStream<T>
    {
        void Write(IEnumerable<T> src);
        void Write(string src);
    }

    static class ArraySegementExtensions
    {
        //self[index] = value;
        public static void Set<T>(this ArraySegment<T> self, int index, T value)
        {
            if (index<0 || index >= self.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            self.Array[self.Offset + index] = value;
        }
    }

    public class CharArrayStream : IWriteStream<Char>
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

        public void Write(IEnumerable<char> src)
        {
            foreach (var c in src)
            {
                m_buffer.Set(m_pos++, c);
            }
        }
        public void Write(string src)
        {
            Write((IEnumerable<char>)src);
        }
    }

    public class StringBuilderStream: IWriteStream<Char>
    {
        StringBuilder m_sb;

        public StringBuilderStream(StringBuilder sb)
        {
            m_sb = sb;
        }

        public void Write(IEnumerable<char> src)
        {
            foreach(var c in src)
            {
                m_sb.Append(c);
            }
        }
        public void Write(string src)
        {
            m_sb.Append(src);
        }
    }
}
