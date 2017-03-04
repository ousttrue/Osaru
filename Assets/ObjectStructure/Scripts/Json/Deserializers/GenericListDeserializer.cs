using System;
using System.Collections.Generic;


namespace ObjectStructure.Json.Deserializers
{
    public class GenericListDeserializer<PARSER, T, U> : IDeserializer<PARSER, U>
        where PARSER : IParser<PARSER>
        where U: IList<T>
    {
        IDeserializer<PARSER, T> m_elementDeserializer;

        public void Setup(ITypeRegistory r)
        {
            m_elementDeserializer = (IDeserializer<PARSER, T>)r.GetDeserializer<T>();
        }

        public void Deserialize(PARSER json, ref U outValue)
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
