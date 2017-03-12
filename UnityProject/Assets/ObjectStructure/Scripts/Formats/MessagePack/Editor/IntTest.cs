using NUnit.Framework;
using ObjectStructure.MessagePack;
using ObjectStructure.Serialization;
using System;
using System.Linq;


namespace ObjectStructureTest.MessagePack
{
    [TestFixture]
    public class IntTest
    {
        TypeRegistory m_r;

        [SetUp]
        public void Setup()
        {
            m_r = new TypeRegistory();
        }

        [Test]
        public void positive_fixnum()
        {
            for (Byte i = 0; i < 128; ++i)
            {
                var bytes = m_r.SerializeToMessagePack(i);
                Assert.AreEqual(new Byte[] { i }, bytes.ToEnumerable());

                var j = default(Byte);
                m_r.Deserialize(MessagePackParser.Parse(bytes), ref j);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void negative_fixnum()
        {
            for (SByte i = -32; i < 0; ++i)
            {
                var bytes = m_r.SerializeToMessagePack(i);

                var j = default(SByte);
                m_r.Deserialize(MessagePackParser.Parse(bytes), ref j);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void uint8()
        {
            {
                Byte i = 0x7F + 20;

                var bytes = m_r.SerializeToMessagePack(i);
                Assert.AreEqual(new Byte[]{
                        0xcc, 0x93,
                        }, bytes.ToEnumerable());

                var j = default(Byte);
                m_r.Deserialize(MessagePackParser.Parse(bytes), ref j);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void cast_large_type()
        {
            {
                Byte i = 0x7F + 20;

                var bytes = m_r.SerializeToMessagePack(i);
                Assert.AreEqual(new Byte[]{
                        0xcc, 0x93,
                        }, bytes.ToEnumerable());

                var j = default(UInt16);
                m_r.Deserialize(MessagePackParser.Parse(bytes), ref j);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void uint16()
        {
            {
                UInt16 i = 0xFF + 20;

                var bytes = m_r.SerializeToMessagePack(i);
                Assert.AreEqual(new Byte[]{
                        0xcd, 0x01, 0x13
                        }, bytes.ToEnumerable());

                var j = default(UInt16);
                m_r.Deserialize(MessagePackParser.Parse(bytes), ref j);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void uint32()
        {
            {
                UInt32 i = 0xFFFF + 20;

                var bytes = m_r.SerializeToMessagePack(i);
                Assert.AreEqual(new Byte[]{
                        0xce, 0x00, 0x01, 0x00, 0x13
                        }, bytes.ToEnumerable());

                var j = default(UInt32);
                m_r.Deserialize(MessagePackParser.Parse(bytes), ref j);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void uint64()
        {
            {
                UInt64 i = 0xFFFFFFFF;
                i += 20;

                var bytes = m_r.SerializeToMessagePack(i);
                Assert.AreEqual(new Byte[]{
                        0xcf, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x13
                        }, bytes.ToEnumerable());

                var j = default(UInt64);
                m_r.Deserialize(MessagePackParser.Parse(bytes), ref j);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void int8()
        {
            {
                SByte i = -64;

                var bytes = m_r.SerializeToMessagePack(i);

                Assert.AreEqual(new Byte[]{
                        0xd0, 0xc0,
                        }, bytes.ToEnumerable());

                var j = default(SByte);
                m_r.Deserialize(MessagePackParser.Parse(bytes), ref j);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void int16()
        {
            {
                Int16 i = -150;

                var bytes = m_r.SerializeToMessagePack(i);

                Assert.AreEqual(new Byte[]{
                        0xd1, 0xFF, 0x6a
                        }, bytes.ToEnumerable());

                var j = default(Int16);
                m_r.Deserialize(MessagePackParser.Parse(bytes), ref j);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void int32()
        {
            {
                Int32 i = -35000;

                var bytes = m_r.SerializeToMessagePack(i);

                Assert.AreEqual(new Byte[]{
                        0xd2, 0xff, 0xff, 0x77, 0x48
                        }, bytes.ToEnumerable());

                var j = default(Int32);
                m_r.Deserialize(MessagePackParser.Parse(bytes), ref j);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void int64()
        {
            {
                Int64 i = -2147483650;

                var bytes = m_r.SerializeToMessagePack(i);

                Assert.AreEqual(new Byte[]{
                        0xd3, 0xff, 0xff, 0xff, 0xff, 0x7f, 0xff, 0xff, 0xfe
                        }, bytes.ToEnumerable());

                var j = default(Int64);
                m_r.Deserialize(MessagePackParser.Parse(bytes), ref j);
                Assert.AreEqual(i, j);
            }
        }
    }
}
