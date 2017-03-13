using System;
using System.Collections.Generic;
using System.Linq;


namespace Osaru.Serialization.Deserializers
{
    public class ClassReflectionDeserializer<T> : IDeserializerBase<T>
        where T : class
    {
        Dictionary<string, IMemberDeserializer<T>> m_deserializers 
            = new Dictionary<string, IMemberDeserializer<T>>();

        public void Setup(TypeRegistory r)
        {
            m_deserializers = r.GetMemberDeserializers<T>()
                .ToDictionary(x => x.MemberName, x=>x)
                ;
        }

        public void Deserialize<PARSER>(PARSER json, ref T memberOwner)
            where PARSER: IParser<PARSER>
        {
            if (json.IsNull)
            {
                memberOwner = null;
                return;
            }

            if (memberOwner == null)
            {
                memberOwner=Activator.CreateInstance<T>();
            }

            foreach(var kv in json.ObjectItems)
            {
                IMemberDeserializer<T> d;
                if(m_deserializers.TryGetValue(kv.Key, out d))
                {
                    d.Deserialize(kv.Value, ref memberOwner);
                }
            }
        }
    }
}
