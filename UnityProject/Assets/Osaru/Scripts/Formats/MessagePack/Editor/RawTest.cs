using NUnit.Framework;
using Osaru;
using Osaru.MessagePack;
using Osaru.Serialization;
using System;
using System.Linq;

namespace OsaruTest.MessagePack
{
    [TestFixture]
    public class RawTest
    {
        TypeRegistry m_r;

        [SetUp]
        public void Setup()
        {
            m_r = new TypeRegistry();
        }

        [Test]
        public void fix_raw()
        {
            var src = new Byte[] { 0, 1, 2 };
            var bytes = m_r.SerializeToMessagePack(src);

            var v = default(Byte[]);
            m_r.Deserialize(MessagePackParser.Parse(bytes), ref v);
            Assert.AreEqual(src, v);
        }

        [Test]
        public void raw16()
        {
            var src = Enumerable.Range(0, 50).Select(x => (Byte)x).ToArray();
            var bytes = m_r.SerializeToMessagePack(src);

            var v = default(Byte[]);
            m_r.Deserialize(MessagePackParser.Parse(bytes), ref v);
            Assert.AreEqual(src.ToArray(), v);
        }
    }
}
