using System.Collections.Generic;


namespace Osaru.RPC
{
    public class RPCDispatcher
    {
        Dictionary<string, IRPCMethod> m_map = new Dictionary<string, IRPCMethod>();

        public void AddMethod(string name, IRPCMethod method)
        {
            m_map.Add(name, method);
        }

        public void Dispatch<T>(IRPCResponseContext<T> f)
            where T: IParser<T>, new()
        {
            m_map[f.Request.Method].Call(f);
        }
    }
}
