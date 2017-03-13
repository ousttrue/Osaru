using NUnit.Framework;
using Osaru.MessagePack;
using Osaru.Serialization;
using System;
using System.Linq;

namespace OsaruTest.MessagePack
{
    [TestFixture]
    public class StringTest
    {
        TypeRegistory m_r;

        [SetUp]
        public void Setup()
        {
            m_r = new TypeRegistory();
        }

        [Test]
        public void str()
        {
            var bytes = m_r.SerializeToMessagePack("文字列");

            var v = default(String);
            m_r.Deserialize(MessagePackParser.Parse(bytes), ref v);

            Assert.AreEqual("文字列", v);
        }

        [Test]
        public void fix_str()
        {
            for (int i = 1; i < 32; ++i)
            {
                var str = String.Join("", Enumerable.Range(0, i).Select(_ => "0").ToArray());
                var bytes = m_r.SerializeToMessagePack(str);

                var value = MessagePackParser.Parse(bytes);

                Assert.AreEqual(i, ((String)value.GetValue()).Length);
            }
        }
    }
}
