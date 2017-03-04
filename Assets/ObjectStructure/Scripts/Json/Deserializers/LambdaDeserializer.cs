using System;
using System.Collections.Generic;


namespace ObjectStructure.Json.Deserializers
{
    public class LambdaDeserializer<T> : DeserializerBase<T>
    {
        public delegate void DeserializeFunc(IParser json, ref T outValue, ITypeRegistory r);
        DeserializeFunc m_deserializer;
        public LambdaDeserializer(DeserializeFunc deserializer)
        {
            m_deserializer = deserializer;
        }

        public override void Deserialize<PARSER>(PARSER json, ref T outValue, ITypeRegistory r)
        {
            m_deserializer(json, ref outValue, r);
        }
    }
}
