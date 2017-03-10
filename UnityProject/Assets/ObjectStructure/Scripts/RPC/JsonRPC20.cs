using ObjectStructure.Json;
using ObjectStructure.Serialization.Serializers;
using System;


namespace ObjectStructure.RPC
{
    public class JsonRPC20Exception : Exception
    {
        public JsonRPC20Exception(string message) : base(message)
        {
        }
    }


    public class JsonRPC20Formatter : IRPCFormatter
    {
        JsonFormatter m_f;
        RPCResponse<JsonParser> m_response;
        public RPCResponse<JsonParser> Response
        {
            get { return m_response; }
        }

        public JsonRPC20Formatter(RPCRequest<JsonParser> request)
        {
            m_f = new JsonFormatter();
            m_response.Id = request.Id;
        }

        public void Error(Exception ex)
        {
            m_response.Error = ex;
        }

        public void Success()
        {
            m_f.Null();
            m_response.Result = JsonParser.Parse(m_f.GetStore().Buffer()
                , ParseMode.ToEnd);
        }

        public void Success<R>(R result, SerializerBase<R> s)
        {
            s.Serialize(result, m_f);
            m_response.Result = JsonParser.Parse(m_f.GetStore().Buffer()
                , ParseMode.ToEnd);
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
            var request = JsonRPC20.Request(json);
            var formatter = new JsonRPC20Formatter(request);
            dispatcher.Dispatch(request, formatter);
            return formatter.Response;
        }
    }
}
