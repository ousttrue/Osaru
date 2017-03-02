using System.Linq;


namespace ObjectStructure.Json.Deserializers
{
    public class TypedArrayDeserializer<T> : DeserializerBase<T[]>
    {
        DeserializerBase<T> m_elementDeserializer;

        public override void Setup(JsonSerializeTypeRegistory r)
        {
            m_elementDeserializer = r.GetDeserializer<T>();
        }

        public override void Deserialize(JsonParser json, ref T[] outValue, JsonSerializeTypeRegistory r)
        {
            var count = json.ArrayItems.Count();
            if (outValue == null || outValue.Length != count)
            {
                outValue = new T[count];
            }

            int i = 0;
            foreach (var item in json.ArrayItems)
            {
                m_elementDeserializer.Deserialize(item, ref outValue[i++], r);
            }
        }
    }
}
