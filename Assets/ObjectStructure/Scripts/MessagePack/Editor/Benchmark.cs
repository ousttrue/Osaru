using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Text;

/// <summary>
/// from https://github.com/neuecc/ZeroFormatter/blob/master/sandbox/PerformanceComparison/Program.cs
/// </summary>
public class Benchmark
{
    string HtmlPath= UnityEngine.Application.dataPath + "/ObjectStructure/Scripts/MessagePack/Editor/CSharpHtml.txt";

    const int Iteration = 10;
    static bool dryRun = true;

    [Serializable]
    public class Person : IEquatable<Person>
    {
        public virtual int Age { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual Sex Sex { get; set; }

        public bool Equals(Person other)
        {
            if (other == null)
            {
                int a = 0;
            }
            return Age == other.Age
                && FirstName == other.FirstName
                && LastName == other.LastName
                && Sex == other.Sex;
        }
    }

    public enum Sex : sbyte
    {
        Unknown, Male, Female,
    }

    static void Validate(string label, Person original, IList<Person> originalList, Person copy, IList<Person> copyList)
    {
        if (!EqualityComparer<Person>.Default.Equals(original, copy)) Console.WriteLine(label + " Invalid Deserialize Small Object");
        if (!originalList.SequenceEqual(copyList)) Console.WriteLine(label + " Invalid Deserialize Large Array");
    }

    static void Validate2<T>(string label, T original, T copy)
    {
        if (!EqualityComparer<T>.Default.Equals(original, copy)) Console.WriteLine(label + " Invalid Deserialize");
    }

    static void Validate2<T>(string label, IList<T> original, IList<T> copy)
    {
        if (!original.SequenceEqual(copy)) Console.WriteLine(label + " Invalid Deserialize");
    }

    struct Measure : IDisposable
    {
        string label;
        Stopwatch s;

        public Measure(string label)
        {
            this.label = label;
            System.GC.Collect(2, GCCollectionMode.Forced/*, blocking: true*/);
            this.s = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            s.Stop();
            //if (!dryRun)
            {
                //Console.WriteLine($"{ label,15}   {s.Elapsed.TotalMilliseconds} ms");
            }

            System.GC.Collect(2, GCCollectionMode.Forced/*, blocking: true*/);
        }
    }

    static T SerializeMsgPack<T>(T original)
    {
        T copy = default(T);
        byte[] bytes = null;

        // Note:We should check MessagePackSerializer.Get<T>() on every iteration
        // But currenly MsgPack-Cli has bug of get serializer
        // https://github.com/msgpack/msgpack-cli/issues/191
        // so, get serializer at first.
        // and If enum serialization options to ByUnderlyingValue, gets more fast but we check default option only.

        //var serializer = MessagePackSerializer.Get<T>();

        using (new Measure("Serialize"))
        {
            for (int i = 0; i < Iteration; i++)
            {
                bytes = NMessagePack.Serializer.Serialize(original);
            }
        }

        using (new Measure("Deserialize"))
        {
            for (int i = 0; i < Iteration; i++)
            {
                copy=NMessagePack.Deserializer.Deserialize<T>(bytes);
            }
        }

        Assert.AreEqual(original, copy);

        using (new Measure("ReSerialize"))
        {
            for (int i = 0; i < Iteration; i++)
            {
                bytes = NMessagePack.Serializer.Serialize(copy);
            }
        }

        if (!dryRun)
        {
            //Console.WriteLine(string.Format("{0,15}   {1}", "Binary Size", ToHumanReadableSize(bytes.Length)));
        }

        return copy;
    }

    public struct Vector3
    {
        public float x;

        public float y;

        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    [SetUp]
    public void Setup()
    {
        NMessagePack.Serializer.Clear();
        NMessagePack.Deserializer.Clear();
        NMessagePack.Experiment.Register();
    }

    [Test]
    public void MessagePackBenchmarkTest()
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        var p = new Person
        {
            Age = 99999,
            FirstName = "Windows",
            LastName = "Server",
            Sex = Sex.Male,
        };
        IList<Person> l = Enumerable.Range(1000, 1000).Select(x => new Person { Age = x, FirstName = "Windows", LastName = "Server", Sex = Sex.Female }).ToArray();

        var integer = 1;
        var v3 = new Vector3 { x = 12345.12345f, y = 3994.35226f, z = 325125.52426f };
        IList<Vector3> v3List = Enumerable.Range(1, 100).Select(_ => new Vector3 { x = 12345.12345f, y = 3994.35226f, z = 325125.52426f }).ToArray();
        var largeString = File.ReadAllText(HtmlPath);

        Console.WriteLine("Warming-up"); Console.WriteLine();
        SerializeMsgPack(p);
        SerializeMsgPack(l);
        SerializeMsgPack(integer); SerializeMsgPack(v3); SerializeMsgPack(largeString); SerializeMsgPack(v3List);

        dryRun = false;

        Console.WriteLine();
        Console.WriteLine("Small Object(int,string,string,enum) {0} Iteration", Iteration); Console.WriteLine();

        var c = SerializeMsgPack(p); Console.WriteLine();

        Console.WriteLine("Large Array(SmallObject[1000]) {0} Iteration", Iteration); Console.WriteLine();

        var C = SerializeMsgPack(l); Console.WriteLine();

        Validate("NMessagePack", p, l, c, C);

        Console.WriteLine();
        Console.WriteLine("Additional Benchmarks"); Console.WriteLine();

