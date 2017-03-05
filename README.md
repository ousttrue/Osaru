# ObjectStructure
.Net3.5(Unity) Serialization library.

```
+-------------+             +-----------+
|IDeserializer| -> value -> |ISerializer|
+-------------+             +-----------+
  A                           |
  |                           V
+-------+    convert        +----------+
|IParser| ----------------> |IFormatter|
+-------+                   +----------+
  A                           |
  |                           V
JSON, MessagePack           JSON, MessagePack
```

## Features
* JSON
* MessagePack
* separate parser and deserializer
* inplace serialization(json)
* inplace deserialization(json)

## Usage

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
* [ ] json from messagepack
* [ ] messagepack from json
* [ ] RPC
* [ ] code generator for RPC client

