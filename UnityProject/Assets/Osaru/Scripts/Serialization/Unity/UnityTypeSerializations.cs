using Osaru.Serialization.Deserializers;
using Osaru.Serialization.Serializers;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Osaru.Serialization
{

    public class Vector2Deserializer : IDeserializerBase<Vector2>
    {
        IDeserializerBase<Single> m_d;
        public void Setup(TypeRegistry r)
        {
            m_d = r.GetDeserializer<Single>();
        }
        public void Deserialize<PARSER>(PARSER parser, ref Vector2 outValue) where PARSER : IParser<PARSER>
        {
            var it = parser.ListItems.GetEnumerator();
            var a = it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.x);
            var b = it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.y);
        }
    }
    public class Vector3Deserializer : IDeserializerBase<Vector3>
    {
        IDeserializerBase<Single> m_d;
        public void Setup(TypeRegistry r)
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
    public class Vector4Deserializer : IDeserializerBase<Vector4>
    {
        IDeserializerBase<Single> m_d;
        public void Setup(TypeRegistry r)
        {
            m_d = r.GetDeserializer<Single>();
        }
        public void Deserialize<PARSER>(PARSER parser, ref Vector4 outValue) where PARSER : IParser<PARSER>
        {
            var it = parser.ListItems.GetEnumerator();
            var a = it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.x);
            var b = it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.y);
            var c = it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.z);
            var d = it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.w);
        }
    }
    public class QuaternionDeserializer : IDeserializerBase<Quaternion>
    {
        IDeserializerBase<Single> m_d;
        public void Setup(TypeRegistry r)
        {
            m_d = r.GetDeserializer<Single>();
        }
        public void Deserialize<PARSER>(PARSER parser, ref Quaternion outValue) where PARSER : IParser<PARSER>
        {
            var it = parser.ListItems.GetEnumerator();
            var a = it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.x);
            var b = it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.y);
            var c = it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.z);
            var d = it.MoveNext();
            m_d.Deserialize(it.Current, ref outValue.w);
        }
    }

    public static class UnityTypeSerializations
    {
        public static IEnumerable<TypeSerialization> Serializations
        {
            get
            {
                yield return TypeSerialization.Create(
                    new LambdaSerializer<Vector2>((t, f) =>
                    {
                        f.BeginList(2);
                        f.Value(t.x);
                        f.Value(t.y);
                        f.EndList();
                    })
                    , new Vector2Deserializer()
                    );

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

                yield return TypeSerialization.Create(
                    new LambdaSerializer<Vector4>((t, f) =>
                    {
                        f.BeginList(4);
                        f.Value(t.x);
                        f.Value(t.y);
                        f.Value(t.z);
                        f.Value(t.w);
                        f.EndList();
                    })
                    , new Vector4Deserializer()
                    );

                yield return TypeSerialization.Create(
                    new LambdaSerializer<Quaternion>((t, f) =>
                    {
                        f.BeginList(4);
                        f.Value(t.x);
                        f.Value(t.y);
                        f.Value(t.z);
                        f.Value(t.w);
                        f.EndList();
                    })
                    , new QuaternionDeserializer()
                    );

#if UNITY_EDITOR || UNITY_WSA || UNITY_STANDALONE
#endif
            }
        }
    }
}
