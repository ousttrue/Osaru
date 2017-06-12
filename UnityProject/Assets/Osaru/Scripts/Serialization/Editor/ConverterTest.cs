using NUnit.Framework;
using Osaru;
using Osaru.Json;
using Osaru.MessagePack;
using Osaru.Serialization;
using Osaru.Serialization.Deserializers;


namespace OsaruTest
{
    public class ConverterTest
    {
        static void ConvertJsonToMessagePackTest<T>(T value)
        {
            var r = new TypeRegistry();
            var converted = r.SerializeToJsonBytes(value)
                .ParseAsJson()
                .ToMessagePack();
            var cc = r.GetDeserializer<T>().Deserialize(converted.ParseAsMessagePack());

            var formated= r.SerializeToMessagePack(value);
            var ff = r.GetDeserializer<T>().Deserialize(formated.ParseAsMessagePack());

            Assert.AreEqual(ff, cc);
        }

        static void ConvertMessagePackToJsonTest<T>(T value)
        {
            var r = new TypeRegistry();
            var converted = r.SerializeToMessagePack(value)
                .ParseAsMessagePack()
                .ToJson();
            var cc = r.GetDeserializer<T>().Deserialize(converted.ParseAsJson());

            var formated= r.SerializeToJsonBytes(value);
            var ff = r.GetDeserializer<T>().Deserialize(formated.ParseAsJson());

            Assert.AreEqual(ff, cc);
        }

        [Test]
        public void JsonToMessagePackTest()
        {
            //ConvertJsonToMessagePackTest(1); // double int missmatch
            ConvertJsonToMessagePackTest("abc");
            ConvertJsonToMessagePackTest(true);
            //ConvertJsonToMessagePackTest((object)null);
            ConvertJsonToMessagePackTest(new UnityEngine.Vector3(1, 2, 3));
        }

        [Test]
        public void MessagePackToJsonTest()
        {
            ConvertMessagePackToJsonTest(1);
            ConvertMessagePackToJsonTest("abc");
            ConvertMessagePackToJsonTest(true);
            //ConvertMessagePackToJsonTest((object)null);
            ConvertMessagePackToJsonTest(new UnityEngine.Vector3(1, 2, 3));
        }
    }
}
