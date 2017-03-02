
using NUnit.Framework;
using System;
using System.Linq;

namespace NMessagePack
{
    [TestFixture]
    public class RawTest
    {
        [SetUp]
        public void Setup()
        {
            Serializer.Clear();
            Deserializer.Clear();
        }

        [Test]
        public void fix_raw()
        {
            var src = new Byte[] { 0, 1, 2 };
            var bytes=Serializer.Serialize(src);

            Byte[] v=Deserializer.Deserialize<Byte[]>(bytes);
            Assert.AreEqual(src, v);
        }

        [Test]
        public void raw16()
        {
            var src = Enumerable.Range(0, 50).Select(x =>(Byte)x).ToList();
            var bytes =  Serializer.Serialize(src);

            Byte[] v=Deserializer.Deserialize<Byte[]>(bytes);
            Assert.AreEqual(src.ToArray(), v);
        }
    }
}
