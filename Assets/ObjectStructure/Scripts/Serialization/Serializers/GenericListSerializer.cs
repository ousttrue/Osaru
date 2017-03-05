using System.Collections.Generic;


namespace ObjectStructure.Serialization.Serializers
{
    public class GenericListSerializer<T, U> : ClassSerializerBase<U>
        where U: class, IList<T>
    {
        ISerializer<T> m_elementSerializer;

        public override void Setup(TypeRegistory r)
        {
            m_elementSerializer = (ISerializer<T>)r.GetSerializer<T>();
        }

        public override void NonNullSerialize(U t, IFormatter f)
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
