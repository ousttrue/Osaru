using Osaru.Serialization;
using Osaru.Serialization.Deserializers;
using System;
using System.Collections.Generic;


namespace Osaru.RPC
{
    public class RPCDispatcher
    {
        TypeRegistory m_r;
        Dictionary<string, IRPCMethod> m_map = new Dictionary<string, IRPCMethod>();

        public RPCDispatcher(TypeRegistory r=null)
        {
            m_r= r ?? new TypeRegistory();
        }

        public void AddMethod(string name, IRPCMethod method)
        {
            m_map.Add(name, method);
        }

        public ArraySegment<Byte> Dispatch<PARSER, FORMATTER>(ArraySegment<Byte> bytes)
            where PARSER : IParser<PARSER>, new()
            where FORMATTER : IFormatter, new()
        {
            var d = m_r.GetDeserializer<RPCRequest<PARSER>>();
            var parser = new PARSER();
            parser.SetBytes(bytes);
            var request = d.Deserialize(parser);

            var response = Dispatch<PARSER, FORMATTER>(request);

            var f = new FORMATTER();
            var s = m_r.GetSerializer<RPCResponse<PARSER>>();
            s.Serialize(response, f);
            return f.GetStore().Bytes;
        }

        public RPCResponse<PARSER> Dispatch<PARSER, FORMATTER>(RPCRequest<PARSER> request)
            where PARSER: IParser<PARSER>, new()
            where FORMATTER: IFormatter, new()
        {
            var context = new RPCContext<PARSER>(request, new FORMATTER());
            Dispatch(context);
            return context.Response;
        }

        public void Dispatch<T>(RPCContext<T> context)
            where T: IParser<T>, new()
        {
            m_map[context.Request.Method].Call(context);
        }
    }
}
