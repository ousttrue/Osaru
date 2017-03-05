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

    public static class RPCMethod
    {
        public static RPCMethod<A0, A1, R> Create<A0, A1, R>(TypeRegistory r, Func<A0, A1, R> p)
        {
            return new RPCMethod<A0, A1, R>(r, new RPCMethod<A0, A1, R>.Method(p));
        }
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
        TypeRegistory m_r;
        Dictionary<string, IRPCMethod> m_map = new Dictionary<string, IRPCMethod>();

        public RPCDispatcher(TypeRegistory r=null)
        {
            m_r = r;
            if(m_r==null)
            {
                m_r = new TypeRegistory();
            }
        }

        public void AddMethod<A0, A1, R>(string name, Func<A0, A1, R> p)
        {
            m_map.Add(name, RPCMethod.Create(m_r, p));
        }

        public void Dispatch<T>(RPCRequest<T> request, IFormatter f)
            where T: IParser<T>
        {
            m_map[request.Method].Call(request.Params, f);
        }
    }
}
