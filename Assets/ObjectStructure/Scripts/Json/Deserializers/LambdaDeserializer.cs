using System;


namespace ObjectStructure.Json.Deserializers
{
    public class LambdaDeserializer<T> : DeserializerBase<T>
    {
        public delegate void DeserializeFunc(JsonParser json, ref T outValue, JsonSerializeTypeRegistory r);
        DeserializeFunc m_deserializer;
        public LambdaDeserializer(DeserializeFunc deserializer)
        {
            m_deserializer = deserializer;
        }

        public override void Deserialize(JsonParser json, ref T outValue, JsonSerializeTypeRegistory r)
        {
            m_deserializer(json, ref outValue, r);
        }
    }
}
