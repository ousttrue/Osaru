using System;
using System.Text;

namespace ObjectStructure.Json.Serializers
{
    public class StringSerializer : SerializerBase<String>
    {
        public override void Serialize(String s, IWriteStream w, TypeRegistory r)
        {
            StaticSerialize(s, w);
        }

        public static void StaticSerialize(String s, IWriteStream w)
        {
            w.Write('"');
            w.Write(s);
            w.Write('"');
        }

        public static string Quote(string src)
        {
            var sb = new StringBuilder();
            StaticSerialize(src, new StringBuilderStream(sb));
            return sb.ToString();
        }
    }
}
