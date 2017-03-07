# ObjectStructure
.Net3.5(Unity) Serialization library.

```
                     +----------+
                     |RPC method|
                     +----------+
                       A     |
+----------------+     |     V    +-----------+
|IDeserializer<T>| ->  T     U -> |ISerializer|
+----------------+                +-----------+
  A                                 |
  |                                 V
+-------+    convert              +----------+
|IParser| ----------------------> |IFormatter|
+-------+                         +----------+
 A     A                            |    |
 |     |                            |    V
 |  JSON(string)                    V  JSON(string)
MessagePack(byte[])               MessagePack(byte[])
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
var service = new JsonRCP20Server();
var request = "{\"jsonrpc\":\"2.0\",\"method\":\"Add\",\"params\":[1,2],\"id\":1}";
var response = service.Process(request);
Assert.AreEqual("{\"jsonrpc\":\"2.0\",\"result\":3,\"id\":1}", response);
```

# ToDO
* [x] integrate [MsgPack library](https://github.com/ousttrue/NMessagePack)
* [x] reorganize messagepack library
* [ ] user class serializer
* [ ] json formatter test
* [x] json and messagepack converter
* [x] json-rpc-2.0
* [ ] messagepack-rpc
* [ ] code generator for RPC client
* [ ] json base64 string for binary support
* [ ] json byte[] backend not string

