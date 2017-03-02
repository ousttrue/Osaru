namespace ObjectStructure.Json.Serializers
{
    public class TypedArraySerializer<T> : SerializerBase<T[]>
    {
        ISerializer m_elementSerializer = null;

        public override void Setup(JsonSerializeTypeRegistory r)
        {
            m_elementSerializer = r.GetSerializer<T>();
        }

        public override void Serialize(T[] t, IWriteStream w, JsonSerializeTypeRegistory r)
        {
            w.Write('[');
            bool isFirst = true;
            foreach (var item in t)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    w.Write(',');
                }
                m_elementSerializer.Serialize(item, w, r);
            }
            w.Write(']');
        }
    }
}
