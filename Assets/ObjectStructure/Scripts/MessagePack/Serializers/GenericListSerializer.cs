using System.Collections.Generic;


namespace NMessagePack.Serializers
{
    public class GenericListSerializer<T> : SerializerBase<IList<T>>
    {
        SerializerBase<T> m_serializer;

        public GenericListSerializer()
        {
            m_serializer = Serializer.GetSerializer<T>();
        }

        protected override void NonNullSerialize(MsgPackWriter w, IList<T> t)
        {
            w.MsgPackArray(t.Count);
            foreach (var o in t)
            {
                m_serializer.Serialize(w, o);
            }
        }
    }
}
