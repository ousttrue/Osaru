using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace NMessagePack
{
    public struct BytesSegment : IEnumerable<Byte>
    {
        #region ArraySegment
        Byte[] m_array;
        public Byte[] Array
        {
            get { return m_array; }
            private set { m_array = value; }
        }

        Int32 m_offset;
        public Int32 Offset
        {
            get { return m_offset; }
            private set { m_offset = value; }
        }

        Int32 m_count;
        public Int32 Count
        {
            get { return m_count; }
            private set { m_count = value; }
        }
        #endregion

        #region Indexer
        public Byte this[int i]
        {
            get
            {
                if (i < 0) throw new IndexOutOfRangeException();
                if (i >= Count) throw new IndexOutOfRangeException();
                return Array[Offset + i];
            }
            set
            {
                if (i < 0) throw new IndexOutOfRangeException();
                if (i >= Count) throw new IndexOutOfRangeException();
                Array[Offset + i] = value;
            }
        }
        #endregion

        public BytesSegment(Byte[] bytes, Int32 offset, Int32 count)
        {
            if (offset < 0)
            {
                throw new ArgumentException("negative offset");
            }
            if (count < 0)
            {
                throw new ArgumentException("negative count");
            }
            m_array = bytes;
            m_offset = offset;
            m_count = count;
        }

        public BytesSegment(Byte[] bytes) : this(bytes, 0, bytes.Length) { }

        #region IEnumerator
        public IEnumerator<byte> GetEnumerator()
        {
            return Array.Skip(Offset).Take(Count).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Array.Skip(Offset).Take(Count).GetEnumerator();
        }
        #endregion

        public void Assign(Func<Byte, Byte> pred)
        {
            for (int i = 0; i < Count; ++i)
            {
                this[i] = pred(this[i]);
            }
        }

        public BytesSegment Advance(Int32 n)
        {
            return new BytesSegment(Array, Offset + n, Count - n);
        }

        public BytesSegment Take(Int32 n)
        {
            return new BytesSegment(Array, Offset, n);
        }

        public Byte[] TakeReversedArray(Int32 n)
        {
            var bytes = new Byte[n];
            var x = n - 1;
            for (int i = 0; i < n; ++i, --x)
            {
                bytes[i] = this[x];
            }
            return bytes;
        }

        #region Converter
        /// <summary>
        /// Read Uint16 From NetworkBytesOrder to HostBytesOrder
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public UInt16 N2H_UInt16()
        {
            if (BitConverter.IsLittleEndian)
            {
                return (UInt16)(this[0] << 8 | this[1]);
            }
            else
            {
                return BitConverter.ToUInt16(this.Array, this.Offset);
            }
        }

        public UInt32 N2H_UInt32()
        {
            if (BitConverter.IsLittleEndian)
            {
                return (UInt32)(this[0] << 24 | this[1] << 16 | this[2] << 8 | this[3]);
            }
            else
            {
                return BitConverter.ToUInt32(this.Array, this.Offset);
            }
        }

        public UInt64 N2H_UInt64()
        {
            var uvalue = BitConverter.ToUInt64(this.Array, this.Offset);
            if (BitConverter.IsLittleEndian)
            {
                ulong swapped =
                     ((0x00000000000000FF) & (uvalue >> 56)
                     | (0x000000000000FF00) & (uvalue >> 40)
                     | (0x0000000000FF0000) & (uvalue >> 24)
                     | (0x00000000FF000000) & (uvalue >> 8)
                     | (0x000000FF00000000) & (uvalue << 8)
                     | (0x0000FF0000000000) & (uvalue << 24)
                     | (0x00FF000000000000) & (uvalue << 40)
                     | (0xFF00000000000000) & (uvalue << 56));
                return swapped;
            }
            else
            {
                return uvalue;
            }
        }

        public Int16 N2H_Int16()
        {
            if (BitConverter.IsLittleEndian)
            {
                return (Int16)(this[0] << 8 | this[1]);
            }
            else
            {
                return BitConverter.ToInt16(this.Array, this.Offset);
            }
        }

        public Int32 N2H_Int32()
        {
            if (BitConverter.IsLittleEndian)
            {
                return (Int32)(this[0] << 24 | this[1] << 16 | this[2] << 8 | this[3]);
            }
            else
            {
                return BitConverter.ToInt32(this.Array, this.Offset);
            }
        }

        public Int64 N2H_Int64()
        {
            var value = BitConverter.ToUInt64(this.Array, this.Offset);
            if (BitConverter.IsLittleEndian)
            {
                ulong swapped =
                     ((0x00000000000000FF) & (value >> 56)
                     | (0x000000000000FF00) & (value >> 40)
                     | (0x0000000000FF0000) & (value >> 24)
                     | (0x00000000FF000000) & (value >> 8)
                     | (0x000000FF00000000) & (value << 8)
                     | (0x0000FF0000000000) & (value << 24)
                     | (0x00FF000000000000) & (value << 40)
                     | (0xFF00000000000000) & (value << 56));
                return (long)swapped;
            }
            else
            {
                return (long)value;
            }
        }

        public Single N2H_Single()
        {
            if (BitConverter.IsLittleEndian)
            {
                return BitConverter.ToSingle(TakeReversedArray(4), 0);
            }
            else
            {
                return BitConverter.ToSingle(this.Array, this.Offset);
            }
        }

        public Double N2H_Double()
        {
            return BitConverter.Int64BitsToDouble(N2H_Int64());
        }

        public void N2H_CopyTo(Byte[] buffer, int elementSize)
        {
            if (buffer.Length < Count) throw new ArgumentException();

            for (int i = 0; i < Count; i += elementSize)
            {
                for (int j = 0; j < elementSize; ++j)
                {
                    buffer[i + j] = this[i + elementSize - 1 - j];
                }
            }
        }

        public void N2H_CopyTo(Array result, Byte[] buffer)
        {
            if (BitConverter.IsLittleEndian)
            {
                if (buffer.Length < Count)
                {
                    throw new ArgumentException();
                }
                N2H_CopyTo(buffer, Marshal.SizeOf(result.GetType().GetElementType()));

                Buffer.BlockCopy(buffer, 0, result, 0, Count);
            }
            else
            {
                Buffer.BlockCopy(Array, Offset, result, 0, Count);
            }
        }
    }
    #endregion
}
