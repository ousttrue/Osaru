# Osaru
Object Serialization And RPC Utilities

.Net3.5(Unity) Serialization library.

```
                     +----------+
                     |RPC method|
                     +----------+
                       A     |
+----------------+     |     V    +--------------+
|IDeserializer<T>| ->  T     U -> |ISerializer<U>|
+----------------+                +--------------+
  A                                 |
  |                                 V
+-------+    convert              +----------+
|IParser| ----------------------> |IFormatter|
+-------+                         +----------+
  A                               |IStore    | --> Stream
  |           +-----------+       +--------- +
  +-----------|JSON       |<--------+
              |MessagePack|         Byte[]
              +-----------+
            serialized byte[]
```

# Features
* separate parser and deserializer
* inplace serialization
* inplace deserialization
* UWP compatible

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
// setup
var typeRegistory = new TypeRegistory();
var method = typeRegistory.RPCFunc((int a, int b) => a + b);
var dispatcher = new RPCDispatcher();
dispatcher.AddMethod("Add", method);

// request
var request = "{\"jsonrpc\":\"2.0\",\"method\":\"Add\",\"params\":[1,2],\"id\":1}";
var response = dispatcher.DispatchJsonRPC20(request);
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
* [ ] json base64 string for binary support
* [x] json byte[] backend not string
* [x] StreamStore
* [x] BytesStore
* [ ] fix dump
* [ ] rpc proxy
* [x] endian conversion use union
* [ ] StreamSubject
* [x] add Endian interface to IStore 

