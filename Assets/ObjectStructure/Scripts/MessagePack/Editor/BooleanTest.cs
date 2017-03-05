using NUnit.Framework;
using ObjectStructure.MessagePack;
using ObjectStructure.Serialization;
using System;
using System.IO;


[TestFixture]
public class BooleanTest
{
    TypeRegistory m_r;

    [SetUp]
    public void Setup()
    {
        m_r = new TypeRegistory();
    }

    [Test]
    public void nil()
    {
        {
            var ms = new MemoryStream();
            var w = new MsgPackWriter(ms); ;
            w.MsgPackNil();
            var bytes = ms.ToArray();
            Assert.AreEqual(new Byte[] { 0xC0 }, bytes);

            Object j = null;
            m_r.Deserialize(MsgPackValue.Parse(bytes), ref j);
            Assert.AreEqual(null, j);
        }

        {
            var bytes = m_r.SerializeToMessagePack((object)null);
            Assert.AreEqual(new Byte[] { (byte)MsgPackType.NIL }, bytes);

            Object j = null;
            m_r.Deserialize(MsgPackValue.Parse(bytes), ref j);
            Assert.AreEqual(null, j);
        }
    }

    [Test]
    public void True()
    {
        var bytes = m_r.SerializeToMessagePack(true);
        Assert.AreEqual(new Byte[] { 0xC3 }, bytes);

        var value = MsgPackValue.Parse(bytes);
        var j = value.GetValue<Boolean>();
        Assert.AreEqual(true, j);
    }

    [Test]
    public void False()
    {
        var bytes = m_r.SerializeToMessagePack(false);
        Assert.AreEqual(new Byte[] { 0xC2 }, bytes);

        var value = MsgPackValue.Parse(bytes);
        var j = value.GetValue<Boolean>();
        Assert.AreEqual(false, j);
    }
}
