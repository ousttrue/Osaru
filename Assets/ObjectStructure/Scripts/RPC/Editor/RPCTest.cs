using NUnit.Framework;
using ObjectStructure.Json;
using ObjectStructure.RPC;
using ObjectStructure.Serialization;
using System;

namespace ObjectStructureTest.RPC
{
    public class RpcTests
    {
        [Test]
        public void JsonRpcTest()
        {
            var d = new RPCDispatcher();

            var r = new TypeRegistory();
            d.AddMethod("Add", r.RPCMethod((int a, int b)=>a+b));

            var json = "{\"jsonrpc\":\"2.0\", \"method\":\"Add\", \"params\":[1, 2], \"id\":1}";

            var request = JsonRPC20.Request(JsonParser.Parse(json));

            var f = new JsonFormatter();

            var response = new RPCResponse<JsonParser>();
            response.Id = request.Id;
            try
            {
                d.Dispatch(request, f);
                response.IsSuccess = true;
                response.Result = f.Result();
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Result = ex;
            }

            var responseJson = r.SerializeToJson(response);
            var responseParsed = JsonParser.Parse(responseJson);
            Assert.AreEqual(3, responseParsed["result"].GetInt32());
        }
    }
}
