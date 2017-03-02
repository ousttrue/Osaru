using System.Collections.Generic;
using System.Text;


namespace JsonSan.Serializers
{
    public class TypedArraySerializer<T> : SerializerBase<T[]>
    {
        ISerializer m_serializer = null;

        public override void Setup(TypeRegistory r)
        {
            m_serializer = r.GetSerializer<T>();
        }

        public override void Serialize(T[] t, TypeRegistory r, IWriteStream<char> w)
        {
            w.Write("[");
            bool isFirst = true;
            foreach(var item in t)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    w.Write(",");
                }
                m_serializer.Serialize(item, r, w);
            }
            w.Write("]");
        }
    }
}
