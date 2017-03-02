using System;


namespace ObjectStructure.Json.Deserializers
{
    public class LambdaDeserializer<T> : DeserializerBase<T>
    {
        public delegate void DeserializeFunc(Node json, ref T outValue, TypeRegistory r);
        DeserializeFunc m_deserializer;
        public LambdaDeserializer(DeserializeFunc deserializer)
        {
            m_deserializer = deserializer;
        }

        public override void Deserialize(Node json, ref T outValue, TypeRegistory r)
        {
            m_deserializer(json, ref outValue, r);
        }
    }
}
