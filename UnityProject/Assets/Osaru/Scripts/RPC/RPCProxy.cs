#if !NETFX_CORE
using Osaru.Serialization;
using Osaru.Serialization.Deserializers;
using Osaru.Serialization.Serializers;
using System;
using System.Collections.Generic;
using UniRx;


namespace Osaru.RPC
{
    public interface IRPCRequestTransporter<PARSER, FORMATTER> : IObserver<RPCRequest<PARSER>>
        where PARSER : IParser<PARSER>, new()
        where FORMATTER : IFormatter, new()
    {
        IObservable<RPCResponse<PARSER>> ResponseObservable { get; }
    }
    public class RPCRequestTransporter<PARSER, FORMATTER> : IRPCRequestTransporter<PARSER, FORMATTER>
        where PARSER : IParser<PARSER>, new()
        where FORMATTER : IFormatter, new()
    {
        IDuplexStream m_transport;
        SerializerBase<RPCRequest<PARSER>> m_s;
        IDeserializerBase<RPCResponse<PARSER>> m_d;
        public RPCRequestTransporter(IDuplexStream transport)
        {
            m_transport = transport;
            var r = new TypeRegistory();
            m_s = r.GetSerializer<RPCRequest<PARSER>>();
            m_d = r.GetDeserializer<RPCResponse<PARSER>>();

            transport.ReadObservable.Subscribe(x =>
            {
                var parser = new PARSER();
                parser.SetBytes(x);
                var response = default(RPCResponse<PARSER>);
                m_d.Deserialize(parser, ref response);
                m_subject.OnNext(response);
            });
        }

        Subject<RPCResponse<PARSER>> m_subject = new Subject<RPCResponse<PARSER>>();
        public IObservable<RPCResponse<PARSER>> ResponseObservable
        {
            get
            {
                return m_subject;
            }
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            throw error;
            //Console.WriteLine(error);
        }

        public void OnNext(RPCRequest<PARSER> value)
        {
            var f = new FORMATTER();
            m_s.Serialize(value, f);
            m_transport.WriteAsync(f.GetStore().Bytes);
        }
    }

    public class RPCRequestFactory<PARSER, FORMATTER>: IObserver<RPCResponse<PARSER>>
        where PARSER: IParser<PARSER>, new()
        where FORMATTER: IFormatter, new()
    {
        public class DisposableRequest : IDisposable
        {
            public RPCRequest<PARSER> Request
            {
                get;
                private set;
            }

            bool m_canceld;
            public bool IsCanceled
            {
                get { return m_canceld; }
            }
            public void Dispose()
            {
                m_canceld = true;
            }

            Action<RPCResponse<PARSER>> m_onResponse;

            public DisposableRequest(RPCRequest<PARSER> request
                , Action<RPCResponse<PARSER>> onResponse)
            {
                Request = request;
                m_onResponse = onResponse;
            }

            public void Response(RPCResponse<PARSER> response)
            {
                if (IsCanceled)
                {
                    return;
                }
                m_onResponse(response);
            }
        }

        TypeRegistory m_r;
        int m_id = 1;

        public RPCRequestFactory()
        {
            m_r = new TypeRegistory();
        }

        Dictionary<int, DisposableRequest> m_requestMap = new Dictionary<int, DisposableRequest>();
        Subject<RPCRequest<PARSER>> m_requestSubject = new Subject<RPCRequest<PARSER>>();
        public IObservable<RPCRequest<PARSER>> RequestObservable
        {
            get { return m_requestSubject; }
        }

        public IObservable<R> RequestAsync<A0, A1, R>(string name, A0 a0, A1 a1)
        {
            var s0 = m_r.GetSerializer<A0>();
            var s1 = m_r.GetSerializer<A1>();
            var d = m_r.GetDeserializer<R>();

            // request
            var f = new FORMATTER();
            f.Clear();
            f.BeginList(2);
            s0.Serialize(a0, f);
            s1.Serialize(a1, f);
            f.EndList();
            var request = new RPCRequest<PARSER>
            {
                Id = m_id++,
                Method = name,
                ParamsBytes = f.GetStore().Bytes
            };

            return Observable.Create<R>(observer =>
            {
                var disposable = new DisposableRequest(request, res =>
                {
                    if (res.Error != null)
                    {
                        observer.OnError(new Exception(res.Error));
                    }
                    else
                    {
                        var r = default(R);
                        d.Deserialize(res.Result, ref r);
                        observer.OnNext(r);
                        observer.OnCompleted();
                    }
                });
                m_requestMap.Add(request.Id, disposable);
                m_requestSubject.OnNext(request);

                return disposable;
            });
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            //Console.WriteLine(error);
            throw error;
        }

        public void OnNext(RPCResponse<PARSER> value)
        {
            DisposableRequest disposable;
            if (!m_requestMap.TryGetValue(value.Id, out disposable))
            {
                throw new Exception("no request for " + value.Id);
                //Console.WriteLine("no request for " + value.Id);
                return;
            }

            disposable.Response(value);
        }
    }
}
#endif
