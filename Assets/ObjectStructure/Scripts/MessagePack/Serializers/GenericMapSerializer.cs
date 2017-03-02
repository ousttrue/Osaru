using System.Collections.Generic;


namespace NMessagePack.Serializers
{
    public class GenericMapSerializer<K, V> : SerializerBase<IDictionary<K, V>>
    {
        SerializerBase<K> m_keySerializer;
        SerializerBase<V> m_valueSerializer;

        public GenericMapSerializer()
        {
            m_keySerializer = Serializer.GetSerializer<K>();
            m_valueSerializer = Serializer.GetSerializer<V>();
        }

        protected override void NonNullSerialize(MsgPackWriter w, IDictionary<K, V> t)
        {
            w.MsgPackMap(t.Count);
            foreach (var o in t)
            {
                m_keySerializer.Serialize(w, o.Key);
                m_valueSerializer.Serialize(w, o.Value);
            }
        }
    }
}
