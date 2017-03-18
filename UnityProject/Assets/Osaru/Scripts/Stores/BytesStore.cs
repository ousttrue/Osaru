using System;
using System.Runtime.InteropServices;
using System.Text;


namespace Osaru
{
    public class BytesStore : IStore
    {
        Byte[] m_buffer;
        void Require(int size)
        {
            if (m_buffer == null)
            {
                m_buffer = new Byte[Math.Max(size, 1024)];
                return;
            }

            if (m_pos + size < m_buffer.Length)
            {
                return;
            }

            var newSize = Math.Max(m_pos + size, m_buffer.Length * 2);
            //Console.WriteLine(newSize);
            var old = m_buffer;
            m_buffer = new Byte[newSize];
            Array.Copy(old, m_buffer, m_pos);
        }
       
        int m_pos;

        public ArraySegment<byte> Bytes
        {
            get
            {
                return new ArraySegment<byte>(m_buffer, 0, m_pos);
            }
        }

        public void Clear()
        {
            m_pos = 0;
        }

        public void Write(sbyte value)
        {
            Require(Marshal.SizeOf(value));
            m_buffer[m_pos++] = (Byte)value;
        }

        char[] m_c = new char[1];
        public void Write(char c)
        {
            if(c<=0x7F)
            {
                // ascii
                Require(1);
                m_buffer[m_pos++] = (Byte)c;
                return;
            }

            Require(3);
            m_c[0] = c;
            var size=Encoding.UTF8.GetBytes(m_c, 0, 1, m_buffer, m_pos);
            m_pos += size;
        }

        public void Write(string src)
        {
            var size = Encoding.UTF8.GetByteCount(src);
            Require(size);
            var byteSize = Encoding.UTF8.GetBytes(src, 0, src.Length
                , m_buffer, m_pos);
            if (size != byteSize)
            {
                throw new Exception();
            }
            m_pos += byteSize;
        }

        public void Write(ArraySegment<byte> bytes)
        {
            Require(bytes.Count);
            Array.Copy(bytes.Array, bytes.Offset
                , m_buffer, m_pos, bytes.Count);
            m_pos += bytes.Count;
        }

        public void Write(short value)
        {
            Require(Marshal.SizeOf(value));
            var u = ByteUnion.WordValue.Create(value);
            m_buffer[m_pos++] = u.Byte0;
            m_buffer[m_pos++] = u.Byte1;
        }

        public void Write(int value)
        {
            Require(Marshal.SizeOf(value));
            var u = ByteUnion.DWordValue.Create(value);
            m_buffer[m_pos++] = u.Byte0;
            m_buffer[m_pos++] = u.Byte1;
            m_buffer[m_pos++] = u.Byte2;
            m_buffer[m_pos++] = u.Byte3;
        }

        public void Write(long value)
        {
            Require(Marshal.SizeOf(value));
            var u = ByteUnion.QWordValue.Create(value);
            m_buffer[m_pos++] = u.Byte0;
            m_buffer[m_pos++] = u.Byte1;
            m_buffer[m_pos++] = u.Byte2;
            m_buffer[m_pos++] = u.Byte3;
            m_buffer[m_pos++] = u.Byte4;
            m_buffer[m_pos++] = u.Byte5;
            m_buffer[m_pos++] = u.Byte6;
            m_buffer[m_pos++] = u.Byte7;
        }

        public void Write(byte value)
        {
            Require(Marshal.SizeOf(value));
            m_buffer[m_pos++] = value;
        }

        public void Write(ushort value)
        {
            Require(Marshal.SizeOf(value));
            var u = ByteUnion.WordValue.Create(value);
            m_buffer[m_pos++] = u.Byte0;
            m_buffer[m_pos++] = u.Byte1;
        }

        public void Write(uint value)
        {
            Require(Marshal.SizeOf(value));
            var u = ByteUnion.DWordValue.Create(value);
            m_buffer[m_pos++] = u.Byte0;
            m_buffer[m_pos++] = u.Byte1;
            m_buffer[m_pos++] = u.Byte2;
            m_buffer[m_pos++] = u.Byte3;
        }

        public void Write(ulong value)
        {
            Require(Marshal.SizeOf(value));
            var u = ByteUnion.QWordValue.Create(value);
            m_buffer[m_pos++] = u.Byte0;
            m_buffer[m_pos++] = u.Byte1;
            m_buffer[m_pos++] = u.Byte2;
            m_buffer[m_pos++] = u.Byte3;
            m_buffer[m_pos++] = u.Byte4;
            m_buffer[m_pos++] = u.Byte5;
            m_buffer[m_pos++] = u.Byte6;
            m_buffer[m_pos++] = u.Byte7;
        }

        public void Write(float value)
        {
            Require(Marshal.SizeOf(value));
            var u = ByteUnion.DWordValue.Create(value);
            m_buffer[m_pos++] = u.Byte0;
            m_buffer[m_pos++] = u.Byte1;
            m_buffer[m_pos++] = u.Byte2;
            m_buffer[m_pos++] = u.Byte3;
        }

        public void Write(double value)
        {
            Require(Marshal.SizeOf(value));
            var u = ByteUnion.QWordValue.Create(value);
            m_buffer[m_pos++] = u.Byte0;
            m_buffer[m_pos++] = u.Byte1;
            m_buffer[m_pos++] = u.Byte2;
            m_buffer[m_pos++] = u.Byte3;
            m_buffer[m_pos++] = u.Byte4;
            m_buffer[m_pos++] = u.Byte5;
            m_buffer[m_pos++] = u.Byte6;
            m_buffer[m_pos++] = u.Byte7;
        }
    }
}
