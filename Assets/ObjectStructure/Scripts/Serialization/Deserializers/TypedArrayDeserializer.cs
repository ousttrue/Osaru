using System.Linq;


namespace ObjectStructure.Serialization.Deserializers
{
    public class TypedArrayDeserializer<T> : IDeserializerBase<T[]>
    {
        IDeserializerBase<T> m_elementDeserializer;

        public void Setup(TypeRegistory r)
        {
            m_elementDeserializer = r.GetDeserializer<T>();
        }

        public void Deserialize<PARSER>(PARSER json, ref T[] outValue)
        where PARSER : IParser<PARSER>
        {
            var count = json.ListItems.Count();
            if (outValue == null || outValue.Length != count)
            {
                outValue = new T[count];
            }

            int i = 0;
            foreach (var item in json.ListItems)
            {
                m_elementDeserializer.Deserialize(item, ref outValue[i++]);
            }
        }
    }
}
