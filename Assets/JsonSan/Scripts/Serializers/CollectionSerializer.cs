using System.Collections;
using System.Text;


namespace JsonSan.Serializers
{
    public class CollectionSerializer<T> : SerializerBase<T>
        where T: ICollection
    {
        public override string Serialize(T t, TypeRegistory r)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            bool isFirst = true;
            foreach(var item in t)
            {
                if (isFirst)
                {
                    isFirst = false; ;
                }
                else
                {
                    sb.Append(",");
                }
                var serializer = r.GetSerializer(item.GetType());
                sb.Append(serializer.Serialize(item, r));
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
