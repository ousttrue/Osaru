#if !NETFX_CORE
using Osaru.Json;
using Osaru.RPC;
using Osaru.Serialization;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UniRx;

namespace Osaru.Reactive
{
    public interface IRPCProxyFactory
    {
        DELEGATE CreateProxy<DELEGATE>(TypeRegistory r);
    }

    public interface IRequestFactory
    {
        RPCRequestContext NewRequest(string method);
    }

    public struct RPCRequestContext
    {
        public int ID;
        public IFormatter Formatter;

        public RPCRequestContext(int id, IFormatter formatter)
        {
            ID = id;
            Formatter = formatter;
        }

        public void End()
        {
            Formatter.Key("id"); Formatter.Value(ID);
            Formatter.EndMap();
        }
    }

    public class JsonRPC20RequestFactory : IRequestFactory
    {
        int m_id = 1;

        public RPCRequestContext NewRequest(string method)
        {
            var f = new JsonFormatter();
            f.BeginMap(4);
            f.Key("jsonrpc"); f.Value("2.0");
            f.Key("method"); f.Value(method);
            f.Key("params");

            return new RPCRequestContext(m_id++, f);
        }
    }

    public interface ITransport
    {
        IStore Store { get; }
        IObservable<ArraySegment<Byte>> BytesObservable { get; }
    }

    public class RPCProxyFactory
    {
        IRequestFactory m_factory = new JsonRPC20RequestFactory();

        IObservable<R> RequestAsync<A0, A1, R>(string name
            , Action<IFormatter> f0
            , Action<IFormatter> f1
            )
        {
            var request = m_factory.NewRequest(name);
            request.Formatter.BeginList(2);
            f0(request.Formatter);
            f1(request.Formatter);
            request.Formatter.EndList();
            request.End();

            var bytes = request.Formatter.GetStore().Bytes;
            var requestJson = Encoding.UTF8.GetString(bytes.Array, bytes.Offset, bytes.Count);

            return Observable.Create<R>(observer =>
            {
                // setup
                var typeRegistory = new TypeRegistory();
                var method = typeRegistory.RPCFunc((int a, int b) => a + b);
                var dispatcher = new RPCDispatcher();
                dispatcher.AddMethod("Add", method);

                // request
                //var request = "{\"jsonrpc\":\"2.0\",\"method\":\"Add\",\"params\":[1,2],\"id\":1}";
                var response = dispatcher.DispatchJsonRPC20(requestJson);
                var responseJson = JsonParser.Parse(response);

                var d = typeRegistory.GetDeserializer<R>();
                var r = default(R);
                d.Deserialize(responseJson["result"], ref r);

                observer.OnNext(r);
                observer.OnCompleted();

                return Disposable.Empty;
            });
        }

        public Func<A0, A1, IObservable<R>> FuncProxy<A0, A1, R>(string name, TypeRegistory r)
        {
            var s0 = r.GetSerializer<A0>();
            var s1 = r.GetSerializer<A1>();

            return (a0, a1) =>
                  RequestAsync<A0, A1, R>(name
                  , f => s0.Serialize(a0, f)
                  , f => s1.Serialize(a1, f)
                  );
                ;
        }
    }
}
#endif