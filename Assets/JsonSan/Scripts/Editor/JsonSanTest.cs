using NUnit.Framework;
using JsonSan;
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
            Assert.AreEqual(ValueType.Unknown, node.ValueType);
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
            Assert.AreEqual(ValueType.Boolean, node.ValueType);
            Assert.AreEqual(true, node.GetBoolean());
            Assert.Catch(typeof(System.FormatException), () => node.GetNumber());
        }
        {
            var node = Node.Parse(" false ");
            Assert.AreEqual(1, node.Start);
            Assert.AreEqual(6, node.End);
            Assert.AreEqual(ValueType.Boolean, node.ValueType);
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
            Assert.AreEqual(ValueType.Number, node.ValueType);
            Assert.AreEqual(1, (int)node.GetNumber());
            Assert.Catch(typeof(System.FormatException), () => node.GetBoolean());
        }
        {
            var node = Node.Parse(" 22 ");
            Assert.AreEqual(1, node.Start);
            Assert.AreEqual(3, node.End);
            Assert.AreEqual(ValueType.Number, node.ValueType);
            Assert.AreEqual(22, (int)node.GetNumber());
        }
        {
            var node = Node.Parse(" 3.3 ");
            Assert.AreEqual(1, node.Start);
            Assert.AreEqual(4, node.End);
            Assert.AreEqual(ValueType.Number, node.ValueType);
            Assert.AreEqual(3, (int)node.GetNumber());
            Assert.AreEqual(3.3f, (float)node.GetNumber());
        }
        {
            var node = Node.Parse(" -4.44444444444444444444 ");
            Assert.AreEqual(ValueType.Number, node.ValueType);
            Assert.AreEqual(-4, (int)node.GetNumber());
            Assert.AreEqual(-4.44444444444444444444, node.GetNumber());
        }
        {
            var node = Node.Parse(" -5e-4 ");
            Assert.AreEqual(ValueType.Number, node.ValueType);
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
            Assert.AreEqual(ValueType.String, node.ValueType);
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
            Assert.AreEqual(ValueType.String, node.ValueType);
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
            Assert.AreEqual(2, node.End);
            Assert.AreEqual(ValueType.Object, node.ValueType);
            Assert.AreEqual(0, node.Count());
        }

        {
            var json = "{\"key\":\"value\"}";
            var node = Node.Parse(json);
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(json.Length, node.End);
            Assert.AreEqual(ValueType.Object, node.ValueType);

            var it = node.GetEnumerator();

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual("key", it.Current.GetString());

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual("value", it.Current.GetString());

            Assert.IsFalse(it.MoveNext());
            Assert.AreEqual(2, node.Count());
        }

        {
            var json = "{\"key\":\"value\"}";
            var node = Node.Parse(json);
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(json.Length, node.End);
            Assert.AreEqual(ValueType.Object, node.ValueType);

            var it = node.GetEnumerator();

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual("key", it.Current.GetString());

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual("value", it.Current.GetString());

            Assert.IsFalse(it.MoveNext());
            Assert.AreEqual(2, node.Count());
        }
    }

    [Test]
    public void NestedObjectTest()
    {
        { 
            var json = "{\"key\":{ \"nestedKey\": \"nestedValue\" } }";
            var node = Node.Parse(json);
            Assert.AreEqual(ValueType.Object, node.ValueType);

            var nested = node["key"];

            var it = nested.GetEnumerator();

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual("nestedKey", it.Current.GetString());

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual("nestedValue", it.Current.GetString());

            Assert.IsFalse(it.MoveNext());
            Assert.AreEqual(2, node.Count());

            Assert.AreEqual("nestedValue", node["key"]["nestedKey"].GetString());
        }
    }

    [Test]
    public void ArrayTest()
    {
        {
            var json = "[]";
            var node = Node.Parse(json);
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(2, node.End);
            Assert.AreEqual(ValueType.Array, node.ValueType);
            Assert.AreEqual(0, node.Count());
        }

        {
            var json = "[\"key\",1]";
            var node = Node.Parse(json);
            Assert.AreEqual(0, node.Start);
            Assert.AreEqual(json.Length, node.End);
            Assert.AreEqual(ValueType.Array, node.ValueType);

            var it = node.GetEnumerator();

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual("key", it.Current.GetString());

            Assert.IsTrue(it.MoveNext());
            Assert.AreEqual(1, it.Current.GetNumber());

            Assert.IsFalse(it.MoveNext());
            Assert.AreEqual(2, node.Count());
        }
    }
}
