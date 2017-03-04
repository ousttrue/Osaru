using System.Collections.Generic;


namespace ObjectStructure.Json.Serializers
{
    public class GenericListSerializer<T> : ISerializer<IList<T>>
    {
        ISerializer<T> m_elementSerializer;

        public void Setup(ITypeRegistory r)
        {
            m_elementSerializer = (ISerializer<T>)r.GetSerializer<T>();
        }

        public void Serialize(IList<T> t, IWriteStream w)
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
