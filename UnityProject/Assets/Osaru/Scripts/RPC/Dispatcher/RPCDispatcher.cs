using Osaru.Serialization;
using Osaru.Serialization.Deserializers;
using System;
using System.Collections.Generic;


namespace Osaru.RPC
{
    public class RPCDispatcher
    {
        TypeRegistry m_r;

        public RPCDispatcher(TypeRegistry r=null)
        {
            m_r= r ?? new TypeRegistry();
        }

        #region Request
        Dictionary<string, IRPCMethod> m_requestMap = new Dictionary<string, IRPCMethod>();
        public void AddRequestMethod(string name, IRPCMethod method)
        {
            m_requestMap.Add(name, method);
        }

        public ArraySegment<Byte> DispatchRequest<PARSER, FORMATTER>(ArraySegment<Byte> bytes)
            where PARSER : IParser<PARSER>, new()
            where FORMATTER : IFormatter, new()
        {
            var parser = new PARSER();
            parser.SetBytes(bytes);
            return DispatchRequest<PARSER, FORMATTER>(parser);
        }

        public ArraySegment<Byte> DispatchRequest<PARSER, FORMATTER>(PARSER parser)
            where PARSER : IParser<PARSER>, new()
            where FORMATTER : IFormatter, new()
        {
            var d = m_r.GetDeserializer<RPCRequest<PARSER>>();
            var request = d.Deserialize(parser);

            var response = DispatchRequest<PARSER, FORMATTER>(request);

            var f = new FORMATTER();
            var s = m_r.GetSerializer<RPCResponse<PARSER>>();
            s.Serialize(response, f);
            return f.GetStore().Bytes;
        }

        public RPCResponse<PARSER> DispatchRequest<PARSER, FORMATTER>(RPCRequest<PARSER> request)
            where PARSER: IParser<PARSER>, new()
            where FORMATTER: IFormatter, new()
        {
            var context = new RPCContext<PARSER>(request, new FORMATTER());
            DispatchRequest(context);
            return context.Response;
        }

        public void DispatchRequest<T>(RPCContext<T> context)
            where T: IParser<T>, new()
        {
            var method = m_requestMap[context.Request.Method];
            method.Call(context);
        }
        #endregion

        #region Notify
        Dictionary<string, IRPCMethod> m_notifyMap = new Dictionary<string, IRPCMethod>();

        public void DispatchNotify<PARSER>(PARSER parser)
            where PARSER : IParser<PARSER>, new()
        {
            var d = m_r.GetDeserializer<RPCRequest<PARSER>>();
            var request = d.Deserialize(parser);

            DispatchNotify<PARSER>(request);
        }

        public void DispatchNotify<PARSER>(RPCRequest<PARSER> request)
            where PARSER : IParser<PARSER>, new()
        {
            var method = m_requestMap[request.Method];
            method.Call(request);
        }
        #endregion
    }
}
