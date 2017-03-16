using System;
using System.Runtime.InteropServices;


namespace Osaru
{
    public static class ByteUnion
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct WordValue
        {
            [FieldOffset(0)]
            public Int16 Signed;
            [FieldOffset(0)]
            public UInt16 Unsigned;

            [FieldOffset(0)]
            public byte Byte0;
            [FieldOffset(1)]
            public byte Byte1;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DWordValue
        {
            [FieldOffset(0)]
            public Int32 Signed;
            [FieldOffset(0)]
            public UInt32 Unsigned;
            [FieldOffset(0)]
            public Single Float;

            [FieldOffset(0)]
            public byte Byte0;
            [FieldOffset(1)]
            public byte Byte1;
            [FieldOffset(2)]
            public byte Byte2;
            [FieldOffset(3)]
            public byte Byte3;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct QWordValue
        {
            [FieldOffset(0)]
            public Int64 Signed;
            [FieldOffset(0)]
            public UInt64 Unsigned;
            [FieldOffset(0)]
            public Double Float;

            [FieldOffset(0)]
            public byte Byte0;
            [FieldOffset(1)]
            public byte Byte1;
            [FieldOffset(2)]
            public byte Byte2;
            [FieldOffset(3)]
            public byte Byte3;
            [FieldOffset(4)]
            public byte Byte4;
            [FieldOffset(5)]
            public byte Byte5;
            [FieldOffset(6)]
            public byte Byte6;
            [FieldOffset(7)]
            public byte Byte7;
        }
    }
}
