namespace ObjectStructure.RPC
{
    public struct RPCRequest<T>
        where T : IParser<T>
    {
        public string Method;
        public T Params;
        public int Id;
    }
}
