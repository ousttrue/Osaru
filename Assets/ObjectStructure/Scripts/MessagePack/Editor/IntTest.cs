using NUnit.Framework;
using System;


namespace NMessagePack
{
    [TestFixture]
    public class IntTest 
    {
        [SetUp]
        public void Setup()
        {
            Serializer.Clear();
            Deserializer.Clear();
        }

        [Test]
        public void positive_fixnum() 
        {
            for(Byte i=0; i<128; ++i){
                var bytes = Serializer.Serialize(i);
                Assert.AreEqual(new Byte[]{ i }, bytes);

                var j=Deserializer.Deserialize<Byte>(bytes);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void negative_fixnum() 
        {
            for(SByte i=-32; i<0; ++i){
                var bytes = Serializer.Serialize(i);

                SByte j=Deserializer.Deserialize<SByte>(bytes);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void uint8() 
        {
            {
                Byte i=0x7F+20;

                var bytes = Serializer.Serialize(i);
                Assert.AreEqual(new Byte[]{
                        0xcc, 0x93,
                        }, bytes);

                Byte j=Deserializer.Deserialize<Byte>(bytes);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void cast_large_type()
        {
            {
                Byte i = 0x7F + 20;

                var bytes = Serializer.Serialize(i);
                Assert.AreEqual(new Byte[]{
                        0xcc, 0x93,
                        }, bytes);

                UInt16 j=Deserializer.Deserialize<UInt16>(bytes);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void uint16() 
        {
            {
                UInt16 i=0xFF+20;

                var bytes=Serializer.Serialize(i);
                Assert.AreEqual(new Byte[]{
                        0xcd, 0x01, 0x13
                        }, bytes);

                UInt16 j=Deserializer.Deserialize<UInt16>(bytes);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void uint32() 
        {
            {
                UInt32 i=0xFFFF+20;

                var bytes = Serializer.Serialize(i);
                Assert.AreEqual(new Byte[]{
                        0xce, 0x00, 0x01, 0x00, 0x13
                        }, bytes);

                UInt32 j=Deserializer.Deserialize<UInt32>(bytes);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void uint64() 
        {
            {
                UInt64 i=0xFFFFFFFF;
                i += 20;

                var bytes = Serializer.Serialize(i);
                Assert.AreEqual(new Byte[]{
                        0xcf, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x13
                        }, bytes);

                UInt64 j=Deserializer.Deserialize<UInt64>(bytes);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void int8()
        {
            {
                SByte i = -64;

                var bytes = Serializer.Serialize(i);

                Assert.AreEqual(new Byte[]{
                        0xd0, 0xc0,
                        }, bytes);

                SByte j=Deserializer.Deserialize<SByte>(bytes);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void int16()
        {
            {
                Int16 i = -150;

                var bytes = Serializer.Serialize(i);

                Assert.AreEqual(new Byte[]{
                        0xd1, 0xFF, 0x6a
                        }, bytes);

                Int16 j=Deserializer.Deserialize<Int16>(bytes);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void int32()
        {
            {
                Int32 i = -35000;

                var bytes = Serializer.Serialize(i);

                Assert.AreEqual(new Byte[]{
                        0xd2, 0xff, 0xff, 0x77, 0x48
                        }, bytes);

                Int32 j=Deserializer.Deserialize<Int32>(bytes);
                Assert.AreEqual(i, j);
            }
        }

        [Test]
        public void int64()
        {
            {
                Int64 i = -2147483650;

                var bytes = Serializer.Serialize(i);

                Assert.AreEqual(new Byte[]{
                        0xd3, 0xff, 0xff, 0xff, 0xff, 0x7f, 0xff, 0xff, 0xfe
                        }, bytes);

                var j=Deserializer.Deserialize<Int64>(bytes);
                Assert.AreEqual(i, j);
            }
        }
    }
}
