using NUnit.Framework;
using ObjectStructure.Json;
using System.Linq;


public class JsonSanTest
{
    [Test]
    public void NullTest()
    {
        {
            var node = Node.Parse("null");
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
            var node = Node.Parse("true");
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(4, node.End);
            Assert.AreEqual(JsonValueType.Boolean, node.ValueType);
            Assert.AreEqual(true, node.GetBoolean());
            Assert.Catch(typeof(JsonValueException), () => node.GetNumber());
        }
        {
            var node = Node.Parse(" false ");
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
            var node = Node.Parse("1");
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(1, node.End);
            Assert.AreEqual(JsonValueType.Number, node.ValueType);
            Assert.AreEqual(1, (int)node.GetNumber());
            Assert.Catch(typeof(JsonValueException), () => node.GetBoolean());
        }
        {
            var node = Node.Parse(" 22 ");
            Assert.AreEqual(1, node.Start);
            Assert.AreEqual(3, node.End);
            Assert.AreEqual(JsonValueType.Number, node.ValueType);
            Assert.AreEqual(22, (int)node.GetNumber());
        }
        {
            var node = Node.Parse(" 3.3 ");
            Assert.AreEqual(1, node.Start);
            Assert.AreEqual(4, node.End);
            Assert.AreEqual(JsonValueType.Number, node.ValueType);
            Assert.AreEqual(3, (int)node.GetNumber());
            Assert.AreEqual(3.3f, (float)node.GetNumber());
        }
        {
            var node = Node.Parse(" -4.44444444444444444444 ");
            Assert.AreEqual(JsonValueType.Number, node.ValueType);
            Assert.AreEqual(-4, (int)node.GetNumber());
            Assert.AreEqual(-4.44444444444444444444, node.GetNumber());
        }
        {
            var node = Node.Parse(" -5e-4 ");
            Assert.AreEqual(JsonValueType.Number, node.ValueType);
            Assert.AreEqual(0, (int)node.GetNumber());
            Assert.AreEqual(-5e-4, node.GetNumber());
        }
    }

    [Test]
    public void StringTest()
    {
        {
            var value = "hoge";
            var quoted = "\"hoge\"";
            Assert.AreEqual(quoted, Node.Quote(value));
            var node = Node.Parse(quoted);
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(quoted.Length, node.End);
            Assert.AreEqual(JsonValueType.String, node.ValueType);
            Assert.AreEqual("hoge", node.GetString());
        }

        {
            var value = @"fuga
  hoge";
            var quoted = "\"fuga\r\n  hoge\"";
            Assert.AreEqual(quoted, Node.Quote(value));
            var node = Node.Parse(quoted);
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(quoted.Length, node.End);
            Assert.AreEqual(JsonValueType.String, node.ValueType);
            Assert.AreEqual(value, node.GetString());
        }
    }

    [Test]
    public void ObjectTest()
    {
        {
            var json = "{}";
            var node = Node.Parse(json);
            Assert.AreEqual(0, node.Start);

            Assert.Catch(() => { var result = node.End; }, "raise exception");
            node.ParseToEnd();
            Assert.AreEqual(2, node.End);

            Assert.AreEqual(JsonValueType.Object, node.ValueType);
            Assert.AreEqual(0, node.ObjectItems.Count());
        }

        {
            var json = "{\"key\":\"value\"}";
            var node = Node.Parse(json, true);
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
            var node = Node.Parse(json, true);
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
            var node = Node.Parse(json);
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
            var node = Node.Parse(json);
            Assert.AreEqual(0, node.Start);

            Assert.Catch(()=> { var result=node.End; }, "raise exception");
            node.ParseToEnd();
            Assert.AreEqual(2, node.End);

            Assert.AreEqual(JsonValueType.Array, node.ValueType);
        }

        {
            var json = "[1,2,3]";
            var node = Node.Parse(json);
            Assert.AreEqual(0, node.Start);

            Assert.Catch(() => { var result = node.End; }, "raise exception");

            Assert.AreEqual(JsonValueType.Array, node.ValueType);
            Assert.AreEqual(1, node[0].GetNumber());
            Assert.AreEqual(2, node[1].GetNumber());
            Assert.AreEqual(3, node[2].GetNumber());
        }

        {
            var json = "[\"key\",1]";
            var node = Node.Parse(json);
            Assert.AreEqual(0, node.Start);

            Assert.Catch(() => { var result = node.End; }, "raise exception");
            node.ParseToEnd();
            Assert.AreEqual(json.Length, node.End);

            Assert.AreEqual(JsonValueType.Array, node.ValueType);

            var it = node.ArrayItems.GetEnumerator();

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual("key", it.Current.GetString());

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(1, it.Current.GetNumber());

            Assert.IsFalse(it.MoveNext());

            Assert.AreEqual("key", node[0].GetString());
            Assert.AreEqual(1, node[1].GetNumber());
        }
    }

    [Test]
    public void ParseTest()
    {
        var json = "{";
        Assert.Catch(typeof(JsonParseException), ()=>Node.Parse(json, true));
    }
}
