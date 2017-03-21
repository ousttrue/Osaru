# Osaru
Object Serialization And RPC Utilities

.Net3.5(Unity) Serialization library.

```
                     +----------+
                     |RPC method|
                     +----------+
                       ^     |
+----------------+     |     v    +--------------+
|IDeserializer<T>| ->  T     U -> |ISerializer<U>|
+----------------+                +--------------+
  ^                                 |
  |                                 v
+-------+    convert              +----------+
|IParser| ----------------------> |IFormatter|
+-------+                         +----------+
  ^                               |IStore    | --> Stream
  |           +-----------+       +--------- +
  +-----------|JSON       |<--------+
              |MessagePack|         Byte[]
              +-----------+
            serialized byte[]
```

# Features
* separate deserializer and parser
* separate serializer and formatter
* inplace serialization
* inplace deserialization(but struct field setter use boxing)
* UWP compatible

# Formats

## JSON
* http://www.json.org/index.html
* http://www.jsonrpc.org/specification

## MessagePack
* https://github.com/msgpack/msgpack/blob/master/spec.md
* https://github.com/msgpack-rpc/msgpack-rpc/blob/master/spec.md

# Usage

## parse json

```cs
var src = "{\"key\":{ \"nestedKey\": \"nestedValue\" } }";
var json = JsonParser.Parse(src);

Assert.AreEqual("nestedValue", json["key"]["nestedKey"].GetString());
```

## json array

```cs
var json = JsonParser.Parse("[1, 2, 3]");
for(var item in json.ListItems)
{
    Console.WriteLine(item.GetInt32());
}
```

## json object

```cs
var json = JsonParser.Parse("{\"key\": \"value\"}");
for(var item in json.ObjectItems)
{
    Console.WriteLine(item.Key); // JSON allow only string key
    Console.WriteLine(item.Value.GetString());
}
```

## format json

```cs
var f=new JsonFormatter();
f.Value("abc");
Console.WriteLine(f.ToString()); // "abc"

f.Clear();
f.BeginList();
f.Value(true);
f.Null();
f.Value(1);
f.EndList();
Console.WriteLine(f.ToString()); // [true,null,1]

f.Clear();
f.BeginMap();
f.Key("key1"); f.Value(true);
f.Key("key2"); f.Null();
f.Key("key3"); f.Value(1);
f.EndMap();
Console.WriteLine(f.ToString()); // {"key1":true,"key2":null,"key3":1}

// get bytes
ArraySegment<Byte>=f.GetStore().Bytes;
```

## serialize & deserialize

```cs
class Point
{
    public int X;
    public int Y;
    public Point(int x, int y)
    {
        X=x;
        Y=y;
    }
}

var r=new TypeRegistory();

// serialize
var bytes=r.SerializeToJsonBytes(new Point(1, 2));

// parse as json
var json=bytes.ParseAsJson();
Console.WriteLine(json["X"]); // 1
Console.WriteLine(json["Y"]); // 2

// deserialize
var p=default(Point);
r.Deserialize(json, ref p);
Console.WriteLine(p.X); // 1
Console.WriteLine(p.Y); // 2
```

## RPC

```cs
// setup
var typeRegistory = new TypeRegistory();
var method = typeRegistory.RPCFunc((int a, int b) => a + b);
var dispatcher = new RPCDispatcher();
dispatcher.AddMethod("Add", method);

// request
var request = "{\"jsonrpc\":\"2.0\",\"method\":\"Add\",\"params\":[1,2],\"id\":1}";
var requestBytes=Encoding.UTF8.GetBytes(request);
var responseBytes = dispatcher.Dispatch(new ArraySegment<Byte>(requestBytes));
var response=Encoding.UTF8.GetString(responseBytes.Array, responseBytes.Offset, responseBytes.Count);
Assert.AreEqual("{\"jsonrpc\":\"2.0\",\"result\":3,\"id\":1}", response);
```

# ToDO
* [x] integrate [MsgPack library](https://github.com/ousttrue/NMessagePack)
* [x] reorganize messagepack library
* [x] fix UWP UnitTest
* [x] RPCFormatter
* [x] user class serialization
* [x] json and messagepack converter
* [x] json-rpc-2.0
* [x] messagepack-rpc
* [ ] code generator for RPC client
* [x] json base64 string for binary support
* [x] json byte[] backend not string
* [x] StreamStore
* [x] BytesStore
* [x] fix IParser.Dump
* [x] rpc proxy
* [x] endian conversion use union
* [x] add Endian interface to IStore 
* [ ] commonalize tests for json and messagepack
* [ ] organize extensions

