using Osaru.Serialization;
using Osaru.Serialization.Deserializers;
using Osaru.Serialization.Serializers;
using System;
using UniRx;


namespace Osaru.RPC
{
    public interface IReadObservable
    {
        IObservable<ArraySegment<Byte>> ReadObservable { get; }
    }
    public interface IWritable
    {
        IObservable<Unit> WriteAsync(ArraySegment<Byte> bytes);
    }
    public interface IDuplexStream : IReadObservable, IWritable
    {
    }

    public class RpcService<PARSER, FORMATTER> : IDisposable
        where PARSER : IParser<PARSER>, new()
        where FORMATTER: IFormatter, new()
    {
        TypeRegistory m_r = new TypeRegistory();
        IDeserializerBase<RPCRequest<PARSER>> m_d;
        SerializerBase<RPCResponse<PARSER>> m_s;

        RPCDispatcher m_dispatcher;
        public RPCDispatcher Dispatcher
        {
            get { return m_dispatcher; }
        }

        CompositeDisposable m_disposable = new CompositeDisposable();

        public RpcService()
        {
            m_d = m_r.GetDeserializer<RPCRequest<PARSER>>();
            m_s = m_r.GetSerializer<RPCResponse<PARSER>>();
            m_dispatcher = new RPCDispatcher();
        }

        void OnRequestBytes(ArraySegment<Byte> x, IWritable transport)
        {
            var req = default(RPCRequest<PARSER>);

            try
            {
                var parser = new PARSER();
                parser.SetBytes(x);
                m_d.Deserialize(parser, ref req);
            }
            catch (Exception ex)
            {
                // parse error
                var errorResponse = new RPCResponse<PARSER>
                {
                    Id = req.Id,
                    Error = ex.Message,
                };
                var responseFormatter = new FORMATTER();
                m_s.Serialize(errorResponse, responseFormatter);
                transport.WriteAsync(responseFormatter.GetStore().Bytes).Subscribe();
                return;
            }

            try
            {
                var formatter = new FORMATTER();
                var context = new RPCResponseContext<PARSER>(req, formatter);
                m_dispatcher.Dispatch(context);

                var responseFormatter = new FORMATTER();
                m_s.Serialize(context.Response, responseFormatter);
                transport.WriteAsync(responseFormatter.GetStore().Bytes).Subscribe();
            }
            catch (Exception ex)
            {
                // call error
                var errorResponse = new RPCResponse<PARSER>
                {
                    Id = req.Id,
                    Error = ex.Message,
                };
                var responseFormatter = new FORMATTER();
                m_s.Serialize(errorResponse, responseFormatter);
                transport.WriteAsync(responseFormatter.GetStore().Bytes).Subscribe();
                return;
            }
        }

        public void AddStream(IDuplexStream transport)
        {
            transport.ReadObservable.Subscribe(x =>
            {
                OnRequestBytes(x, transport);
            }
            , ex =>
            {
                Console.WriteLine(ex);
            }
            , () =>
            {
            })
            .AddTo(m_disposable)
            ;
        }

        public void Dispose()
        {
            m_disposable.Dispose();
        }
    }
}
