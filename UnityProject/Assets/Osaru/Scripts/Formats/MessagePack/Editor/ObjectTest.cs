﻿using NUnit.Framework;
using Osaru;
using Osaru.MessagePack;
using Osaru.Serialization;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace OsaruTest.MessagePack
{
    [TestFixture]
    public class ObjectTest
    {
        TypeRegistry m_r;

        [Serializable, DataContract]
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
            m_r = new TypeRegistry();
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

        [Serializable, DataContract]
        struct MeshTransport
        {
            public UnityEngine.Vector3[] Vertices;
            public int[] Indices;
        }

        [Serializable, DataContract]
        struct Transformation
        {
            public UnityEngine.Vector3 Position;
            public UnityEngine.Vector3 EulerAngles;
        }

        static void GoBackTest<T>(ref T original)
        {
            var typeRegistry = new Osaru.Serialization.TypeRegistry();
            var msgPack = typeRegistry.SerializeToMessagePack(original);
            var d = typeRegistry.GetDeserializer<T>();
            var parsed = MessagePackParser.Parse(msgPack);
            var copy = default(T);
            d.Deserialize(parsed, ref copy);

            Assert.AreEqual(original, copy);
        }

        [Test]
        public void ArrayInObjectTest()
        {
            var array = new object[] { 0
                , 1
                , "AddMesh"
                , new object[] {
                    "mesh_name"
                    , new MeshTransport
                    {
                        Vertices=Enumerable.Range(0, 100).Select(x => new UnityEngine.Vector3(0, 0, x)).ToArray(),
                        Indices=Enumerable.Range(0, 100).ToArray(),
                    }
                    , new Transformation
                    {
                        Position=new UnityEngine.Vector3(1, 0, 0),
                        EulerAngles=new UnityEngine.Vector3(1, 2, 3),
                    }
                }
            };

            {
                var val = (string)((object[])array[3])[0];
                GoBackTest(ref val);
            }
            {
                var val = (MeshTransport)((object[])array[3])[1];
                //GoBackTest(ref val);
            }
            {
                var val = (Transformation)((object[])array[3])[2];
                GoBackTest(ref val);
            }
        }
    }
}
