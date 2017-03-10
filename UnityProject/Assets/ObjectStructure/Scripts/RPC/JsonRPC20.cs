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


    public class JsonRPC20Context : IRPCContext<JsonParser>
    {
        JsonFormatter m_f;

        RPCRequest<JsonParser> m_request;
        public RPCRequest<JsonParser> Request
        {
            get { return m_request; }
        }

        public string Result
        {
            get
            {
                return m_f.GetStore().Buffer();
            }
        }

        public JsonRPC20Context(string src)
        {
            var json = JsonParser.Parse(src);
            m_request = JsonRPC20.Request(json);
            m_f = new JsonFormatter();
        }

        public JsonRPC20Context(RPCRequest<JsonParser> request)
        {
            m_request = request;
            m_f = new JsonFormatter();
        }

        public void Error(Exception ex)
        {
            m_f.BeginMap(3);
            m_f.Key("jsonrpc"); m_f.Value("2.0");
            m_f.Key("error"); m_f.Value(ex.Message);
            m_f.Key("id"); m_f.Value(Request.Id);
            m_f.EndMap();
        }

        void BeginSuccess()
        {
            m_f.BeginMap(3);
            m_f.Key("jsonrpc"); m_f.Value("2.0");
            m_f.Key("result");
        }

        void End()
        {
            m_f.Key("id"); m_f.Value(Request.Id);
            m_f.EndMap();
        }

        public void Success()
        {
            BeginSuccess();
            m_f.Null();
            End();
        }

        public void Success<R>(R result, SerializerBase<R> s)
        {
            BeginSuccess();
            s.Serialize(result, m_f);
            End();
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

        public static string DispatchJsonRPC20(this RPCDispatcher dispatcher,
            string requestJson)
        {
            var context = new JsonRPC20Context(requestJson);
            dispatcher.Dispatch(context);
            return context.Result;
        }
    }
}
