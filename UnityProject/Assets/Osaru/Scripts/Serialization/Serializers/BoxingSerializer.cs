using System;


namespace Osaru.Serialization.Serializers
{
    public class BoxingSerializer : SerializerBase<Object>
    {
        TypeRegistory m_r;
        public BoxingSerializer(TypeRegistory r)
        {
            m_r = r;
        }

        public override void Setup(TypeRegistory r)
        {
        }

        public override void Serialize(object t, IFormatter f)
        {
            m_r.SerializeBoxing(t, f);
        }
    }
}
