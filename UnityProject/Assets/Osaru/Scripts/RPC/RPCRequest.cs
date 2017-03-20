using System;


namespace Osaru.RPC
{
    public struct RPCRequest<T>
        where T : IParser<T>, new()
    {
        public string Method;
        public ArraySegment<byte> ParamsBytes;
        public T Params
        {
            get
            {
                var parser = new T();
                parser.SetBytes(ParamsBytes);
                return parser;
            }
        }
        public int Id;
    }
}
