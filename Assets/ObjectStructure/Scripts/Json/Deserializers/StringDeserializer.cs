using System;
using System.Text;


namespace ObjectStructure.Json.Deserializers
{
    public class StringDeserializer : DeserializerBase<String>
    {
        public override void Deserialize(JsonParser json, ref String outValue, TypeRegistory r)
        {
            outValue = json.GetString();
        }

        public static void Unescape(string src, IWriteStream w)
        {
            int i = 0;
            int length = src.Length - 1;
            while (i < length)
            {
                if (src[i] == '\\')
                {
                    var c = src[i + 1];
                    switch (c)
                    {
                        case '\\':
                        case '/':
                        case '"':
                            // remove prefix
                            w.Write(c);
                            i += 2;
                            continue;

                        case 'b':
                            w.Write('\b');
                            i += 2;
                            continue;
                        case 'f':
                            w.Write('\f');
                            i += 2;
                            continue;
                        case 'n':
                            w.Write('\n');
                            i += 2;
                            continue;
                        case 'r':
                            w.Write('\r');
                            i += 2;
                            continue;
                        case 't':
                            w.Write('\t');
                            i += 2;
                            continue;
                    }
                }

                w.Write(src[i]);
                i += 1;
            }
            while(i<=length)
            {
                w.Write(src[i++]);
            }
        }
        public static string Unescape(string src)
        {
            var sb = new StringBuilder();
            Unescape(src, new StringBuilderStream(sb));
            return sb.ToString();
        }

        public static void Unquote(string src, IWriteStream w)
        {
            Unescape(src.Substring(1, src.Length - 2), w);
        }
        public static string Unquote(string src)
        {
            var sb = new StringBuilder();
            Unquote(src, new StringBuilderStream(sb));
            return sb.ToString();
        }
    }
}
