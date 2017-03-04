using NUnit.Framework;
using ObjectStructure;
using ObjectStructure.Json;
using ObjectStructure.Json.Deserializers;
using ObjectStructure.Json.Serializers;
using System.Linq;


public class JsonParserTest
{
    [Test]
    public void NullTest()
    {
        {
            var node = JsonParser.Parse("null");
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(4, node.End);
            Assert.AreEqual(JsonValueType.Unknown, node.ValueType);
            Assert.IsTrue(node.IsNull);
        }
    }

    [Test]
    public void BooleanTest()
    {
        {
            var node = JsonParser.Parse("true");
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(4, node.End);
            Assert.AreEqual(JsonValueType.Boolean, node.ValueType);
            Assert.AreEqual(true, node.GetBoolean());
            Assert.Catch(typeof(JsonValueException), () => node.GetDouble());
        }
        {
            var node = JsonParser.Parse(" false ");
            Assert.AreEqual(1, node.Start);
            Assert.AreEqual(6, node.End);
            Assert.AreEqual(JsonValueType.Boolean, node.ValueType);
            Assert.AreEqual(false, node.GetBoolean());
        }
    }

    [Test]
    public void NumberTest()
    {
        {
            var node = JsonParser.Parse("1");
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(1, node.End);
            Assert.AreEqual(JsonValueType.Number, node.ValueType);
            Assert.AreEqual(1, (int)node.GetDouble());
            Assert.Catch(typeof(JsonValueException), () => node.GetBoolean());
        }
        {
            var node = JsonParser.Parse(" 22 ");
            Assert.AreEqual(1, node.Start);
            Assert.AreEqual(3, node.End);
            Assert.AreEqual(JsonValueType.Number, node.ValueType);
            Assert.AreEqual(22, (int)node.GetDouble());
        }
        {
            var node = JsonParser.Parse(" 3.3 ");
            Assert.AreEqual(1, node.Start);
            Assert.AreEqual(4, node.End);
            Assert.AreEqual(JsonValueType.Number, node.ValueType);
            Assert.AreEqual(3, (int)node.GetDouble());
            Assert.AreEqual(3.3f, (float)node.GetDouble());
        }
        {
            var node = JsonParser.Parse(" -4.44444444444444444444 ");
            Assert.AreEqual(JsonValueType.Number, node.ValueType);
            Assert.AreEqual(-4, (int)node.GetDouble());
            Assert.AreEqual(-4.44444444444444444444, node.GetDouble());
        }
        {
            var node = JsonParser.Parse(" -5e-4 ");
            Assert.AreEqual(JsonValueType.Number, node.ValueType);
            Assert.AreEqual(0, (int)node.GetDouble());
            Assert.AreEqual(-5e-4, node.GetDouble());
        }
    }

    [Test]
    public void StringTest()
    {
        {
            var value = "hoge";
            var quoted = "\"hoge\"";
            Assert.AreEqual(quoted, StringSerializer.Quote(value));
            var node = JsonParser.Parse(quoted);
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(quoted.Length, node.End);
            Assert.AreEqual(JsonValueType.String, node.ValueType);
            Assert.AreEqual("hoge", node.GetString());
        }

        {
            var value = @"fuga
  hoge";
            var quoted = "\"fuga\\r\\n  hoge\"";
            Assert.AreEqual(quoted, StringSerializer.Quote(value));
            var node = JsonParser.Parse(quoted);
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(quoted.Length, node.End);
            Assert.AreEqual(JsonValueType.String, node.ValueType);
            Assert.AreEqual(value, node.GetString());
        }
    }

    [Test]
    public void StringEscapeTest()
    {
        {
            var value = "\"";
            var escaped = "\\\"";
            Assert.AreEqual(escaped, StringSerializer.Escape(value));
            Assert.AreEqual(value, StringDeserializer.Unescape(escaped));
        }
        {
            var value = "\\";
            var escaped = "\\\\";
            Assert.AreEqual(escaped, StringSerializer.Escape(value));
            Assert.AreEqual(value, StringDeserializer.Unescape(escaped));
        }
        {
            var value = "/";
            var escaped = "\\/";
            Assert.AreEqual(escaped, StringSerializer.Escape(value));
            Assert.AreEqual(value, StringDeserializer.Unescape(escaped));
        }
        {
            var value = "\b";
            var escaped = "\\b";
            Assert.AreEqual(escaped, StringSerializer.Escape(value));
            Assert.AreEqual(value, StringDeserializer.Unescape(escaped));
        }
        {
            var value = "\f";
            var escaped = "\\f";
            Assert.AreEqual(escaped, StringSerializer.Escape(value));
            Assert.AreEqual(value, StringDeserializer.Unescape(escaped));
        }
        {
            var value = "\n";
            var escaped = "\\n";
            Assert.AreEqual(escaped, StringSerializer.Escape(value));
            Assert.AreEqual(value, StringDeserializer.Unescape(escaped));
        }
        {
            var value = "\r";
            var escaped = "\\r";
            Assert.AreEqual(escaped, StringSerializer.Escape(value));
            Assert.AreEqual(value, StringDeserializer.Unescape(escaped));
        }
        {
            var value = "\t";
            var escaped = "\\t";
            Assert.AreEqual(escaped, StringSerializer.Escape(value));
            Assert.AreEqual(value, StringDeserializer.Unescape(escaped));
        }
    }

