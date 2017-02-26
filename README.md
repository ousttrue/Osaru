# JsonSan
JSON parser for .Net3.5(Unity).

## Usage

```cs
var json = "{\"key\":{ \"nestedKey\": \"nestedValue\" } }";
var node = Node.Parse(json);

Assert.AreEqual("nestedValue", node["key"]["nestedKey"].GetString());
```

