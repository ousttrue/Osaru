using System.Collections.Generic;


namespace ObjectStructure.Json.Serializers
{
    public class GenericListSerializer<T> : SerializerBase<IList<T>>
    {
        ISerializer m_elementSerializer;

        public override void Setup(TypeRegistory r)
        {
            m_elementSerializer= r.GetSerializer<T>();
        }

        public override void Serialize(IList<T> t, IWriteStream w, TypeRegistory r)
        {
            w.Write('[');
            bool isFirst = true;
            foreach(var item in t)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    w.Write(',');
                }
                m_elementSerializer.Serialize(item, w, r);
            }
            w.Write(']');
        }
    }
}
