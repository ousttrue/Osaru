using System;


namespace ObjectStructure.Json.Deserializers
{
    public class StringDeserializer : DeserializerBase<String>
    {
        public override void Deserialize(JsonParser json, ref String outValue, TypeRegistory r)
        {
        }

        public static string Unquote(string src)
        {
            return src.Substring(1, src.Length - 2);
        }
    }
}
