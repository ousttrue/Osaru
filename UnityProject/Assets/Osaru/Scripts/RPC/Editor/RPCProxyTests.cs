#if !NETFX_CORE
using NUnit.Framework;
using Osaru.Json;
using Osaru.MessagePack;
using Osaru.RPC;
using Osaru.Serialization;
using System;
using UniRx;


namespace OsaruTest.Reactive
{
    public class RPCProxyTests
    {
        delegate IObservable<int> Add(int a, int b);

        public class DuplexStream: IDuplexStream
        {
            IObservable<ArraySegment<Byte>> m_observable;
            public IObservable<ArraySegment<Byte>> ReadObservable
            {
                get
                {
                    return m_observable;
                }
            }

            IWritable m_writable;
            public IObservable<Unit> WriteAsync(ArraySegment<Byte> bytes)
            {
                return m_writable.WriteAsync(bytes);
            }

            public DuplexStream(IObservable<ArraySegment<Byte>> observable
                , IWritable writable)
            {
                m_observable = observable;
                m_writable = writable;
            }
        }

        public class Transport
        {
            public DuplexStream ServerSide
            {
                get; private set;
            }

            public DuplexStream ClientSide
            {
                get; private set;
            }

            class InOut: IWritable
            {
                Subject<ArraySegment<Byte>> m_subject = new Subject<ArraySegment<byte>>();
                public IObservable<ArraySegment<Byte>> ReadObservable
                {
                    get { return m_subject; }
                }
                public IObservable<Unit> WriteAsync(ArraySegment<byte> bytes)
                {
                    m_subject.OnNext(bytes);

                    return Observable.ReturnUnit();
                }
            }

            public Transport()
            {
                var c_to_s = new InOut();
                var s_to_c = new InOut();

                ServerSide = new DuplexStream(s_to_c.ReadObservable, c_to_s);
                ClientSide = new DuplexStream(c_to_s.ReadObservable, s_to_c);
            }
        }

        [Test]
        public void JsonRPCTest()
        {
            var transport = new Transport();

            // server side
            var service = new RpcService<JsonParser, JsonFormatter>();
            var r = new TypeRegistry();
            var method = r.RPCFunc((int a, int b) => a + b);
            service.Dispatcher.AddMethod("Add", method);
            service.AddStream(transport.ServerSide);

            // client side
            var requestTransporter = new RPCTransporter<JsonParser, JsonFormatter>(
                transport.ClientSide);
            var factory = new RPCRequestManager<JsonParser, JsonFormatter>();
            factory.RequestObservable.Subscribe(requestTransporter);
            requestTransporter.ResponseObservable.Subscribe(factory);

            // call
            var f = new RPCParamsFormatter<JsonFormatter>();
            int? result = null;
            factory.RequestAsync<int>("Add", f.Params(1, 2)).Subscribe(x =>
            {
                result = x;
            }
            , ex=>
            {
                throw ex;
            }
            );

            // wait result
            for(int i=0; i<100; ++i)
            {
                if (result.HasValue)
                {
                    break;
                }
                System.Threading.Thread.Sleep(100);
            }

            Assert.AreEqual(3, result.Value);
        }

        [Test]
        public void MessagePackRPCTest()
        {
            var transport = new Transport();

            // server side
            var service = new RpcService<MessagePackParser, MessagePackFormatter>();
            var r = new TypeRegistry();
            var method = r.RPCFunc((int a, int b) => a + b);
            service.Dispatcher.AddMethod("Add", method);
            service.AddStream(transport.ServerSide);

            // client side
            var requestTransporter = new RPCTransporter<MessagePackParser, MessagePackFormatter>(
                transport.ClientSide);
            var factory = new RPCRequestManager<MessagePackParser, MessagePackFormatter>();
            factory.RequestObservable.Subscribe(requestTransporter);
            requestTransporter.ResponseObservable.Subscribe(factory);

            // call
            var f = new RPCParamsFormatter<MessagePackFormatter>();
            int? result = null;
            factory.RequestAsync<int>("Add", f.Params(1, 2)).Subscribe(x =>
            {
                result = x;
            }
            , ex =>
            {
                throw ex;
            }
            );

            // wait result
            for (int i = 0; i < 100; ++i)
            {
                if (result.HasValue)
                {
                    break;
                }
                System.Threading.Thread.Sleep(100);
            }

            Assert.AreEqual(3, result.Value);
        }
    }
}
#endif