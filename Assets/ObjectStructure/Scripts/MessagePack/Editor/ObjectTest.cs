
using NUnit.Framework;
using ObjectStructure.MessagePack;
using ObjectStructure.Serialization;


[TestFixture]
public class ObjectTest
{
    TypeRegistory m_r;

    [SetUp]
    public void Setup()
    {
        m_r = new TypeRegistory();
        Deserializer.Clear();
    }

    /*
    [Test]
    public void obj()
    {
        var bytes = Serializer.Serialize((object)1);

        int j=MsgPack.Unpack<int>(bytes);

        Assert.AreEqual(1, j);
    }
    */

    [Test]
    public void map_root()
    {
        var src = new
        {
            Name = "Hoge",

            Number = 4
            ,
            Nest = new
            {
                Name = "Nested"
            }
        };
        var bytes = m_r.SerializeToMessagePack(src);

        var value = MsgPackValue.Parse(bytes);
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
                new {
                Name = "Hoge"
                ,
                Number = 4
                ,
                Nest = new
                {
                    Name = "Nested"
                }
                }
            };
        var bytes = m_r.SerializeToMessagePack(src);

        var value = MsgPackValue.Parse(bytes);

        Assert.AreEqual(src[0].Name, value[0]["Name"].GetValue());
        Assert.AreEqual(src[0].Number, value[0]["Number"].GetValue());
        Assert.AreEqual(src[0].Nest.Name, value[0]["Nest"]["Name"].GetValue());
    }
}
