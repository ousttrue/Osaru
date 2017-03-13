using System.Collections.Generic;


namespace Osaru.Serialization.Deserializers
{
    public class GenericListDeserializer<T, U> : IDeserializerBase<U>
        where U: IList<T>
    {
        IDeserializerBase<T> m_elementDeserializer;

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

            foreach (var item in json.ListItems)
            {
                var value = default(T);
                m_elementDeserializer.Deserialize(item, ref value);
                outValue.Add(value);
            }
        }
    }
}
