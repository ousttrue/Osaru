# ObjectStructure
.Net3.5(Unity) Serialize library.

## Features
* JSON
* MessagePack
* parser without deserialize
* inplace serialize(json)
* inplace deserialize(json)

## Usage

```cs
var src = "{\"key\":{ \"nestedKey\": \"nestedValue\" } }";
var json = JsonParser.Parse(src);

Assert.AreEqual("nestedValue", json["key"]["nestedKey"].GetString());
```

# ToDO
* [x] integrate [MsgPack library](https://github.com/ousttrue/NMessagePack)
* [ ] reorganize messagepack library
* [ ] json from messagepack
* [ ] messagepack from json
* [ ] RPC
* [ ] code generator for RPC client

