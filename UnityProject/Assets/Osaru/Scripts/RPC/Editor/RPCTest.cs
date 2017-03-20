#if !NETFX_CORE
using NUnit.Framework;
using Osaru.RPC;
using Osaru.Serialization;
using System.Text;
using System;
using System.Linq;
using UniRx;
using System.Reflection;
using System.IO;
using Osaru.MessagePack;

namespace OsaruTest.RPC
{
    public class RpcTests
    {
        [Test]
        public void JsonRpcTest()
        {
            // setup
            var typeRegistory = new TypeRegistory();
            var method = typeRegistory.RPCFunc((int a, int b) => a + b);
            var dispatcher = new RPCDispatcher();
            dispatcher.AddMethod("Add", method);

            // request
            var request = "{\"jsonrpc\":\"2.0\",\"method\":\"Add\",\"params\":[1,2],\"id\":1}";
            var response = Encoding.UTF8.GetString(dispatcher.DispatchJsonRPC20(request).ToEnumerable().ToArray());
            Assert.AreEqual("{\"jsonrpc\":\"2.0\",\"result\":3,\"id\":1}", response);
        }
    }
}
#endif