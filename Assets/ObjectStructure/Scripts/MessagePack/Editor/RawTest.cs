
using NUnit.Framework;
using ObjectStructure.MessagePack;
using ObjectStructure.Serialization;
using System;
using System.Linq;


[TestFixture]
public class RawTest
{
    TypeRegistory m_r;

    [SetUp]
    public void Setup()
    {
        m_r = new TypeRegistory();
    }

    [Test]
    public void fix_raw()
    {
        var src = new Byte[] { 0, 1, 2 };
        var bytes = m_r.SerializeToMessagePack(src);

        var v = default(Byte[]);
        m_r.Deserialize(MsgPackValue.Parse(bytes), ref v);
        Assert.AreEqual(src, v);
    }

    [Test]
    public void raw16()
    {
        var src = Enumerable.Range(0, 50).Select(x => (Byte)x).ToArray();
        var bytes = m_r.SerializeToMessagePack(src);

        var v = default(Byte[]);
        m_r.Deserialize(MsgPackValue.Parse(bytes), ref v);
        Assert.AreEqual(src.ToArray(), v);
    }
}
