using System;
using System.Text;


namespace ObjectStructure.Json.Serializers
{
    public class StringSerializer : ISerializer<String>
    {
        public void Setup(ITypeRegistory r)
        {
            //throw new NotImplementedException();
        }

        public void Serialize(String s, IWriteStream w)
        {
            Quote(s, w);
        }

        public static void Escape(String s, IWriteStream w)
        {
            var it = s.GetEnumerator();
            while(it.MoveNext())
            {
                switch(it.Current)
                {
                    case '"':
                    case '\\':
                    case '/':
                        // \\ prefix
                        w.Write('\\');
                        w.Write(it.Current);
                        break;

                    case '\b':
                        w.Write('\\');
                        w.Write('b');
                        break;
                    case '\f':
                        w.Write('\\');
                        w.Write('f');
                        break;
                    case '\n':
                        w.Write('\\');
                        w.Write('n');
                        break;
                    case '\r':
                        w.Write('\\');
                        w.Write('r');
                        break;
                    case '\t':
                        w.Write('\\');
                        w.Write('t');
                        break;

                    default:
                        w.Write(it.Current);
                        break;
                }
            }
        }
        public static string Escape(String s)
        {
            var sb = new StringBuilder();
            Escape(s, new StringBuilderStream(sb));
            return sb.ToString();
        }

        public static void Quote(String s, IWriteStream w)
        {
            w.Write('"');
            Escape(s, w);
            w.Write('"');
        }
        public static string Quote(string s)
        {
            var sb = new StringBuilder();
            Quote(s, new StringBuilderStream(sb));
            return sb.ToString();
        }
    }
}
