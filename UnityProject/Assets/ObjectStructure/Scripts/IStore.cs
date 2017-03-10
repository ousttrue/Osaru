namespace ObjectStructure
{
    public interface IStore
    {
        void Clear();
        BytesSegment Bytes { get; }

        void Write(string src);
        void Write(char c);
    }
}
