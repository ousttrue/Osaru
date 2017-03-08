namespace ObjectStructure
{
    public interface IWriteStream: IStore<string>
    {
        void Write(string src);
        void Write(char c);
    }
}
