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

        public void Serialize(U t, IWriteStream w)
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
