using System.Reflection;
using System.Linq;
using System;

namespace NMessagePack.Serializers
{
    public class ClassReflectionSerializer<T> : StringKeySerializer<T>
        where T : class
    {
        public ClassReflectionSerializer()
        {
            m_writers = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(pi =>
                        {
                            var nsAttrs = pi.GetCustomAttributes(typeof(NonSerializedAttribute), false);
                            return nsAttrs.Length == 0;
                        })
                .Select(x =>
                        {
                            var valueSerialize = x.CreatePropSerializeFunc<T>();
                            return new Writer((MsgPackWriter w, T t) =>
                                {
                                    m_keySerializer.Serialize(w, x.Name);
                                    valueSerialize(w, t);
                                });
                        })
            .ToArray();
        }

        protected override void NonNullSerialize(MsgPackWriter w, T t)
        {
            w.MsgPackMap(m_writers.Length);
            foreach (var writer in m_writers)
            {
                writer(w, t);
            }
        }
    }
}
