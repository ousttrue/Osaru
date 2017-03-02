# ObjectStructure
JSON parser for .Net3.5(Unity).

## Features
* inplace serialize
* inplace deserialize

## Usage

```cs
var json = "{\"key\":{ \"nestedKey\": \"nestedValue\" } }";
var node = Node.Parse(json);

Assert.AreEqual("nestedValue", node["key"]["nestedKey"].GetString());
```

# ToDO
* [ ] integrate [MsgPack library](https://github.com/ousttrue/NMessagePack)