        Console.WriteLine("Int32(1) {0} Iteration", Iteration); Console.WriteLine();

        var W2 = SerializeMsgPack(integer); Console.WriteLine();

        Console.WriteLine("Vector3(float, float, float) {0} Iteration", Iteration); Console.WriteLine();

        var X2 = SerializeMsgPack(v3); Console.WriteLine();

        Console.WriteLine("HtmlString({0}bytes) {1} Iteration"
            , Encoding.UTF8.GetByteCount(largeString)
            , Iteration); Console.WriteLine();

        var Y2 = SerializeMsgPack(largeString); Console.WriteLine();

        Console.WriteLine("Vector3[100] {0} Iteration", Iteration); Console.WriteLine();

        var Z2 = SerializeMsgPack(v3List); Console.WriteLine();

        Validate2("MsgPack-Cli", W2, integer);
        Validate2("MsgPack-Cli", X2, v3);
        Validate2("MsgPack-Cli", Y2, largeString);
        Validate2("MsgPack-Cli", Z2, v3List);

        //ストップウォッチを止める
        sw.Stop();

        //結果を表示する
        UnityEngine.Debug.LogFormat("[Benchmark]{0}", sw.Elapsed);
    }

    [Test]
    public void JsonBenchmarkTest()
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        var p = new Person
        {
            Age = 99999,
            FirstName = "Windows",
            LastName = "Server",
            Sex = Sex.Male,
        };
        IList<Person> l = Enumerable.Range(1000, 1000).Select(x => new Person { Age = x, FirstName = "Windows", LastName = "Server", Sex = Sex.Female }).ToArray();

        var integer = 1;
        var v3 = new Vector3 { x = 12345.12345f, y = 3994.35226f, z = 325125.52426f };
        IList<Vector3> v3List = Enumerable.Range(1, 100).Select(_ => new Vector3 { x = 12345.12345f, y = 3994.35226f, z = 325125.52426f }).ToArray();
        var largeString = File.ReadAllText(HtmlPath);

        Console.WriteLine("Warming-up"); Console.WriteLine();
        SerializeJson(p);
        SerializeJson(l);
        SerializeJson(integer); SerializeJson(v3); SerializeJson(largeString); SerializeJson(v3List);

        dryRun = false;

        Console.WriteLine();
        Console.WriteLine("Small Object(int,string,string,enum) {0} Iteration", Iteration); Console.WriteLine();

        var c = SerializeJson(p); Console.WriteLine();

        Console.WriteLine("Large Array(SmallObject[1000]) {0} Iteration", Iteration); Console.WriteLine();

        var C = SerializeJson(l); Console.WriteLine();

        Validate("NMessagePack", p, l, c, C);

        Console.WriteLine();
        Console.WriteLine("Additional Benchmarks"); Console.WriteLine();

        Console.WriteLine("Int32(1) {0} Iteration", Iteration); Console.WriteLine();

        var W2 = SerializeJson(integer); Console.WriteLine();

        Console.WriteLine("Vector3(float, float, float) {0} Iteration", Iteration); Console.WriteLine();

        var X2 = SerializeJson(v3); Console.WriteLine();

        Console.WriteLine("HtmlString({0}bytes) {1} Iteration"
            , Encoding.UTF8.GetByteCount(largeString)
            , Iteration); Console.WriteLine();

        var Y2 = SerializeJson(largeString); Console.WriteLine();

        Console.WriteLine("Vector3[100] {0} Iteration", Iteration); Console.WriteLine();

        var Z2 = SerializeJson(v3List); Console.WriteLine();

        Validate2("MsgPack-Cli", W2, integer);
        Validate2("MsgPack-Cli", X2, v3);
        Validate2("MsgPack-Cli", Y2, largeString);
        Validate2("MsgPack-Cli", Z2, v3List);

        //ストップウォッチを止める
        sw.Stop();

        //結果を表示する
        UnityEngine.Debug.LogFormat("[Benchmark]{0}", sw.Elapsed);
    }

    static T SerializeJson<T>(T original)
    {
        T copy = default(T);
        string json = null;

        // Note:We should check MessagePackSerializer.Get<T>() on every iteration
        // But currenly MsgPack-Cli has bug of get serializer
        // https://github.com/msgpack/msgpack-cli/issues/191
        // so, get serializer at first.
        // and If enum serialization options to ByUnderlyingValue, gets more fast but we check default option only.

        var r = new ObjectStructure.Json.TypeRegistory();
        var serializer = r.GetSerializer<T>();
        var deserializer = r.GetDeserializer<T>();

        using (new Measure("Serialize"))
        {
            for (int i = 0; i < Iteration; i++)
            {
                json = serializer.Serialize(original, r);
            }
        }

        using (new Measure("Deserialize"))
        {
            for (int i = 0; i < Iteration; i++)
            {
                //copy = NMessagePack.Deserializer.Deserialize<T>(bytes);
                copy=deserializer.Deserialize(ObjectStructure.Json.JsonParser.Parse(json), r);
            }
        }

        Assert.AreEqual(original, copy);

        using (new Measure("ReSerialize"))
        {
            for (int i = 0; i < Iteration; i++)
            {
                json = serializer.Serialize(copy, r);
            }
        }

        if (!dryRun)
        {
            //Console.WriteLine(string.Format("{0,15}   {1}", "Binary Size", ToHumanReadableSize(bytes.Length)));
        }

        return copy;
    }

}
