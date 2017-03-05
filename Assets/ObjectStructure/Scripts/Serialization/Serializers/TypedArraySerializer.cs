namespace ObjectStructure.Serialization.Serializers
{
    public class TypedArraySerializer<T> : SerializerBase<T[]>
    {
        SerializerBase<T> m_elementSerializer = null;

        public override void Setup(TypeRegistory r)
        {
            m_elementSerializer = (SerializerBase<T>)r.GetSerializer<T>();
        }

        public override void Serialize(T[] t, IFormatter f)
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
