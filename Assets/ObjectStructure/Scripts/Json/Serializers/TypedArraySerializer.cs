namespace ObjectStructure.Json.Serializers
{
    public class TypedArraySerializer<T> : ISerializer<T[]>
    {
        ISerializer<T> m_elementSerializer = null;

        public void Setup(ITypeRegistory r)
        {
            m_elementSerializer = (ISerializer<T>)r.GetSerializer<T>();
        }

        public void Serialize(T[] t, IWriteStream w)
        {
            w.Write('[');
            bool isFirst = true;
            foreach (var item in t)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    w.Write(',');
                }
                m_elementSerializer.Serialize(item, w);
            }
            w.Write(']');
        }
    }
}
