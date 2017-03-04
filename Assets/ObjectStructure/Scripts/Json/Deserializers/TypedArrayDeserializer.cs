using System.Linq;


namespace ObjectStructure.Json.Deserializers
{
    public class TypedArrayDeserializer<PARSER, T> : IDeserializer<PARSER, T[]>
        where PARSER : IParser<PARSER>
    {
        IDeserializer<PARSER, T> m_elementDeserializer;

        public void Setup(ITypeRegistory r)
        {
            m_elementDeserializer = (IDeserializer<PARSER, T>)r.GetDeserializer<T>();
        }

        public void Deserialize(PARSER json, ref T[] outValue)
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
