using System;


namespace ObjectStructure
{
    public interface IStore
    {
        void Clear();
        ArraySegment<Byte> Bytes { get; }

        void Write(ArraySegment<Byte> bytes);
        void Write(string src);
        void Write(char c);
    }
}
