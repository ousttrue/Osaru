﻿using NUnit.Framework;
using ObjectStructure.Json;
using ObjectStructure.RPC;

namespace ObjectStructureTest.RPC
{
    public class RpcTests
    {
        [Test]
        public void JsonRpcTest()
        {
            var d = new RPCDispatcher();

            d.AddMethod("Add", (int a, int b)=>a+b);

            var json = "{\"jsonrpc\":\"2.0\", \"method\":\"Add\", \"params\":[1, 2], \"id\":1}";

            var request = JsonRPC20.Request(JsonParser.Parse(json));

            var f = new JsonFormatter();
            d.Dispatch(request, f);

            Assert.AreEqual("3", f.Result());
        }
    }
}
