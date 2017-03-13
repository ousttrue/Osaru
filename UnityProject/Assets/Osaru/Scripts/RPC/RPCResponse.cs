using Osaru.Serialization;
using System;


namespace Osaru.RPC
{
    public struct RPCResponse<T>
        where T : IParser<T>
    {
        public Exception Error;
        public T Result;
        public int Id;

        [Serializer]
        public static void Serialize(RPCResponse<T> res, IFormatter f)
        {
            if (res.Error == null)
            {
                f.BeginMap(3);
                f.Key("jsonrpc"); f.Value("2.0");
                f.Key("result"); res.Result.Dump(f);
                f.Key("id"); f.Value(res.Id);
                f.EndMap();
            }
            else
            {
                f.BeginMap(3);
                f.Key("jsonrpc"); f.Value("2.0");
                f.Key("error"); f.Value(res.Error.Message);
                f.Key("id"); f.Value(res.Id);
                f.EndMap();
                //throw res.Error;
            }
        }
    }
}
