using ObjectStructure.MessagePack;
using ObjectStructure.Serialization.Serializers;
using System;


namespace ObjectStructure.RPC
{
    public class MessagePackRPCException : Exception
    {
        public MessagePackRPCException(string message) : base(message)
        {
        }
    }


    public class MessagePackRPCContext : IRPCContext<MessagePackParser>
    {
        MessagePackFormatter m_f;

        RPCRequest<MessagePackParser> m_request;
        public RPCRequest<MessagePackParser> Request
        {
            get { return m_request; }
        }

        public BytesSegment Result
        {
            get
            {
                return m_f.GetStore().Bytes;
            }
        }

        public MessagePackRPCContext(Byte[] src)
        {
            var messagepack = MessagePackParser.Parse(src);
            m_request = MessagePackRPC.Request(messagepack);
            m_f = new MessagePackFormatter();
        }

        public MessagePackRPCContext(RPCRequest<MessagePackParser> request)
        {
            m_request = request;
            m_f = new MessagePackFormatter();
        }

        // [1, id, error, null]
        public void Error(Exception ex)
        {
            m_f.BeginList(4);
            m_f.Value(1);
            m_f.Value(Request.Id);
            m_f.Value(ex.Message);
            m_f.Null(); 
            m_f.EndList();
        }

        // [1, id, null, result]
        void BeginSuccess()
        {
            m_f.BeginList(4);
            m_f.Value(1);
            m_f.Value(Request.Id);
            m_f.Null();
        }

        void EndSuccess()
        {
            m_f.EndList();
        }

        public void Success()
        {
            BeginSuccess();
            m_f.Null();
            EndSuccess();
        }

        public void Success<R>(R result, SerializerBase<R> s)
        {
            BeginSuccess();
            s.Serialize(result, m_f);
            EndSuccess();
        }
    }


    public static class MessagePackRPC
    {
        public static RPCRequest<T> Request<T>(T parser)
            where T : IParser<T>
        {
            if (parser[0].GetInt32() != 0)
                throw new MessagePackRPCException("messagepack-rpc type is not 0");

            return new RPCRequest<T>
            {
                Id = parser[1].GetInt32(),
                Method = parser[2].GetString(),
                Params = parser[3],
            };
        }

        public static BytesSegment DispatchMessagePackRPC(this RPCDispatcher dispatcher,
            byte[] src)
        {
            var context = new MessagePackRPCContext(src);
            dispatcher.Dispatch(context);
            return context.Result;
        }
        public static BytesSegment DispatchMessagePackRPC(this RPCDispatcher dispatcher,
            RPCRequest<MessagePackParser> request)
        {
            var context = new MessagePackRPCContext(request);
            dispatcher.Dispatch(context);
            return context.Result;
        }
    }
}