    [Test]
    public void ObjectTest()
    {
        {
            var json = "{}";
            var node = JsonParser.Parse(json);
            Assert.AreEqual(0, node.Start);

            Assert.Catch(() => { var result = node.End; }, "raise exception");
            node.ParseToEnd();
            Assert.AreEqual(2, node.End);

            Assert.AreEqual(JsonValueType.Object, node.ValueType);
            Assert.AreEqual(0, node.ObjectItems.Count());
        }

        {
            var json = "{\"key\":\"value\"}";
            var node = JsonParser.Parse(json, true);
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(json.Length, node.End);
            Assert.AreEqual(JsonValueType.Object, node.ValueType);

            var it = node.ObjectItems.GetEnumerator();

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual("key", it.Current.Key);
            Assert.AreEqual("value", it.Current.Value.GetString());

            Assert.IsFalse(it.MoveNext());
        }

        {
            var json = "{\"key\":\"value\"}";
            var node = JsonParser.Parse(json, true);
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(json.Length, node.End);
            Assert.AreEqual(JsonValueType.Object, node.ValueType);

            var it = node.ObjectItems.GetEnumerator();

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual("key", it.Current.Key);
            Assert.AreEqual("value", it.Current.Value.GetString());

            Assert.IsFalse(it.MoveNext());
        }
    }

    [Test]
    public void NestedObjectTest()
    {
        { 
            var json = "{\"key\":{ \"nestedKey\": \"nestedValue\" }, \"key2\": { \"nestedKey2\": \"nestedValue2\" } }";
            var node = JsonParser.Parse(json);
            Assert.AreEqual(JsonValueType.Object, node.ValueType);

            {
                var it = node.ObjectItems.GetEnumerator();

                Assert.IsTrue(it.MoveNext());
                Assert.AreEqual("key", it.Current.Key);
                Assert.AreEqual(JsonValueType.Object, it.Current.Value.ValueType);

                Assert.IsTrue(it.MoveNext());
                Assert.AreEqual("key2", it.Current.Key);
                Assert.AreEqual(JsonValueType.Object, it.Current.Value.ValueType);

                Assert.IsFalse(it.MoveNext());
            }

            var nested = node["key2"];

            {
                var it = nested.ObjectItems.GetEnumerator();

                Assert.IsTrue(it.MoveNext());
                Assert.AreEqual("nestedKey2", it.Current.Key);
                Assert.AreEqual("nestedValue2", it.Current.Value.GetString());

                Assert.IsFalse(it.MoveNext());
            }

            Assert.AreEqual("nestedValue2", node["key2"]["nestedKey2"].GetString());
        }
    }

    [Test]
    public void ArrayTest()
    {
        {
            var json = "[]";
            var node = JsonParser.Parse(json);
            Assert.AreEqual(0, node.Start);

            Assert.Catch(()=> { var result=node.End; }, "raise exception");
            node.ParseToEnd();
            Assert.AreEqual(2, node.End);

            Assert.AreEqual(JsonValueType.Array, node.ValueType);
        }

        {
            var json = "[1,2,3]";
            var node = JsonParser.Parse(json);
            Assert.AreEqual(0, node.Start);

            Assert.Catch(() => { var result = node.End; }, "raise exception");

            Assert.AreEqual(JsonValueType.Array, node.ValueType);
            Assert.AreEqual(1, node[0].GetDouble());
            Assert.AreEqual(2, node[1].GetDouble());
            Assert.AreEqual(3, node[2].GetDouble());
        }

        {
            var json = "[\"key\",1]";
            var node = JsonParser.Parse(json);
            Assert.AreEqual(0, node.Start);

            Assert.Catch(() => { var result = node.End; }, "raise exception");
            node.ParseToEnd();
            Assert.AreEqual(json.Length, node.End);

            Assert.AreEqual(JsonValueType.Array, node.ValueType);

            var it = node.ArrayItems.GetEnumerator();

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual("key", it.Current.GetString());

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(1, it.Current.GetDouble());

            Assert.IsFalse(it.MoveNext());

            Assert.AreEqual("key", node[0].GetString());
            Assert.AreEqual(1, node[1].GetDouble());
        }
    }

    [Test]
    public void ParseTest()
    {
        var json = "{";
        Assert.Catch(typeof(JsonParseException), ()=>JsonParser.Parse(json, true));
    }
}
