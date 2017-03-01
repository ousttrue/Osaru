using System.Linq;


namespace JsonSan.Deserializers
{
    public class TypedArrayDeserializer<T> : DeserializerBase<T[]>
    {
        DeserializerBase<T> m_deserializer;

        public override void Setup(TypeRegistory r)
        {
            m_deserializer = r.GetDeserializer<T>();
        }

        public override void Deserialize(Node json, ref T[] outValue, TypeRegistory r)
        {
            var count = json.ArrayItems.Count();
            if (outValue == null || outValue.Length != count)
            {
                outValue = new T[count];
            }

            int i = 0;
            foreach (var item in json.ArrayItems)
            {
                m_deserializer.Deserialize(item, ref outValue[i++], r);
            }
        }
    }
}
