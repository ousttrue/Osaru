using ObjectStructure.Serialization;
using ObjectStructure.Serialization.Deserializers;
using ObjectStructure.Serialization.Serializers;


namespace ObjectStructure
{
    public interface IRPCMethod
    {
        void Call<T>(T request, IFormatter f)
            where T : IParser<T>;
    }

    // Action<A0>
    public class RPCAction<A0> : IRPCMethod
    {
        IDeserializerBase<A0> m_d0;
        public delegate void Method(A0 a0);
        Method m_method;
        public RPCAction(TypeRegistory r, Method method)
        {
            m_method = method;
            m_d0 = r.GetDeserializer<A0>();
        }

        public void Call<T>(T param, IFormatter f)
            where T : IParser<T>
        {
            var a0 = default(A0);
            m_d0.Deserialize(param[0], ref a0);

            m_method(a0);
            f.Null();
        }
    }

    // Action<A0, A1>
    public class RPCAction<A0, A1> : IRPCMethod
    {
        IDeserializerBase<A0> m_d0;
        IDeserializerBase<A1> m_d1;
        public delegate void Method(A0 a0, A1 a1);
        Method m_method;
        public RPCAction(TypeRegistory r, Method method)
        {
            m_method = method;
            m_d0 = r.GetDeserializer<A0>();
            m_d1 = r.GetDeserializer<A1>();
        }

        public void Call<T>(T param, IFormatter f)
            where T : IParser<T>
        {
            var a0 = default(A0);
            m_d0.Deserialize(param[0], ref a0);

            var a1 = default(A1);
            m_d1.Deserialize(param[1], ref a1);

            m_method(a0, a1);
            f.Null();
        }
    }

    // Func<R>
    public class RPCFunc<R> : IRPCMethod
    {
        SerializerBase<R> m_s;
        public delegate R Method();
        Method m_method;
        public RPCFunc(TypeRegistory r, Method method)
        {
            m_method = method;
            m_s = r.GetSerializer<R>();
        }

        public void Call<T>(T param, IFormatter f)
            where T : IParser<T>
        {
            m_s.Serialize(m_method(), f);
        }
    }

    // Func<A0, R>
    public class RPCFunc<A0, R> : IRPCMethod
    {
        IDeserializerBase<A0> m_d0;
        SerializerBase<R> m_s;
        public delegate R Method(A0 a0);
        Method m_method;
        public RPCFunc(TypeRegistory r, Method method)
        {
            m_method = method;
            m_d0 = r.GetDeserializer<A0>();
            m_s = r.GetSerializer<R>();
        }

        public void Call<T>(T param, IFormatter f)
            where T : IParser<T>
        {
            var a0 = default(A0);
            m_d0.Deserialize(param[0], ref a0);

            m_s.Serialize(m_method(a0), f);
        }
    }

    // Func<A0, A1, R>
    public class RPCFunc<A0, A1, R> : IRPCMethod
    {
        IDeserializerBase<A0> m_d0;
        IDeserializerBase<A1> m_d1;
        SerializerBase<R> m_s;
        public delegate R Method(A0 a0, A1 a1);
        Method m_method;
        public RPCFunc(TypeRegistory r, Method method)
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
}
