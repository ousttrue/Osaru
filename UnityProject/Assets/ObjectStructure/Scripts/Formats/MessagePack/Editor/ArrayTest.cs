using NUnit.Framework;
using ObjectStructure.MessagePack;
using ObjectStructure.Serialization;
using System;
using System.Linq;

namespace ObjectStructureTest.MessagePack
{
    [TestFixture]
    public class ArrayTest
    {
        TypeRegistory m_r;

        [SetUp]
        public void Setup()
        {
            m_r = new TypeRegistory();
        }

        [Test]
        public void fix_array()
        {
            var data = new[] { 0, 1, false, (Object)null };
            var bytes = m_r.SerializeToMessagePack(data);

            Assert.AreEqual(new Byte[]{
                (Byte)MsgPackType.FIX_ARRAY_0x4,
                (Byte)MsgPackType.POSITIVE_FIXNUM,
                (Byte)MsgPackType.POSITIVE_FIXNUM_0x01,
                (Byte)MsgPackType.FALSE,
                (Byte)MsgPackType.NIL
            }, bytes.ToEnumerable());

            var parsed = MessagePackParser.Parse(bytes);

            Assert.AreEqual(4, parsed.Count);
            Assert.AreEqual(0, parsed[0].GetValue());
            Assert.AreEqual(1, parsed[1].GetValue());
            Assert.False((Boolean)parsed[2].GetValue());
            Assert.AreEqual(null, parsed[3].GetValue());
        }

        [Test]
        public void array16()
        {
            var data = Enumerable.Range(0, 20).Select(x => (Object)x).ToArray();
            var bytes = m_r.SerializeToMessagePack(data);

            var value = MessagePackParser.Parse(bytes);
            Assert.AreEqual(true, value.FormatType.IsArray());
            Assert.AreEqual(20, value.Count);
            for (int i = 0; i < 20; ++i)
            {
                Assert.AreEqual(i, value[i].GetValue());
            }
        }
    }
}
