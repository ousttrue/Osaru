using NUnit.Framework;
using Osaru.MessagePack;
using Osaru.Serialization;
using Osaru;
using System;
using System.IO;
using System.Linq;

namespace OsaruTest.MessagePack
{
    [TestFixture]
    public class MapTest
    {
        TypeRegistry m_r;

        [SetUp]
        public void Setup()
        {
            m_r = new TypeRegistry();
        }

        [Test]
        public void fix_map()
        {
            var bytes = m_r.SerializeToMessagePackMap(
                0, 1,
                2, 3);

            Assert.AreEqual(new Byte[]{
                0x82, 0x00, 0x01, 0x02, 0x03
            }, bytes.ToEnumerable());

            var value = MessagePackParser.Parse(bytes);

            Assert.AreEqual(2, value.Count);
            Assert.AreEqual(1, value.GetValueByIntKey(0).GetValue());
            Assert.AreEqual(3, value.GetValueByIntKey(2).GetValue());
        }

        [Test]
        public void map16()
        {
            var w = new MessagePackFormatter (); ;
            int size = 18;
            w.BeginMap(size);
            for (int i = 0; i < size; ++i)
            {
                w.Value(i);
                w.Value(i + 5);
            }
            var bytes = w.GetStore().Bytes.ToEnumerable().ToArray();

            Assert.AreEqual(
                new Byte[]{0xde, 0x0, 0x12, 0x0, 0x5, 0x1, 0x6, 0x2, 0x7, 0x3, 0x8, 0x4, 0x9, 0x5, 0xa, 0x6, 0xb, 0x7, 0xc, 0x8, 0xd, 0x9, 0xe, 0xa, 0xf, 0xb, 0x10, 0xc,
0x11, 0xd, 0x12, 0xe, 0x13, 0xf, 0x14, 0x10, 0x15, 0x11, 0x16},
            bytes);


            var value = MessagePackParser.Parse(bytes);

            Assert.AreEqual(15, value.GetValueByIntKey(10).GetInt32());
        }
    }
}
