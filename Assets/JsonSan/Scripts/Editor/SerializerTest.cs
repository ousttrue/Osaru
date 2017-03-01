using JsonSan;
using NUnit.Framework;
using System;
using System.Collections.Generic;


public static class TypeRegistoryExtensions
{
    public static void TypeTest<T>(this TypeRegistory typeRegistory
        , T value, string expected)
    {
        var serializer = typeRegistory.GetSerializer<T>();
        var serialized = serializer.Serialize(value, typeRegistory);
        Assert.AreEqual(expected, serialized);

        var deserializer = typeRegistory.GetDeserializer<T>();
        var deserialized = default(T);
        try
        {
            deserialized = Activator.CreateInstance<T>();
        }
        catch(Exception)
        {

        }
        var json = Node.Parse(serialized);
        deserializer.Deserialize(json, ref deserialized, typeRegistory);

        Assert.AreEqual(value, deserialized);
    }
}

public class SerializerTest
{
    [Test]
    public void NumberTest()
    {
        var typeRegistory = new TypeRegistory();
        typeRegistory.TypeTest(1, "1");
    }

    [Test]
    public void ArrayTest()
    {
        var typeRegistory = new TypeRegistory();
        var array = new[] { 1, 2, 3 };
        typeRegistory.TypeTest(array, "[1,2,3]");
    }

    [Test]
    public void ListTest()
    {
        var typeRegistory = new TypeRegistory();
        var array = new List<int> { 1, 2, 3 };
        typeRegistory.TypeTest(array, "[1,2,3]");
    }
}
