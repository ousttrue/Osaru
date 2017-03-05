# ObjectStructure
.Net3.5(Unity) Serialization library.

```
+----------------+          +-----------+
|IDeserializer<T>| ->  T -> |ISerializer|
+----------------+          +-----------+
  A                           |
  |                           V
+-------+    convert        +----------+
|IParser| ----------------> |IFormatter|
+-------+                   +----------+
 A     A                      |    |
 |     |                      |    V
 |  JSON(string)              V  JSON(string)
MessagePack(byte[])         MessagePack(byte[])
```

# Features
* separate parser and deserializer
* inplace serialization
* inplace deserialization

# Formats

## JSON
* http://www.json.org/index.html
* http://www.jsonrpc.org/specification

## MessagePack
* https://github.com/msgpack/msgpack/blob/master/spec.md
* https://github.com/msgpack-rpc/msgpack-rpc/blob/master/spec.md

# Usage

```cs
var src = "{\"key\":{ \"nestedKey\": \"nestedValue\" } }";
var json = JsonParser.Parse(src);

Assert.AreEqual("nestedValue", json["key"]["nestedKey"].GetString());
```

## RPC

```cs
var d = new RPCDispatcher();

d.AddMethod("Add", (int a, int b)=>a+b);

var json = "{\"jsonrpc\":\"2.0\", \"method\":\"Add\", \"params\":[1, 2], \"id\":1}";

var request = JsonRPC20.Request(JsonParser.Parse(json));

var f = new JsonFormatter();
d.Dispatch(request, f);

Assert.AreEqual("3", f.Result());
```

# ToDO
* [x] integrate [MsgPack library](https://github.com/ousttrue/NMessagePack)
* [x] reorganize messagepack library
* [ ] user class serializer
* [ ] json formatter test
* [x] json and messagepack converter
* [ ] RPC
* [ ] code generator for RPC client
* [ ] json base64 string for binary support

