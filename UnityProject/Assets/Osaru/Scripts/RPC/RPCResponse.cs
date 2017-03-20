using System;


namespace Osaru.RPC
{
    public struct RPCResponse<T>
        where T : IParser<T>, new()
    {
        public String Error;
        public ArraySegment<Byte> ResultBytes;
        public T Result
        {
            get
            {
                var parser = new T();
                parser.SetBytes(ResultBytes);
                return parser;
            }
        }
        public int Id;
    }
}
