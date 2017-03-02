using System;
using System.Linq;


namespace NMessagePack.Deserializers
{
    public class ClassReflectionDeserializer<T> : DeserializerBase<T>
            where T : class
    {
        delegate void MemberDeserializeFunc(T obj, MsgPackValue value);
        MemberDeserializeFunc[] m_propDeserializer;

        public ClassReflectionDeserializer()
        {
            m_propDeserializer =
                typeof(T).GetProperties()
                .Where(x => x.PropertyType.IsSerializable())
                .Select(x =>
                {
                    var d = Deserializer.GetDeserializer(x.PropertyType);
                    var setter = x.CreateSetter<T>();
                    return new MemberDeserializeFunc((T obj, MsgPackValue value) =>
                    {
                        var v = d.DeserializeObject(value[x.Name]);
                        setter(ref obj, v);
                    });
                })
                .ToArray();
        }

        public override T Deserialize(MsgPackValue value)
        {
            var obj = Activator.CreateInstance<T>();
            foreach (var d in m_propDeserializer)
            {
                d(obj, value);
            }
            return obj;
        }
    }
}
