using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ObjectStructure.Serialization.Deserializers
{
    public class FieldMemberDeserializer<T, U> : IMemberDeserializer<T>
    {
        public string MemberName
        {
            get { return m_fi.Name; }
        }

        FieldInfo m_fi;
        IDeserializerBase<U> m_deserializer;
        public FieldMemberDeserializer(FieldInfo fi, IDeserializerBase<U> d)
        {
            m_fi = fi;
            m_deserializer = d;
        }

        delegate void MemberDeserializer<PARSER>(IDeserializerBase<U> deserializer
            , PARSER parser, ref T memberOwner);
        MemberDeserializer<PARSER> CreateDeserializer<PARSER>(FieldInfo fi)
            where PARSER: IParser<PARSER>
        {
            var param0 = Expression.Parameter(typeof(IDeserializerBase<U>), "deserializer");
            var param1 = Expression.Parameter(typeof(PARSER), "parser");
            var param2 = Expression.Parameter(typeof(T).MakeByRefType(), "memberOwner");
            var genericMethod = typeof(IDeserializerBase<U>).GetMethod("Deserialize");
            var method = genericMethod.MakeGenericMethod(typeof(PARSER));
            var fld = Expression.Field(param2, fi);
            // cannot ref
            var call =Expression.Call(param0, method, param1, fld);

UnityEngine.Debug.Log(call.ToString());

            var lambda = Expression.Lambda<MemberDeserializer<PARSER>>(call, param0, param1, param2);
            return lambda.Compile();
        }

        Dictionary<Type, object> m_deserializerMap = new Dictionary<Type, object>();
        public void Deserialize<PARSER>(PARSER parser, ref T memberOwner)
            where PARSER : IParser<PARSER>
        {
            object d = null;
            var t = typeof(PARSER);
            if(!m_deserializerMap.TryGetValue(t, out d))
            {
                d = CreateDeserializer<PARSER>(m_fi);
                m_deserializerMap.Add(t, d);
            }
            var deserializer = (MemberDeserializer<PARSER>)d;
            deserializer(m_deserializer, parser, ref memberOwner);
        }

        public void DeserializeBoxed<PARSER>(PARSER parser, object boxed) where PARSER : IParser<PARSER>
        {
            var u = default(U);
            m_deserializer.Deserialize(parser, ref u);
            m_fi.SetValue(boxed, u);
        }
    }
}
