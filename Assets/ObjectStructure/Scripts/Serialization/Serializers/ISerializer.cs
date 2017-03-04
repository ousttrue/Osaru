namespace ObjectStructure.Serialization.Serializers
{
    public interface ISerializer<T> : ITypeInitializer
    {
        void Serialize(T t, IWriteStream w);
    }
}
