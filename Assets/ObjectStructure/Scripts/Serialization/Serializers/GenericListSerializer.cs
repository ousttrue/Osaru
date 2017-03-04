using System.Collections.Generic;


namespace ObjectStructure.Serialization.Serializers
{
    public class GenericListSerializer<T, U> : ISerializer<U>
        where U: IList<T>
    {
        ISerializer<T> m_elementSerializer;

        public void Setup(ITypeRegistory r)
        {
            m_elementSerializer = (ISerializer<T>)r.GetSerializer<T>();
        }

        public void Serialize(U t, IFormatter f)
        {
            f.OpenList(t.Count);
            foreach (var item in t)
            {
                m_elementSerializer.Serialize(item, f);
            }
            f.CloseLIst();
        }
    }
}
