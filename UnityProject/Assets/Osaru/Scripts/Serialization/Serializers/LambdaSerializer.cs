using System;
using System.Reflection;

namespace Osaru.Serialization.Serializers
{
    public class LambdaSerializer<T> : SerializerBase<T>
    {
        Action<T, IFormatter> m_serializer;
        LambdaSerializer()
        { }
        public LambdaSerializer(Action<T, IFormatter> serializer)
        {
            m_serializer = serializer;
        }

        public override void Setup(TypeRegistry r)
        {
        }

        public override void Serialize(T t, IFormatter f)
        {
            m_serializer(t, f);
        }

        public static LambdaSerializer<T> Create(MethodInfo mi)
        {
            return new LambdaSerializer<T>(
                (x, f) => mi.Invoke(null, new object[] { x, f })
                );
        }
    }
}
