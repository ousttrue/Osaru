using Osaru.Serialization;
using System;
using System.Collections.Generic;
using UniRx;


namespace Osaru.RPC
{
    public class RPCRequestManager<PARSER, FORMATTER> : IObserver<RPCResponse<PARSER>>
        where PARSER : IParser<PARSER>, new()
        where FORMATTER : IFormatter, new()
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

        TypeRegistry m_r;
        int m_id = 1;

        public RPCRequestManager()
        {
            m_r = new TypeRegistry();
        }

        Dictionary<int, DisposableRequest> m_requestMap = new Dictionary<int, DisposableRequest>();
        Subject<RPCRequest<PARSER>> m_requestSubject = new Subject<RPCRequest<PARSER>>();
        public IObservable<RPCRequest<PARSER>> RequestObservable
        {
            get { return m_requestSubject; }
        }

        public IObservable<R> RequestAsync<R>(string name, ArraySegment<Byte> rpcParams)
        {
            var d = m_r.GetDeserializer<R>();

            // request
            var request = new RPCRequest<PARSER>
            {
                Id = m_id++,
                Method = name,
                ParamsBytes = rpcParams
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
                //return;
            }

            disposable.Response(value);
        }
    }
}
