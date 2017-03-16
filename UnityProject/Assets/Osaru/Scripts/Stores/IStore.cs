using System;


namespace Osaru
{
    public interface IStore
    {
        void Clear();
        ArraySegment<Byte> Bytes { get; }

        void Write(Byte value);
        void Write(UInt16 value);
        void Write(UInt32 value);
        void Write(UInt64 value);

        void Write(SByte value);
        void Write(Int16 value);
        void Write(Int32 value);
        void Write(Int64 value);

        void Write(Single value);
        void Write(Double value);

        void Write(ArraySegment<Byte> bytes);

        void Write(string src);
        void Write(char c);
    }

    public static class IStoreExtensions
    {
        public static void Write(this IStore s, Byte[] bytes)
        {
            s.Write(new ArraySegment<Byte>(bytes));
        }
    }
}
