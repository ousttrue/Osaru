using System;
using System.Collections.Generic;
using System.IO;

namespace NMessagePack
{
    public class EndianByteWriter
    {
        Byte[] m_converterBuffer = new Byte[8];
        Stream m_s;

        public EndianByteWriter(Stream s)
        {
            if (s == null) new ArgumentNullException();
            m_s = s;
        }

        public void WriteByte(Byte n)
        {
            m_s.WriteByte(n);
        }

        void WriteSwapping(UInt16 n)
        {
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(0));
        }
        void WriteSwapping(UInt32 n)
        {
            m_s.WriteByte(n.GetHexDigit(3));
            m_s.WriteByte(n.GetHexDigit(2));
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(0));
        }
        void WriteSwapping(UInt64 n)
        {
            m_s.WriteByte(n.GetHexDigit(7));
            m_s.WriteByte(n.GetHexDigit(6));
            m_s.WriteByte(n.GetHexDigit(5));
            m_s.WriteByte(n.GetHexDigit(4));
            m_s.WriteByte(n.GetHexDigit(3));
            m_s.WriteByte(n.GetHexDigit(2));
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(0));
        }
        void WriteSwapping(Int16 n)
        {
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(0));
        }
        void WriteSwapping(Int32 n)
        {
            m_s.WriteByte(n.GetHexDigit(3));
            m_s.WriteByte(n.GetHexDigit(2));
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(0));
        }
        void WriteSwapping(Int64 n)
        {
            m_s.WriteByte(n.GetHexDigit(7));
            m_s.WriteByte(n.GetHexDigit(6));
            m_s.WriteByte(n.GetHexDigit(5));
            m_s.WriteByte(n.GetHexDigit(4));
            m_s.WriteByte(n.GetHexDigit(3));
            m_s.WriteByte(n.GetHexDigit(2));
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(0));
        }
        // ToDo
        void WriteSwapping(Single _n)
        {
            var n = _n.ToUint32(m_converterBuffer);
            m_s.WriteByte(n.GetHexDigit(3));
            m_s.WriteByte(n.GetHexDigit(2));
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(0));
        }
        // ToDo
        void WriteSwapping(Double _n)
        {
            var n = _n.ToUint64(m_converterBuffer);
            m_s.WriteByte(n.GetHexDigit(7));
            m_s.WriteByte(n.GetHexDigit(6));
            m_s.WriteByte(n.GetHexDigit(5));
            m_s.WriteByte(n.GetHexDigit(4));
            m_s.WriteByte(n.GetHexDigit(3));
            m_s.WriteByte(n.GetHexDigit(2));
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(0));
        }

        void WriteSequencial(UInt16 n)
        {
            m_s.WriteByte(n.GetHexDigit(0));
            m_s.WriteByte(n.GetHexDigit(1));
        }
        void WriteSequencial(UInt32 n)
        {
            m_s.WriteByte(n.GetHexDigit(0));
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(2));
            m_s.WriteByte(n.GetHexDigit(3));
        }
        void WriteSequencial(UInt64 n)
        {
            m_s.WriteByte(n.GetHexDigit(0));
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(2));
            m_s.WriteByte(n.GetHexDigit(3));
            m_s.WriteByte(n.GetHexDigit(4));
            m_s.WriteByte(n.GetHexDigit(5));
            m_s.WriteByte(n.GetHexDigit(6));
            m_s.WriteByte(n.GetHexDigit(7));
        }
        void WriteSequencial(Int16 n)
        {
            m_s.WriteByte(n.GetHexDigit(0));
            m_s.WriteByte(n.GetHexDigit(1));
        }
        void WriteSequencial(Int32 n)
        {
            m_s.WriteByte(n.GetHexDigit(0));
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(2));
            m_s.WriteByte(n.GetHexDigit(3));
        }
        void WriteSequencial(Int64 n)
        {
            m_s.WriteByte(n.GetHexDigit(0));
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(2));
            m_s.WriteByte(n.GetHexDigit(3));
            m_s.WriteByte(n.GetHexDigit(4));
            m_s.WriteByte(n.GetHexDigit(5));
            m_s.WriteByte(n.GetHexDigit(6));
            m_s.WriteByte(n.GetHexDigit(7));
        }
        // ToDo
        void WriteSequencial(Single _n)
        {
            var n = _n.ToUint32(m_converterBuffer);
            m_s.WriteByte(n.GetHexDigit(0));
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(2));
            m_s.WriteByte(n.GetHexDigit(3));
        }
        // ToDo
        void WriteSequencial(Double _n)
        {
            var n = _n.ToUint64(m_converterBuffer);
            m_s.WriteByte(n.GetHexDigit(0));
            m_s.WriteByte(n.GetHexDigit(1));
            m_s.WriteByte(n.GetHexDigit(2));
            m_s.WriteByte(n.GetHexDigit(3));
            m_s.WriteByte(n.GetHexDigit(4));
            m_s.WriteByte(n.GetHexDigit(5));
            m_s.WriteByte(n.GetHexDigit(6));
            m_s.WriteByte(n.GetHexDigit(7));
        }

        #region NetworkByteOrder
        public void WriteUInt16_NBO(UInt16 n)
        {
            if (BitConverter.IsLittleEndian)
            {
                // little to big
                WriteSwapping(n);
            }
            else
            {
                WriteSequencial(n);
            }
        }

        public void WriteUInt32_NBO(UInt32 n)
        {
            if (BitConverter.IsLittleEndian)
            {
                // little to big
                WriteSwapping(n);
            }
            else
            {
                WriteSequencial(n);
            }
        }

        public void WriteUInt64_NBO(UInt64 n)
        {
            if (BitConverter.IsLittleEndian)
            {
                // little to big
                WriteSwapping(n);
            }
            else
            {
                WriteSequencial(n);
            }
        }

        public void WriteInt16_NBO(Int16 n)
        {
            if (BitConverter.IsLittleEndian)
            {
                // little to big
                WriteSwapping(n);
            }
            else
            {
                WriteSequencial(n);
            }
        }

        public void WriteInt32_NBO(Int32 n)
        {
            if (BitConverter.IsLittleEndian)
            {
                // little to big
                WriteSwapping(n);
            }
            else
            {
                WriteSequencial(n);
            }
        }

        public void WriteInt64_NBO(Int64 n)
        {
            if (BitConverter.IsLittleEndian)
            {
                // little to big
                WriteSwapping(n);
            }
            else
            {
                WriteSequencial(n);
            }
        }

        public void WriteSingle_NBO(Single n)
        {
            if (BitConverter.IsLittleEndian)
            {
                // little to big
                WriteSwapping(n);
            }
            else
            {
                WriteSequencial(n);
            }
        }

        public void WriteDouble_NBO(Double n)
        {
            if (BitConverter.IsLittleEndian)
            {
                // little to big
                WriteSwapping(n);
            }
            else
            {
                WriteSequencial(n);
            }
        }

        public void WriteBytes(IEnumerable<Byte> bytes)
        {
            foreach(var b in bytes)
            {
                WriteByte(b);
            }
        }

        #endregion
    }
}
