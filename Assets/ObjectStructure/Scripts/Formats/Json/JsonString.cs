using System;
using System.Text;


namespace ObjectStructure.Json
{
    public static class JsonString
    {
        #region Quote
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
        #endregion

        #region Unquote
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
            while (i <= length)
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
            var str = sb.ToString();
            if (str.Length < 32)
            {
                str = String.Intern(str);
            }
            return str;
        }
        #endregion
    }
}
