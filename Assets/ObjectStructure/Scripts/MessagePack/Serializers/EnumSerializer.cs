using System;


namespace NMessagePack.Serializers
{
    public class EnumSerializer<T> : SerializerBase<T>
    {
        ISerializer m_serializer;
        public EnumSerializer()
        {
            m_serializer = Serializer.GetSerializer(Enum.GetUnderlyingType(typeof(T)));
        }
        protected override void NonNullSerialize(MsgPackWriter w, T t)
        {
            var converted = Convert.ChangeType(t, Enum.GetUnderlyingType(typeof(T)));
            m_serializer.Serialize(w, converted);
        }
    }
}
