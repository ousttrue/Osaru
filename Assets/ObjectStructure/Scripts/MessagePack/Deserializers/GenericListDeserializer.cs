using System;
using System.Collections.Generic;


namespace NMessagePack.Deserializers
{
    public class GenericListDeserializer<T, V> : DeserializerBase<T>
        where T : IList<V>
    {
        DeserializerBase<V> m_v;

        public GenericListDeserializer()
        {
            m_v = Deserializer.GetDeserializer<V>();
        }

        public override T Deserialize(MsgPackValue value)
        {
            if (typeof(T).IsInterface)
            {
                var list = (T)Activator.CreateInstance(typeof(V[]), value.Count);
                int i = 0;
                foreach (var v in value.Values)
                {
                    list[i++] = m_v.Deserialize(v);
                }
                return list;
            }
            else
            {
                var list = Activator.CreateInstance<T>();
                foreach (var v in value.Values)
                {
                    list.Add(m_v.Deserialize(v));
                }
                return list;
            }
        }
    }
}
