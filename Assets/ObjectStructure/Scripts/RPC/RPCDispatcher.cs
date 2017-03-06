using ObjectStructure.Serialization;
using ObjectStructure.Serialization.Deserializers;
using ObjectStructure.Serialization.Serializers;
using System;
using System.Collections.Generic;

namespace ObjectStructure.RPC
{
    public struct RPCRequest<T>
        where T: IParser<T>
    {
        public string Method;
        public T Params;
        public int Id;
    }

    public struct RPCResponse<T>
        where T : IParser<T>
    {
        public bool IsSuccess;
        public object Result;
        public int Id;
    }

    public class JsonRPC20Exception : Exception
    {
        public JsonRPC20Exception(string message) : base(message)
        {
        }
    }

    public static class JsonRPC20
    {
        public static RPCRequest<T> Request<T>(T parser)
            where T: IParser<T>
        {
            if (parser["jsonrpc"].GetString() != "2.0")
                throw new JsonRPC20Exception("jsonrpc is not 2.0");

            return new RPCRequest<T>
            {
                Method=parser["method"].GetString(),
                Params=parser["params"],
                Id=parser["id"].GetInt32(),
            };
        }
    }

    public interface IRPCMethod
    {
        void Call<T>(T request, IFormatter f)
            where T : IParser<T>;
    }

    public class RPCMethod<A0, A1, R>: IRPCMethod
    {
        IDeserializerBase<A0> m_d0;
        IDeserializerBase<A1> m_d1;
        SerializerBase<R> m_s;
        public delegate R Method(A0 a0, A1 a1);
        Method m_method;
        public RPCMethod(TypeRegistory r, Method method)
        {
            m_method = method;
            m_d0 = r.GetDeserializer<A0>();
            m_d1 = r.GetDeserializer<A1>();
            m_s = r.GetSerializer<R>();
        }

        public void Call<T>(T param, IFormatter f)
            where T : IParser<T>
        {
            var a0 = default(A0);
            m_d0.Deserialize(param[0], ref a0);

            var a1 = default(A1);
            m_d1.Deserialize(param[1], ref a1);

            m_s.Serialize(m_method(a0, a1), f);
        }
    }

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
