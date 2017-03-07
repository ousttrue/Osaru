using NUnit.Framework;
using ObjectStructure;
using System;
using System.Text;


namespace ObjectStructureTest
{
    public class IWriteStreamTest
    {
        [Test]
        public void CharArrayStreamTest()
        {

            var buffer = new char[3];
            var stream = new CharArrayStream(buffer);

            stream.Write("abc");
            Assert.AreEqual("abc".ToCharArray(), buffer);

            Assert.Catch(typeof(ArgumentOutOfRangeException), () => stream.Write("d"));
        }

        [Test]
        public void StringBuilderStreamTest()
        {
            var sb = new StringBuilder();
            var stream = new StringBuilderStream(sb);

            stream.Write("abc");
            Assert.AreEqual("abc", sb.ToString());

            stream.Write("d");
            Assert.AreEqual("abcd", sb.ToString());

            sb.Length = 0;
            stream.Write("e");
            Assert.AreEqual("e", sb.ToString());
        }
    }
}
