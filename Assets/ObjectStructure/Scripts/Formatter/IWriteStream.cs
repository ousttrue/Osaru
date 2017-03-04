namespace ObjectStructure
{
    public interface IWriteStream
    {
        void Clear();
        void Write(string src);
        void Write(char c);
    }
}
