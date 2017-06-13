#if !NETFX_CORE
using NUnit.Framework;
using Osaru;
using Osaru.RPC;
using Osaru.Serialization;
using System.Text;
using System.Linq;


namespace OsaruTest.RPC
{
    public class RpcTests
    {
        [Test]
        public void JsonRpcDispatchRequest()
        {
            // setup
            var typeRegistry = new TypeRegistry();
            var method = typeRegistry.RPCFunc((int a, int b) => a + b);
            var dispatcher = new RPCDispatcher();
            dispatcher.AddRequestMethod("Add", method);

            // request
            var request = "{\"jsonrpc\":\"2.0\",\"method\":\"Add\",\"params\":[1,2],\"id\":1}";
            var response = Encoding.UTF8.GetString(dispatcher.DispatchRequest<Osaru.Json.JsonParser, Osaru.Json.JsonFormatter>(request.ParseAsJson()).ToEnumerable().ToArray());
            Assert.AreEqual("{\"jsonrpc\":\"2.0\",\"result\":3,\"id\":1}", response);
        }

        [Test]
        public void JsonRpcDispatchNotify()
        {
            // setup
            var typeRegistry = new TypeRegistry();
            var method = typeRegistry.RPCFunc((int a, int b) => a + b);
            var dispatcher = new RPCDispatcher();
            dispatcher.AddRequestMethod("Add", method);

            // request
            var request = "{\"jsonrpc\":\"2.0\",\"method\":\"Add\",\"params\":[1,2],\"id\":1}";

            dispatcher.DispatchNotify(request.ParseAsJson());
        }
    }
}
#endif
