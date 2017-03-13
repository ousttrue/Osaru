using NUnit.Framework;
using Osaru.RPC;
using Osaru.Serialization;
using System.Text;
using System;
using System.Linq;
using UniRx;
using System.Reflection;

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

        delegate IObservable<int> Add(int a, int b);

        static class ProxyFactory
        {
            public static Delegate CreateDelegate<Delegate>()
            {
                var t = typeof(Delegate);
                var mi = t.GetMethod("Invoke"
                    , BindingFlags.Instance | BindingFlags.Public);

                return (Delegate)(object)new Add((int a, int b) => Observable.Return(a + b));
            }
        }

        [Test]
        public void ProxyMehodTest()
        {
            var proxy = ProxyFactory.CreateDelegate<Add>();

            int? result = null;

            proxy(1, 2).Subscribe(x =>
            {
                result = 3;
            });

            while (!result.HasValue)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
