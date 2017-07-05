using NUnit.Framework;
using System;
using Osaru;
using Osaru.Serialization;
using System.Runtime.Serialization;


#if UNITY_EDITOR
using UnityEngine;
#else
[Serializable]
struct Vector3
{
    public Single X;
    public Single Y;
    public Single Z;

    public override bool Equals(object obj)
    {
        if (obj is Vector3)
        {
            var s = (Vector3)obj;
            return s.X == X && s.Y == Y && s.Z == Z;
        }
        return base.Equals(obj);
    }

    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}]", X, Y, Z);
    }
}
#endif


[Serializable]
class UserType
{
    public String Name
    {
        get;
        set;
    }

    public Vector3 Position
    {
        get;
        set;
    }

#if USE_FORM
        public System.Drawing.Color Color
        {
            get;
            set;
        }
#endif
}

[TestFixture]
public class UserTypeTest
{
    TypeRegistry m_r;

    [SetUp]
    public void Setup()
    {
        m_r = new TypeRegistry();
    }

    [Test]
    public void pack_and_unpack()
    {
        var obj = new UserType
        {
            Name = "hoge"
            ,
            Position = new Vector3 { x = 1, y = 2, z = 3 }
#if USE_FORM
                , Color = System.Drawing.Color.FromArgb(255, 128, 128, 255)
#endif
        };

#if USE_FORM
            // register pack System.Drawing.Color
            MsgPackUtil.Packer.Factory.GetInstance().AddType(typeof(System.Drawing.Color), (Packer p, Object o) =>
            {
                var color=(System.Drawing.Color)o;
                p.Pack_Array(4);
                p.Pack(color.A);
                p.Pack(color.R);
                p.Pack(color.G);
                p.Pack(color.B);
            });
            MsgPackUtil.Unpacker.Factory.GetInstance().AddArrayType(typeof(System.Drawing.Color), (Unpacker u, out Object o, UInt32 size) =>
                {
                    // check map size
                    if (size != 4)
                    {
                        throw new ArgumentException("invalid map size");
                    }
                    Byte a;
                    u.PipelineUnpack(out a);
                    Byte r;
                    u.PipelineUnpack(out r);
                    Byte g;
                    u.PipelineUnpack(out g);
                    Byte b;
                    u.PipelineUnpack(out b);
                    o = System.Drawing.Color.FromArgb(a, r, g, b);
                });
#endif

        // pack
        var bytes = m_r.SerializeToMessagePack(obj);

        // unpack
        var newObj = default(UserType);
        m_r.Deserialize(bytes.ParseAsMessagePack(), ref newObj);

        Assert.AreEqual(obj.Name, newObj.Name);
#if USE_FORM
            Assert.AreEqual(obj.Color, newObj.Color);
#endif
        Assert.AreEqual(obj.Position, newObj.Position);
    }

    [DataContract]
    class KeyName
    {
        [DataMember(Name ="key")]
        public String Name;
    }

    [Test]
    public void key_rename()
    {
        var value = new KeyName
        {
            Name = "hoge"
        };
        var bytes=m_r.SerializeToMessagePack(value);
        var parsed = bytes.ParseAsMessagePack();

        Assert.AreEqual(parsed["key"].GetString(), "hoge");
    }
}
