using System;


namespace ObjectStructure.RPC
{
    public class JsonRPC20Exception : Exception
    {
        public JsonRPC20Exception(string message) : base(message)
        {
        }
    }

    public static class JsonRPC20
    {
        public static RPCRequest<T> Request<T>(T parser)
            where T : IParser<T>
        {
            if (parser["jsonrpc"].GetString() != "2.0")
                throw new JsonRPC20Exception("jsonrpc is not 2.0");

            return new RPCRequest<T>
            {
                Method = parser["method"].GetString(),
                Params = parser["params"],
                Id = parser["id"].GetInt32(),
            };
        }
    }
}
