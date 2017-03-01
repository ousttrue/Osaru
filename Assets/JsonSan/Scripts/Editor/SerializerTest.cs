using JsonSan;
using NUnit.Framework;


public class SerializerTest
{
    public static void TypeTest<T>(T value, string expected)
    {
        var typeRegistory = new TypeRegistory();
        var json = typeRegistory.Serialize(value);
        Assert.AreEqual(expected, json);

        var deserialized = typeRegistory.Deserialize <T>(json);
        Assert.AreEqual(value, deserialized);
    }

    [Test]
    public void NumberTest()
    {
        TypeTest(1, "1");
    }
}
