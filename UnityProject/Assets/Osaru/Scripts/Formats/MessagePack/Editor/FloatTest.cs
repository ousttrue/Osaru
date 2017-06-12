using NUnit.Framework;
using Osaru;
using Osaru.MessagePack;
using Osaru.Serialization;
using System;
using System.Linq;


namespace OsaruTest.MessagePack
{
    [TestFixture]
    public class FloatTest
    {
        TypeRegistry m_r;

        [SetUp]
        public void Setup()
        {
            m_r = new TypeRegistry();
        }

        [Test]
        public void Float32()
        {
            var i = 1.1f;
            var float_be = new byte[]
            {
                0x3f, 0x8c, 0xcc, 0xcd
            };

            var bytes = m_r.SerializeToMessagePack(i);

            var value = MessagePackParser.Parse(bytes);
            var body = value.GetBody();
            Assert.AreEqual(float_be, body.ToEnumerable().ToArray());

            Assert.AreEqual(i, value.GetValue());
        }

        [Test]
        public void Float64()
        {
            var i = 1.1;
            var double_be = new Byte[]{
                0x3f, 0xf1, 0x99, 0x99, 0x99, 0x99, 0x99, 0x9a,
            };

            var bytes = m_r.SerializeToMessagePack(i);

            var value = MessagePackParser.Parse(bytes);
            var body = value.GetBody();
            Assert.AreEqual(double_be, body.ToEnumerable().ToArray());

            Assert.AreEqual(i, value.GetValue());
        }
    }
}
