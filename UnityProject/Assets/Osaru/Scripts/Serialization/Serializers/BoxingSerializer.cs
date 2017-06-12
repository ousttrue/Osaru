using System;


namespace Osaru.Serialization.Serializers
{
    public class BoxingSerializer : SerializerBase<Object>
    {
        TypeRegistry m_r;
        public BoxingSerializer(TypeRegistry r)
        {
            m_r = r;
        }

        public override void Setup(TypeRegistry r)
        {
        }

        public override void Serialize(object t, IFormatter f)
        {
            m_r.SerializeBoxing(t, f);
        }
    }
}
