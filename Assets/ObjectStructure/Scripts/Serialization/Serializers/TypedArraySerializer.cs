namespace ObjectStructure.Serialization.Serializers
{
    public class TypedArraySerializer<T> : ISerializer<T[]>
    {
        ISerializer<T> m_elementSerializer = null;

        public void Setup(ITypeRegistory r)
        {
            m_elementSerializer = (ISerializer<T>)r.GetSerializer<T>();
        }

        public void Serialize(T[] t, IFormatter f)
        {
            f.OpenList(t.Length);
            foreach (var item in t)
            {
                m_elementSerializer.Serialize(item, f);
            }
            f.CloseLIst();
        }
    }
}
