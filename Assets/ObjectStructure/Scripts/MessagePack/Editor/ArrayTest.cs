
using NUnit.Framework;
using System;
using System.Linq;

namespace NMessagePack
{
    [TestFixture]
    public class ArrayTest
    {
        [SetUp]
        public void Setup(){
            Serializer.Clear();
            Deserializer.Clear();
        }

        [Test]
        public void fix_array()
        {
            var bytes = Serializer.SerializeArray(0, 1, false, (Object)null);

            Assert.AreEqual(new Byte[]{
                (Byte)MsgPackType.FIX_ARRAY_0x4,
                (Byte)MsgPackType.POSITIVE_FIXNUM,
                (Byte)MsgPackType.POSITIVE_FIXNUM_0x01,
                (Byte)MsgPackType.FALSE,
                (Byte)MsgPackType.NIL
            }, bytes);

            Object[] a=Deserializer.Deserialize<object[]>(bytes);

            Assert.AreEqual(4, a.Length);
            Assert.AreEqual(0, a[0]);
            Assert.AreEqual(1, a[1]);
            Assert.False((Boolean)a[2]);
            Assert.AreEqual(null, a[3]);
        }

        [Test]
        public void array16()
        {
            var data = Enumerable.Range(0, 20).Select(x => (Object)x).ToArray();            
            var bytes = Serializer.Serialize(data);

            var value = MsgPackValue.Parse(bytes);
            Assert.AreEqual(true, value.FormatType.IsArray());
            Assert.AreEqual(20, value.Count);
            for (int i = 0; i < 20; ++i)
            {
                Assert.AreEqual(i, value[i].GetValue());
            }
        }
    }
}
