using System;
using ObjectStructure.Serialization;


namespace ObjectStructure.RPC
{
    public static class TypeRegistoryExtensions
    {
        public static RPCMethod<A0, A1, R> RPCMethod<A0, A1, R>(
            this TypeRegistory r, Func<A0, A1, R> p)
        {
            return new RPCMethod<A0, A1, R>(r
                , new RPCMethod<A0, A1, R>.Method(p));
        }
    }
}
