using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using System.IO;

namespace NMessagePack
{
    [TestFixture]
    public class FloatTest
    {
        [SetUp]
        public void Setup()
        {
            Serializer.Clear();
            Deserializer.Clear();
        }

        [Test]
        public void Float32()
        {
            var i = 1.1f;
            var float_be = new byte[]
            {
                0x3f, 0x8c, 0xcc, 0xcd
            };

            var bytes = Serializer.Serialize(i);

            var value = MsgPackValue.Parse(bytes);
            var body = value.GetBody();
            Assert.AreEqual(float_be, body.ToArray());

            Assert.AreEqual(i, value.GetValue());
        }

        [Test]
        public void Float64()
        {
            var i = 1.1;
            var double_be = new Byte[]{
                0x3f, 0xf1, 0x99, 0x99, 0x99, 0x99, 0x99, 0x9a,
            };

            var bytes = Serializer.Serialize(i);

            var value = MsgPackValue.Parse(bytes);
            var body = value.GetBody();
            Assert.AreEqual(double_be, body.ToArray());

            Assert.AreEqual(i, value.GetValue());
        }
    }
}
