using Osaru;
using Osaru.Json;
using Osaru.RPC;
using Osaru.Serialization;
using Osaru.Serialization.Deserializers;
using System.Collections.Generic;
using System;
using Osaru.MessagePack;


public static class RPCTypeSerializations
{
    public class JsonRPC20RequestDeserializer : IDeserializerBase<RPCRequest<JsonParser>>
    {
        public void Deserialize<PARSER>(PARSER parser, ref RPCRequest<JsonParser> outValue) where PARSER : IParser<PARSER>
        {
            foreach (var kv in parser.ObjectItems)
            {
                switch (kv.Key)
                {
                    case "jsonrpc":
                        if (kv.Value.GetString() != "2.0")
                        {
                            throw new FormatException("jsonrpc should 2.0");
                        }
                        break;

                    case "method":
                        outValue.Method = kv.Value.GetString();
                        break;

                    case "args":
                    case "params":
                        outValue.ParamsBytes = kv.Value.Dump();
                        break;

                    case "id":
                        outValue.Id = kv.Value.GetInt32();
                        break;

                    default:
                        throw new FormatException("unknown key: " + kv.Key);
                }
            }
        }

        public void Setup(TypeRegistry r)
        {
        }
    }
    public class JsonRPC20ResponseDeserializer : IDeserializerBase<RPCResponse<JsonParser>>
    {
        public void Deserialize<PARSER>(PARSER parser, ref RPCResponse<JsonParser> outValue) where PARSER : IParser<PARSER>
        {
            foreach (var kv in parser.ObjectItems)
            {
                switch (kv.Key)
                {
                    case "jsonrpc":
                        if (kv.Value.GetString() != "2.0")
                        {
                            throw new FormatException("jsonrpc should 2.0");
                        }
                        break;

                    case "result":
                        outValue.ResultBytes = kv.Value.Dump();
                        break;

                    case "error":
                        outValue.Error = kv.Value.GetString();
                        break;

                    case "id":
                        outValue.Id = kv.Value.GetInt32();
                        break;

                    default:
                        throw new FormatException("unknown key: " + kv.Key);
                }
            }

        }

        public void Setup(TypeRegistry r)
        {
        }
    }
    public class MessagePackRPCRequestDeserializer : IDeserializerBase<RPCRequest<MessagePackParser>>
    {
        public void Deserialize<PARSER>(PARSER parser, ref RPCRequest<MessagePackParser> outValue) where PARSER : IParser<PARSER>
        {
            var it = parser.ListItems.GetEnumerator();
            it.MoveNext(); if (it.Current.GetInt32() != 0) throw new FormatException("request should 0");
            it.MoveNext(); outValue.Id = it.Current.GetInt32();
            it.MoveNext(); outValue.Method = it.Current.GetString();
            it.MoveNext(); outValue.ParamsBytes = it.Current.Dump();
        }

        public void Setup(TypeRegistry r)
        {
        }
    }
    public class MessagePackRPCResponseDeserializer : IDeserializerBase<RPCResponse<MessagePackParser>>
    {
        public void Deserialize<PARSER>(PARSER parser, ref RPCResponse<MessagePackParser> outValue) where PARSER : IParser<PARSER>
        {
            var it = parser.ListItems.GetEnumerator();
            it.MoveNext(); if (it.Current.GetInt32() != 1) throw new FormatException("request should 1");
            it.MoveNext(); outValue.Id = it.Current.GetInt32();
            it.MoveNext();
            if (it.Current.IsNull)
            {
                // no error
                it.MoveNext(); outValue.ResultBytes = it.Current.Dump();
            }
            else
            {
                // error
                outValue.Error = it.Current.GetString();
            }
        }

        public void Setup(TypeRegistry r)
        {
        }
    }

    public static IEnumerable<TypeSerialization> Serializations
    {
        get
        {
            // json-rpc-2.0 Request
            yield return TypeSerialization.Create<RPCRequest<JsonParser>>(
                (x, f) =>
                {
                    f.BeginMap(4);
                    f.Key("jsonrpc"); f.Value("2.0");
                    f.Key("method"); f.Value(x.Method);
                    f.Key("params"); f.Dump(x.ParamsBytes);
                    f.Key("id"); f.Value(x.Id);
                    f.EndMap();
                }
                , new JsonRPC20RequestDeserializer());

            // json-rpc-2.0 Response
            yield return TypeSerialization.Create<RPCResponse<JsonParser>>(
                (x, f) =>
                {
                    if (x.Error == null)
                    {
                        f.BeginMap(3);
                        f.Key("jsonrpc"); f.Value("2.0");
                        f.Key("result"); f.Dump(x.ResultBytes);
                        f.Key("id"); f.Value(x.Id);
                        f.EndMap();
                    }
                    else
                    {
                        f.BeginMap(3);
                        f.Key("jsonrpc"); f.Value("2.0");
                        f.Key("error"); f.Value(x.Error);
                        f.Key("id"); f.Value(x.Id);
                        f.EndMap();
                        //throw res.Error;
                    }
                }
                , new JsonRPC20ResponseDeserializer());

            // messagepack-rpc Request
            yield return TypeSerialization.Create<RPCRequest<MessagePackParser>>(
                (x, f) =>
                {
                    f.BeginList(4);
                    f.Value(0);
                    f.Value(x.Id);
                    f.Value(x.Method);
                    f.Dump(x.ParamsBytes);
                    f.EndList();
                }
                , new MessagePackRPCRequestDeserializer());

            // messagepack-rpc Response
            yield return TypeSerialization.Create<RPCResponse<MessagePackParser>>(
                (x, f) =>
                {
                    if (x.Error == null)
                    {
                        f.BeginList(4);
                        f.Value(1);
                        f.Value(x.Id);
                        f.Null();
                        f.Dump(x.ResultBytes);
                        f.EndList();
                    }
                    else
                    {
                        f.BeginList(4);
                        f.Value(1);
                        f.Value(x.Id);
                        f.Value(x.Error);
                        f.Null();
                        f.EndList();
                    }
                }
                , new MessagePackRPCResponseDeserializer());
        }
    }
}
