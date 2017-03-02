namespace NMessagePack.Deserializers
{
    public class ArrayDeserializer<T> : DeserializerBase<T[]>
    {
        public override T[] Deserialize(MsgPackValue value)
        {
            var list = new T[value.Count];
            var d = Deserializer.GetDeserializer<T>();
            int i = 0;
            foreach(var v in value.Values)
            {
                list[i++] = d.Deserialize(v);
            }
            return list;
        }
    }
}
