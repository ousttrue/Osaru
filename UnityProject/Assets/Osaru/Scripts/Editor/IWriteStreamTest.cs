using NUnit.Framework;
using Osaru;
using System;
using System.Text;


namespace OsaruTest
{
    public class IWriteStreamTest
    {
        [Test]
        public void StringBuilderStreamTest()
        {
            var sb = new StringBuilder();
            var stream = new StringBuilderStore(sb);

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
