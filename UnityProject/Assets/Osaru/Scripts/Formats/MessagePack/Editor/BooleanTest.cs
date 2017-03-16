using NUnit.Framework;
using Osaru.MessagePack;
using Osaru.Serialization;
using System;
using System.IO;

namespace OsaruTest.MessagePack
{
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
                var w = new MessagePackFormatter (); ;
                w.Null();
                var bytes = w.GetStore().Bytes;
                Assert.AreEqual(new Byte[] { 0xC0 }, bytes.ToEnumerable());

                var parsed=MessagePackParser.Parse(bytes);
                Assert.True(parsed.IsNull);
            }

            {
                var bytes = m_r.SerializeToMessagePack((object)null);
                Assert.AreEqual(new Byte[] { (byte)MsgPackType.NIL }, bytes.ToEnumerable());

                var parsed = MessagePackParser.Parse(bytes);
                Assert.True(parsed.IsNull);
            }
        }

        [Test]
        public void True()
        {
            var bytes = m_r.SerializeToMessagePack(true);
            Assert.AreEqual(new Byte[] { 0xC3 }, bytes.ToEnumerable());

            var value = MessagePackParser.Parse(bytes);
            var j = value.GetValue<Boolean>();
            Assert.AreEqual(true, j);
        }

        [Test]
        public void False()
        {
            var bytes = m_r.SerializeToMessagePack(false);
            Assert.AreEqual(new Byte[] { 0xC2 }, bytes.ToEnumerable());

            var value = MessagePackParser.Parse(bytes);
            var j = value.GetValue<Boolean>();
            Assert.AreEqual(false, j);
        }
    }
}
