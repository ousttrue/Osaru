using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// reference: http://www.json.org/json-ja.html
/// </summary>
namespace JsonSan
{
    public enum JsonValueType
    {
        Unknown,

        String,
        Number,
        Object,
        Array,
        Boolean,

        Close, // internal use
    }

    public struct StringSegment : IEnumerable<Char>
    {
        public string Value;
        public int Offset;
        public int Count;

        public char this[int index]
        {
            get
            {
                if (index >= Count) throw new ArgumentOutOfRangeException();
                return Value[Offset + index];
            }
        }

        public StringSegment(string value) : this(value, 0, value.Length) { }
        public StringSegment(string value, int offset) : this(value, offset, value.Length - offset) { }
        public StringSegment(string value, int offset, int count)
        {
            Value = value;
            Offset = offset;
            Count = count;
        }

        public bool IsMatch(string str)
        {
            if (Count != str.Length) return false;
            return Value.Substring(Offset, Count) == str;
        }

        public override string ToString()
        {
            return Value.Substring(Offset, Count);
        }

        public IEnumerator<char> GetEnumerator()
        {
            return Value.Skip(Count).Take(Count).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public StringSegment Take(int n)
        {
            if (n > Count) throw new ArgumentOutOfRangeException();
            return new StringSegment(Value, Offset, n);
        }

        public StringSegment Skip(int n)
        {
            if (n > Count) throw new ArgumentOutOfRangeException();
            return new StringSegment(Value, Offset + n, Count - n);
        }

        public bool TrySearch(Func<Char, bool> pred, out int pos)
        {
            pos = 0;
            for (; pos < Count; ++pos)
            {
                if (pred(this[pos]))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public struct Node
    {
        StringSegment m_segment;
        public StringSegment Segment
        {
            get { return m_segment; }
        }

        public bool IsParsedToEnd
        {
            get;
            private set;
        }

        public void ParseToEnd()
        {
            if (ValueType != JsonValueType.Object && ValueType != JsonValueType.Array)
            {
                throw new FormatException("require object or arrray");
            }
            if (IsParsedToEnd)
            {
                throw new InvalidOperationException("already parsed");
            }

            var close = GetNodes(true).Last();
            if (close.ValueType != JsonValueType.Close)
            {
                throw new FormatException("close expected");
            }
            m_segment = m_segment.Take(close.Start + 1 - m_segment.Offset);
            IsParsedToEnd = true;
        }

        public int Start
        {
            get { return m_segment.Offset; }
        }

        public int End
        {
            get {
                if (!IsParsedToEnd) throw new InvalidOperationException("is not parsed to end");
                return m_segment.Offset + m_segment.Count;
            }
        }

        public JsonValueType ValueType
        {
            get;
            private set;
        }

        static StringSegment SearchTokenEnd(StringSegment segment)
        {
            // search token end
            int i = 1;
            if (segment[0] == '"')
            {
                // string
                for (; i < segment.Count; ++i)
                {
                    if (segment[i] == '\"')
                    {
                        return segment.Take(i+1);
                    }
                    else if(segment[i] == '\\')
                    {
                        switch(segment[i+1])
                        {
                            case '"': // fall through
                            case '\\': // fall through
                            case '/': // fall through
                            case 'b': // fall through
                            case 'f': // fall through
                            case 'n': // fall through
                            case 'r': // fall through
                            case 't': // fall through
                                // skip next
                                i+=1;
                                break;

                            case 'u': // unicode
                                // skip next 4
                                i += 4;
                                break;

                            default:
                                // unkonw escape
                                throw new FormatException("unknown escape: "+segment.Skip(i));
                        }                         
                    }
                }
                throw new FormatException("no close string: " + segment.Skip(i));
            }
            else
            {
                // exclude string
                for (; i < segment.Count; ++i)
                {
                    if (Char.IsWhiteSpace(segment[i])
                        || segment[i] == '}' 
                        || segment[i] == ']'
                        )
                    {
                        break;
                    }
                }
                return segment.Take(i);
            }
        }

        Node(StringSegment segment, bool recursive)
        {
            switch (segment[0])
            {
                case '{': ValueType = JsonValueType.Object; break;
                case '[': ValueType = JsonValueType.Array; break;
                case '"': ValueType = JsonValueType.String; break;
                case 't': ValueType = JsonValueType.Boolean; break;
                case 'f': ValueType = JsonValueType.Boolean; break;
                case 'n': ValueType = JsonValueType.Unknown; break;

                case '}': // fall through
                case ']': // fall through
                    ValueType = JsonValueType.Close; break;

                case '-': // fall through
                case '0': // fall through
                case '1': // fall through
                case '2': // fall through
                case '3': // fall through
                case '4': // fall through
                case '5': // fall through
                case '6': // fall through
                case '7': // fall through
                case '8': // fall through
                case '9': // fall through
                    ValueType = JsonValueType.Number; break;

                default:
                    ValueType = JsonValueType.Unknown;
                    throw new FormatException(segment.ToString() + " is not json");
            }

            switch (ValueType)
            {
                case JsonValueType.Array: // fall through
                case JsonValueType.Object: // fall through
                    m_segment = segment;
                    IsParsedToEnd = false;
                    // parse child objects ?
                    if (recursive)
                    {
                        ParseToEnd();
                    }
                    break;

                default:
                    m_segment = SearchTokenEnd(segment);
                    IsParsedToEnd = true;
                    break;
            }
        }

        public static Node Parse(string json, bool recursive=false)
        {
            return Parse(new StringSegment(json), recursive);
        }

        public static Node Parse(StringSegment json, bool recursive)
        {
            // search non whitespace
            int pos;
            if(!json.TrySearch(x => !Char.IsWhiteSpace(x), out pos))
            {
                throw new FormatException("[" + json.ToString() + "] is only whitespace");
            }
            return new Node(json.Skip(pos), recursive);
        }

        #region PrimitiveType
        public bool IsNull
        {
            get
            {
                return m_segment.IsMatch("null");
            }
        }

        public bool GetBoolean()
        {
            if (ValueType != JsonValueType.Boolean) throw new FormatException("is not boolean: "+m_segment);
            var s = m_segment.ToString();
            switch (s)
            {
                case "true": return true;
                case "false": return false;
                default: throw new FormatException(s + " is not boolean");
            }
        }

        public double GetNumber()
        {
            if (ValueType != JsonValueType.Number) throw new FormatException("is not number: " + m_segment);
            return double.Parse(m_segment.ToString());
        }
        #endregion

        #region StringType
        public string GetString()
        {
            if (ValueType != JsonValueType.String) throw new FormatException("is not string: "+m_segment);
            return Unquote(m_segment.ToString());
        }

        public static string Quote(string src)
        {
            return '"' + src + '"';
        }

        public static string Unquote(string src)
        {
            return src.Substring(1, src.Length - 2);
        }
        #endregion

        #region CollectionType
        // for string key object
        public Node this[string target]
        {
            get
            {
                var it = GetNodes(false).GetEnumerator();
                while (it.MoveNext())
                {
                    var key = it.Current;

                    if (!it.MoveNext())
                    {
                        throw new FormatException("no value");
                    }
                    var value = it.Current;

                    if(key.GetString()==target)
                    {
                        return value;
                    }
                }
                throw new KeyNotFoundException();
            }
        }

        public IEnumerable<KeyValuePair<String, Node>> ObjectItems
        {
            get
            {
                if (ValueType != JsonValueType.Object) throw new FormatException("is not object");
                var it = GetNodes(false).GetEnumerator();
                while (it.MoveNext())
                {
                    var key = it.Current.GetString();

                    it.MoveNext();
                    yield return new KeyValuePair<string, Node>(key, it.Current);
                }
            }
        }

        public IEnumerable<Node> ArrayItems
        {
            get
            {
                if (ValueType != JsonValueType.Array) throw new FormatException("is not array");
                return GetNodes(false);
            }
        }

        IEnumerable<Node> GetNodes(bool useCloseNode)
        {
            if(ValueType!=JsonValueType.Array
                && ValueType!=JsonValueType.Object)
            {
                yield break;
            }

            var closeChar = ValueType == JsonValueType.Array ? ']' : '}';
            bool isFirst = true;
            var current = m_segment.Skip(1);
            while (true)
            {
                {
                    // skip white space
                    int nextToken;
                    if (!current.TrySearch(x => !Char.IsWhiteSpace(x), out nextToken))
                    {
                        throw new FormatException("no white space expected");
                    }
                    current = current.Skip(nextToken);
                }

                {
                    if (current[0]==closeChar)
                    {
                        // end
                        if (useCloseNode) {
                            yield return new Node(current, false);
                        }
                        break;
                    }
                }

                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    // search ',' or closeChar
                    int keyPos;
                    if (!current.TrySearch(x => x == ',', out keyPos))
                    {
                        throw new FormatException("',' expected");
                    }
                    current = current.Skip(keyPos + 1);
                }

                {
                    // skip white space
                    int nextToken;
                    if (!current.TrySearch(x => !Char.IsWhiteSpace(x), out nextToken))
                    {
                        throw new KeyNotFoundException("no key node");
                    }
                    current = current.Skip(nextToken);
                }

                // key
                var key = Parse(current, true);
                if (ValueType==JsonValueType.Object && key.ValueType != JsonValueType.String)
                {
                    throw new FormatException("no string key is not allowed: " + key.Segment);
                }
                current = current.Skip(key.Segment.Count);
                yield return key;

                if (ValueType == JsonValueType.Object)
                {
                    // search ':'
                    int valuePos;
                    if (!current.TrySearch(x => x == ':', out valuePos))
                    {
                        throw new FormatException(": is not found");
                    }
                    current = current.Skip(valuePos + 1);

                    {
                        // skip white space
                        int nextToken;
                        if (!current.TrySearch(x => !Char.IsWhiteSpace(x), out nextToken))
                        {
                            throw new KeyNotFoundException("no key node");
                        }
                        current = current.Skip(nextToken);
                    }

                    // value
                    var value = Parse(current, true);
                    current = current.Skip(value.Segment.Count);
                    yield return value;
                }
            }
        }
        #endregion
    }
}
