using System;
using System.Collections.Generic;


namespace NMessagePack.Deserializers
{
    public class GenericMapDeserializer<T, K, V> : DeserializerBase<T>
        where T : IDictionary<K, V>
    {
        DeserializerBase<K> m_k;
        DeserializerBase<V> m_v;

        public GenericMapDeserializer()
        {
            m_k = Deserializer.GetDeserializer<K>();
            m_v = Deserializer.GetDeserializer<V>();
        }

        public override T Deserialize(MsgPackValue value)
        {
            var list = Activator.CreateInstance<T>();
            foreach (var kv in value.Items)
            {
                list.Add(
                    m_k.Deserialize(kv.Key)
                    , m_v.Deserialize(kv.Value)
                    );
            }
            return list;
        }
    }
}
