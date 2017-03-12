using System.Linq;


namespace ObjectStructure.Serialization.Serializers
{
    public class ReflectionSerializer<T> : SerializerBase<T>
        where T: struct
    {
        IMemberSerializer<T>[] m_serializers;

        public override void Setup(TypeRegistory r)
        {
            m_serializers = r.GetMemberSerializers<T>().ToArray();
        }

        public override void Serialize(T t, IFormatter f)
        {
            f.BeginMap(m_serializers.Count());
            foreach (var s in m_serializers)
            {
                f.Key(s.MemberName);
                s.Serialize(ref t, f);
            }
            f.EndMap();
        }
    }
}
