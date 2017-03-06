using NUnit.Framework;
using ObjectStructure.Json;
using ObjectStructure.RPC;
using ObjectStructure.Serialization;
using System;

namespace ObjectStructureTest.RPC
{
    public class RpcTests
    {
        class JsonRCP20Server
        {
            RPCDispatcher m_dispatcher = new RPCDispatcher();
            TypeRegistory m_registory = new TypeRegistory();
            public JsonRCP20Server()
            {
                m_dispatcher.AddMethod("Add"
                    , m_registory.RPCMethod((int a, int b) => a + b));
            }

            public string Process(string json)
            {
                var response = Process(JsonParser.Parse(json));
                var responseJson = m_registory.SerializeToJson(response);
                return responseJson;
            }

            RPCResponse<JsonParser> Process(JsonParser parser)
            {
                var f = new JsonFormatter();
                var request = JsonRPC20.Request(parser);

                var response = new RPCResponse<JsonParser>();
                response.Id = request.Id;
                try
                {
                    m_dispatcher.Dispatch(request, f);
                    response.Result = JsonParser.Parse(f.GetStore().Buffer(), ParseMode.ToEnd);
                }
                catch (Exception ex)
                {
                    response.Error = ex;
                }
                return response;
            }
        }

        [Test]
        public void JsonRpcTest()
        {
            var service = new JsonRCP20Server();
            var request = "{\"jsonrpc\":\"2.0\",\"method\":\"Add\",\"params\":[1,2],\"id\":1}";
            var response = service.Process(request);

            Assert.AreEqual("{\"jsonrpc\":\"2.0\",\"result\":3,\"id\":1}", response);
        }
    }
}
