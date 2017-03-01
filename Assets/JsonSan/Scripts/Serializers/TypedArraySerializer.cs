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

        public override string Serialize(T[] t, TypeRegistory r)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            bool isFirst = true;
            foreach(var item in t)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sb.Append(",");
                }
                sb.Append(m_serializer.Serialize(item, r));
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
