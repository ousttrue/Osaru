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
* JSON
* MessagePack
* separate parser and deserializer
* inplace serialization
* inplace deserialization

# RPC
* http://www.jsonrpc.org/specification
* https://github.com/msgpack-rpc/msgpack-rpc/blob/master/spec.md

# Usage

```cs
var src = "{\"key\":{ \"nestedKey\": \"nestedValue\" } }";
var json = JsonParser.Parse(src);

Assert.AreEqual("nestedValue", json["key"]["nestedKey"].GetString());
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

