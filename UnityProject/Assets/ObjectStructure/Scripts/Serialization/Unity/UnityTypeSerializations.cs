using ObjectStructure.Serialization.Deserializers;
using ObjectStructure.Serialization.Serializers;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ObjectStructure.Serialization
{
    public class Vector3Deserializer : IDeserializerBase<Vector3>
    {
        IDeserializerBase<Single> m_d;
        public void Setup(TypeRegistory r)
        {
            m_d = r.GetDeserializer<Single>();
        }
        public void Deserialize<PARSER>(PARSER parser, ref Vector3 outValue) where PARSER : IParser<PARSER>
        {
            var it = parser.ListItems.GetEnumerator();
            var a=it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.x);
            var b=it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.y);
            var c=it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.z);
        }
    }

    public static class UnityTypeSerializations
    {
        public static IEnumerable<TypeSerialization> Serializations
        {
            get
            {
                yield return TypeSerialization.Create(
                    new LambdaSerializer<Vector3>((t, f) =>
                    {
                        f.BeginList(3);
                        f.Value(t.x);
                        f.Value(t.y);
                        f.Value(t.z);
                        f.EndList();
                    })
                    , new Vector3Deserializer()
                    );

#if UNITY_EDITOR || UNITY_WSA || UNITY_STANDALONE
#endif
            }
        }
    }
}
