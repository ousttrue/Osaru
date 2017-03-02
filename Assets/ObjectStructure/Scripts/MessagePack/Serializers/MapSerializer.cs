using System.Collections;


namespace NMessagePack.Serializers
{
    public class MapSerializer<T> : SerializerBase<T>
        where T : IDictionary
    {
        BoxingSerializer m_serializser;

        public MapSerializer()
        {
            m_serializser = new BoxingSerializer();
        }

        protected override void NonNullSerialize(MsgPackWriter w, T t)
        {
            w.MsgPackMap(t.Count);
            foreach (DictionaryEntry o in t)
            {
                m_serializser.Serialize(w, o.Key);
                m_serializser.Serialize(w, o.Value);
            }
        }
    }
}
