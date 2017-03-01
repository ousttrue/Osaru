using System.Collections.Generic;


namespace JsonSan.Deserializers
{
    public class GenericListDeserializer<T> : DeserializerBase<List<T>>
    {
        DeserializerBase<T> m_deserializer;

        public override void Setup(TypeRegistory r)
        {
            m_deserializer = r.GetDeserializer<T>();
        }

        public override void Deserialize(Node json, ref List<T> outValue, TypeRegistory r)
        {
            if (outValue == null)
            {
                outValue = new List<T>();
            }
            outValue.Clear();

            foreach (var item in json.ArrayItems)
            {
                var value = default(T);
                m_deserializer.Deserialize(item, ref value, r);
                outValue.Add(value);
            }
        }
    }
}
