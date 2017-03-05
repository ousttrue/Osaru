using NUnit.Framework;
using ObjectStructure.MessagePack;
using ObjectStructure.Serialization;
using System;
using System.Linq;


[TestFixture]
public class StringTest
{
    TypeRegistory m_r;

    [SetUp]
    public void Setup()
    {
        m_r = new TypeRegistory();
        Deserializer.Clear();
    }

    [Test]
    public void str()
    {
        var bytes = m_r.SerializeToMessagePack("文字列");

        String v = Deserializer.Deserialize<String>(bytes);

        Assert.AreEqual("文字列", v);
    }

    [Test]
    public void fix_str()
    {
        for (int i = 1; i < 32; ++i)
        {
            var str = String.Join("", Enumerable.Range(0, i).Select(_ => "0").ToArray());
            var bytes = m_r.SerializeToMessagePack(str);

            var value = MsgPackValue.Parse(bytes);

            Assert.AreEqual(i, ((String)value.GetValue()).Length);
        }
    }
}
