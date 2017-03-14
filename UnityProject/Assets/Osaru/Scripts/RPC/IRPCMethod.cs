using Osaru.Serialization.Serializers;
using System;


namespace Osaru.RPC
{
    public interface IRPCContext<T>
        where T : IParser<T>
    {
        RPCRequest<T> Request { get; }
        //RPCResponse<T> Response { get; }
        void Success();
        void Success<R>(R result, SerializerBase<R> s);
        void Error(Exception ex);
    }

    public interface IRPCMethod
    {
        void Call<T>(IRPCContext<T> f)
            where T : IParser<T>;
    }
}
