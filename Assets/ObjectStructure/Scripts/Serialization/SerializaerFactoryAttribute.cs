using System;

namespace ObjectStructure.Serialization
{
    public delegate void SerializeFunc<T>(T t, IFormatter f);
    public class SerializerFactoryAttribute: Attribute{}
    public class SerializerAttribute : Attribute
    {
        public Type Type { get; private set; }
        public SerializerAttribute(Type t)
        {
            Type = t;
        }
        public SerializerAttribute()
        {
        }
    }
}
