using NUnit.Framework;
using System;
using System.Linq;


namespace NMessagePack
{
    [TestFixture]
    public class StringTest
    {
        [SetUp]
        public void Setup()
        {
            Serializer.Clear();
            Deserializer.Clear();
        }

        [Test]
        public void str()
        {
            var bytes = Serializer.Serialize("文字列");

            String v=Deserializer.Deserialize<String>(bytes);

            Assert.AreEqual("文字列", v);
        }

        [Test]
        public void fix_str()
        {
            for(int i=1; i<32; ++i)
            {
                var str = String.Join("", Enumerable.Range(0, i).Select(_ => "0").ToArray());
                var bytes = Serializer.Serialize(str);

                var value = MsgPackValue.Parse(bytes);

                Assert.AreEqual(i, ((String)value.GetValue()).Length);
            }
        }
    }
}
