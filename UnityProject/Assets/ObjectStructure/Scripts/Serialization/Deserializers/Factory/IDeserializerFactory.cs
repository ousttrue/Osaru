using System;

namespace ObjectStructure.Serialization.Deserializers
{
    public class DeserializerFactoryAttribute : Attribute
    { }

    public interface IDeserializerFactory
    {
        IDeserializer Create(Type t);
    }

    public class LambdaDeserializerFactory : IDeserializerFactory
    {
        Func<Type,IDeserializer> m_factory;
        public IDeserializer Create(Type t)
        {
            return m_factory(t);
        }
        public static LambdaDeserializerFactory CreateFactory<T>(
            IDeserializerBase<T> d)
        {
            return new LambdaDeserializerFactory
            {
                m_factory = x =>
                {
                    if (x != typeof(T)) return null;
                    return d;
                }
            };
        }
    }
}
