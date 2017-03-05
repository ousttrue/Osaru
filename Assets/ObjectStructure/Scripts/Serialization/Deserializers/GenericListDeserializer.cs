using System.Collections.Generic;


namespace ObjectStructure.Serialization.Deserializers
{
    public class GenericListDeserializer<T, U> : IDeserializer<U>
        where U: IList<T>
    {
        IDeserializer<T> m_elementDeserializer;

        public void Setup(TypeRegistory r)
        {
            m_elementDeserializer = r.GetDeserializer<T>();
        }

        public void Deserialize<PARSER>(PARSER json, ref U outValue)
            where PARSER : IParser<PARSER>
        {
            if (outValue == null)
            {
                outValue = (U)(IList<T>)new List<T>();
            }
            outValue.Clear();

            foreach (var item in json.ArrayItems)
            {
                var value = default(T);
                m_elementDeserializer.Deserialize(item, ref value);
                outValue.Add(value);
            }
        }
    }
}
