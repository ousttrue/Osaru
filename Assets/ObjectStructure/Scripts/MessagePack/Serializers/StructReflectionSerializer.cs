using System;
using System.Linq;
using System.Reflection;


namespace NMessagePack.Serializers{
    public class StructReflectionSerializer<T> : StringKeySerializer<T>
        where T : struct
    {
        public StructReflectionSerializer()
        {
            var props =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
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
                ;

            var flds =
                typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance).Where(pi =>
                {
                    var nsAttrs = pi.GetCustomAttributes(typeof(NonSerializedAttribute), false);
                    return nsAttrs.Length == 0;
                })
                .Select(x =>
                {
                    var valueSerialize = x.CreateFieldSerializeFunc<T>();
                    return new Writer((MsgPackWriter w, T t) =>
                    {
                        m_keySerializer.Serialize(w, x.Name);
                        valueSerialize(w, t);
                    });
                })
                ;

            m_writers = props.Concat(flds).ToArray();
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
