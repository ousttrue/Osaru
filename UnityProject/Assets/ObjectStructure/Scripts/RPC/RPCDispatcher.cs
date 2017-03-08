using System.Collections.Generic;


namespace ObjectStructure.RPC
{
    public class RPCDispatcher
    {
        Dictionary<string, IRPCMethod> m_map = new Dictionary<string, IRPCMethod>();

        public void AddMethod(string name, IRPCMethod method)
        {
            m_map.Add(name, method);
        }

        public void Dispatch<T>(RPCRequest<T> request, IFormatter f)
            where T: IParser<T>
        {
            m_map[request.Method].Call(request.Params, f);
        }
    }
}
