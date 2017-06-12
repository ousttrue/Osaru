using System.Collections.Generic;


namespace Osaru.Serialization.Serializers
{
    public class GenericListSerializer<T, U> : ClassSerializerBase<U>
        where U: class, IList<T>
    {
        SerializerBase<T> m_elementSerializer;

        public override void Setup(TypeRegistry r)
        {
            m_elementSerializer = (SerializerBase<T>)r.GetSerializer<T>();
        }

        public override void NonNullSerialize(U t, IFormatter f)
        {
            f.BeginList(t.Count);
            foreach (var item in t)
            {
                m_elementSerializer.Serialize(item, f);
            }
            f.EndList();
        }
    }
}
