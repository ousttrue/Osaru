
using NUnit.Framework;
using ObjectStructure.MessagePack;
using ObjectStructure.Serialization;

namespace ObjectStructureTest.MessagePack
{
    [TestFixture]
    public class ObjectTest
    {
        TypeRegistory m_r;
        class Nest
        {
            public string Name { get; set; }
        }
        class Sample
        {
            public string Name { get; set; }
            public int Number;
            public Nest Nest;
        }
        [SetUp]
        public void Setup()
        {
            m_r = new TypeRegistory();
        }

        [Test]
        public void map_root()
        {
            var src = new Sample
            {
                Name = "Hoge",

                Number = 4
                ,
                Nest = new Nest
                {
                    Name = "Nested"
                }
            };
            var bytes = m_r.SerializeToMessagePack(src);

            var value = MessagePackParser.Parse(bytes);
            Assert.AreEqual(true, value.FormatType.IsMap());
            Assert.AreEqual(src.Name, value["Name"].GetValue());
            Assert.AreEqual(src.Number, value["Number"].GetValue());
            Assert.AreEqual(src.Nest.Name, value["Nest"]["Name"].GetValue());
        }

        [Test]
        public void array_root()
        {
            var src = new[]
            {
                new Sample{
                Name = "Hoge"
                ,
                Number = 4
                ,
                Nest = new Nest
                {
                    Name = "Nested"
                }
                }
            };
            var bytes = m_r.SerializeToMessagePack(src);

            var value = MessagePackParser.Parse(bytes);

            Assert.AreEqual(src[0].Name, value[0]["Name"].GetValue());
            Assert.AreEqual(src[0].Number, value[0]["Number"].GetValue());
            Assert.AreEqual(src[0].Nest.Name, value[0]["Nest"]["Name"].GetValue());
        }
    }
}
