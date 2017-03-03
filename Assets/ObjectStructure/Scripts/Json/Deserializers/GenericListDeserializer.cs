using System.Collections.Generic;


namespace ObjectStructure.Json.Deserializers
{
    public class GenericListDeserializer<T> : DeserializerBase<List<T>>
    {
        DeserializerBase<T> m_elementDeserializer;

        public override void Setup(TypeRegistory r)
        {
            m_elementDeserializer = r.GetDeserializer<T>();
        }

        public override void Deserialize(JsonParser json, ref List<T> outValue, TypeRegistory r)
        {
            if (outValue == null)
            {
                outValue = new List<T>();
            }
            outValue.Clear();

            foreach (var item in json.ArrayItems)
            {
                var value = default(T);
                m_elementDeserializer.Deserialize(item, ref value, r);
                outValue.Add(value);
            }
        }
    }
}
