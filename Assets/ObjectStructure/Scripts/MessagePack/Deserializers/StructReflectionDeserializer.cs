using System;
using System.Linq;


namespace NMessagePack.Deserializers
{
    public class StructReflectionDeserializer<T> : DeserializerBase<T>
            where T : struct
    {
        delegate void MemberDeserializeFunc(ref T obj, MsgPackValue value);
        MemberDeserializeFunc[] m_propDeserializer;

        public StructReflectionDeserializer()
        {
            var flds = typeof(T).GetFields()
                .Where(x => x.FieldType.IsSerializable())
                .Select(x =>
                {
                    var d = Deserializer.GetDeserializer(x.FieldType);
                    var setter = x.CreateSetter<T>();
                    return new MemberDeserializeFunc((ref T obj, MsgPackValue value) =>
                    {
                        var v = d.DeserializeObject(value[x.Name]);
                        setter(ref obj, v);
                    });
                })
                ;
            var props =
                typeof(T).GetProperties()
                .Where(x => x.PropertyType.IsSerializable())
                .Select(x =>
                {
                    var d = Deserializer.GetDeserializer(x.PropertyType);
                    var setter = x.CreateSetter<T>();
                    return new MemberDeserializeFunc((ref T obj, MsgPackValue value) =>
                    {
                        var v = d.DeserializeObject(value[x.Name]);
                        setter(ref obj, v);
                    });
                });

            m_propDeserializer = flds.Concat(props).ToArray();
        }

        public override T Deserialize(MsgPackValue value)
        {
            var obj = Activator.CreateInstance<T>();
            foreach (var p in m_propDeserializer)
            {
                p(ref obj, value);
            }
            return obj;
        }
    }
}
