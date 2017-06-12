using System.Collections.Generic;
using System.Linq;


namespace Osaru.Serialization.Deserializers
{
    public class StructReflectionDeserializer<T> : IDeserializerBase<T>
        where T: struct
    {
        Dictionary<string, IMemberDeserializer<T>> m_deserializers
            =new Dictionary<string, IMemberDeserializer<T>>();

        public void Setup(TypeRegistry r)
        {
            m_deserializers = r.GetMemberDeserializers<T>()
                .ToDictionary(x => x.MemberName, x => x)
                ;
        }

        public void Deserialize<PARSER>(PARSER json, ref T memberOwner)
            where PARSER: IParser<PARSER>
        {
#if true
            // boxing
            var boxed = (object)memberOwner;
            foreach(var kv in json.ObjectItems)
            {
                IMemberDeserializer<T> d;
                if(m_deserializers.TryGetValue(kv.Key, out d))
                {
                    d.DeserializeBoxed(kv.Value, boxed);
                }
            }
            // unboxing
            memberOwner = (T)boxed;
#else
            foreach (var kv in json.ObjectItems)
            {
                IMemberDeserializer<T> d;
                if (m_deserializers.TryGetValue(kv.Key, out d))
                {
                    d.Deserialize(kv.Value, ref memberOwner);
                }
            }
#endif
        }
    }
}
