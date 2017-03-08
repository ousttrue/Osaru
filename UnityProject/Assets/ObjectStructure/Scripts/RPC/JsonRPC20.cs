using ObjectStructure.Json;
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

        public static RPCResponse<JsonParser> Process(this RPCDispatcher dispatcher, string src)
        {
            var json = JsonParser.Parse(src);
            var f = new JsonFormatter();
            var request = JsonRPC20.Request(json);

            var response = new RPCResponse<JsonParser>();
            response.Id = request.Id;
            try
            {
                dispatcher.Dispatch(request, f);
                response.Result = JsonParser.Parse(f.GetStore().Buffer(), ParseMode.ToEnd);
            }
            catch (Exception ex)
            {
                response.Error = ex;
            }
            return response;
        }
    }
}
