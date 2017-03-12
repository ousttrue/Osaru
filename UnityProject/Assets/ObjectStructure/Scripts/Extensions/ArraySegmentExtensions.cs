using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace System
{
    public static class ArraySegementExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this ArraySegment<T> self)
        {
            return self.Array.Skip(self.Offset).Take(self.Count);
        }

        public static void Set<T>(this ArraySegment<T> self, int index, T value)
        {
            if (index < 0 || index >= self.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            self.Array[self.Offset + index] = value;
        }

        public static T Get<T>(this ArraySegment<T> self, int index)
        {
            if (index < 0 || index >= self.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return self.Array[self.Offset + index];
        }

        public static ArraySegment<T> Advance<T>(this ArraySegment<T> self, Int32 n)
        {
            return new ArraySegment<T>(self.Array, self.Offset + n, self.Count - n);
        }

        public static ArraySegment<T> Take<T>(this ArraySegment<T> self, Int32 n)
        {
            return new ArraySegment<T>(self.Array, self.Offset, n);
        }

        public static T[] TakeReversedArray<T>(this ArraySegment<T> self, Int32 n)
        {
            var array = new T[n];
            var x = n - 1;
            for (int i = 0; i < n; ++i, --x)
            {
                array[i] = self.Get(x);
            }
            return array;
        }
        #region Converter
        /// <summary>
        /// Read Uint16 From NetworkBytesOrder to HostBytesOrder
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static UInt16 N2H_UInt16(this ArraySegment<Byte> self)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (UInt16)(self.Get(0) << 8 | self.Get(1));
            }
            else
            {
                return BitConverter.ToUInt16(self.Array, self.Offset);
            }
        }

        public static UInt32 N2H_UInt32(this ArraySegment<Byte> self)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (UInt32)(self.Get(0) << 24 | self.Get(1) << 16 | self.Get(2) << 8 | self.Get(3));
            }
            else
            {
                return BitConverter.ToUInt32(self.Array, self.Offset);
            }
        }

        public static UInt64 N2H_UInt64(this ArraySegment<Byte> self)
        {
            var uvalue = BitConverter.ToUInt64(self.Array, self.Offset);
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

        public static Int16 N2H_Int16(this ArraySegment<Byte> self)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (Int16)(self.Get(0) << 8 | self.Get(1));
            }
            else
            {
                return BitConverter.ToInt16(self.Array, self.Offset);
            }
        }

        public static Int32 N2H_Int32(this ArraySegment<Byte> self)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (Int32)(self.Get(0) << 24 | self.Get(1) << 16 | self.Get(2) << 8 | self.Get(3));
            }
            else
            {
                return BitConverter.ToInt32(self.Array, self.Offset);
            }
        }

        public static Int64 N2H_Int64(this ArraySegment<Byte> self)
        {
            var value = BitConverter.ToUInt64(self.Array, self.Offset);
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

        public static Single N2H_Single(this ArraySegment<Byte> self)
        {
            if (BitConverter.IsLittleEndian)
            {
                return BitConverter.ToSingle(self.TakeReversedArray(4), 0);
            }
            else
            {
                return BitConverter.ToSingle(self.Array, self.Offset);
            }
        }

        public static Double N2H_Double(this ArraySegment<Byte> self)
        {
            return BitConverter.Int64BitsToDouble(self.N2H_Int64());
        }

        public static void N2H_CopyTo(this ArraySegment<Byte> self, Byte[] buffer, int elementSize)
        {
            if (buffer.Length < self.Count) throw new ArgumentException();

            for (int i = 0; i < self.Count; i += elementSize)
            {
                for (int j = 0; j < elementSize; ++j)
                {
                    buffer[i + j] = self.Get(i + elementSize - 1 - j);
                }
            }
        }

        public static void N2H_CopyTo(this ArraySegment<Byte> self, Array result, Byte[] buffer)
        {
            if (BitConverter.IsLittleEndian)
            {
                if (buffer.Length < self.Count)
                {
                    throw new ArgumentException();
                }
                self.N2H_CopyTo(buffer, Marshal.SizeOf(result.GetType().GetElementType()));

                Buffer.BlockCopy(buffer, 0, result, 0, self.Count);
            }
            else
            {
                Buffer.BlockCopy(self.Array, self.Offset, result, 0, self.Count);
            }
        }
        #endregion
    }
}
