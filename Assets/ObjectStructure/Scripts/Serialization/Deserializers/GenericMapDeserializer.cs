using System;
using System.Collections.Generic;


namespace ObjectStructure.MessagePack.Deserializers
{
    /*
    public class GenericMapDeserializer<T, K, V> : IDeserializer<T>
        where T : IDictionary<K, V>
    {
        IDeserializer<K> m_k;
        IDeserializer<V> m_v;

        public GenericMapDeserializer()
        {
            m_k = Deserializer.GetDeserializer<K>();
            m_v = Deserializer.GetDeserializer<V>();
        }

        public override T Deserialize(MsgPackValue value)
        {
            var list = Activator.CreateInstance<T>();
            foreach (var kv in value.ObjectItems)
            {
                list.Add(
                    m_k.Deserialize(kv.Key)
                    , m_v.Deserialize(kv.Value)
                    );
            }
            return list;
        }
    }
    */
}
