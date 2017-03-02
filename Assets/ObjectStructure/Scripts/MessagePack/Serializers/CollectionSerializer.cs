using System.Collections;


namespace NMessagePack.Serializers
{
    public class CollectionSerializer<T> : SerializerBase<T>
        where T : ICollection
    {
        BoxingSerializer m_serializer;

        public CollectionSerializer()
        {
            m_serializer = new BoxingSerializer();
        }

        protected override void NonNullSerialize(MsgPackWriter w, T t)
        {
            w.MsgPackArray(t.Count);
            foreach (var o in t)
            {
                m_serializer.Serialize(w, o);
            }
        }
    }
}
