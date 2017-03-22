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

    public interface IRPCTransporter<PARSER, FORMATTER> : IObserver<RPCRequest<PARSER>>
        where PARSER : IParser<PARSER>, new()
        where FORMATTER : IFormatter, new()
    {
        IObservable<RPCResponse<PARSER>> ResponseObservable { get; }
    }

    public class RPCTransporter<PARSER, FORMATTER> : IRPCTransporter<PARSER, FORMATTER>
        where PARSER : IParser<PARSER>, new()
        where FORMATTER : IFormatter, new()
    {
        IDuplexStream m_transport;
        SerializerBase<RPCRequest<PARSER>> m_s;
        IDeserializerBase<RPCResponse<PARSER>> m_d;
        public RPCTransporter(IDuplexStream transport)
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
}
