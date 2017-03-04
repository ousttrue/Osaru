using System.Linq;


namespace ObjectStructure.Serialization.Deserializers
{
    public class TypedArrayDeserializer<T> : IDeserializer<T[]>
    {
        IDeserializer<T> m_elementDeserializer;

        public void Setup(ITypeRegistory r)
        {
            m_elementDeserializer = r.GetDeserializer<T>();
        }

        public void Deserialize<PARSER>(PARSER json, ref T[] outValue)
        where PARSER : IParser<PARSER>
        {
            var count = json.ArrayItems.Count();
            if (outValue == null || outValue.Length != count)
            {
                outValue = new T[count];
            }

            int i = 0;
            foreach (var item in json.ArrayItems)
            {
                m_elementDeserializer.Deserialize(item, ref outValue[i++]);
            }
        }
    }
}
