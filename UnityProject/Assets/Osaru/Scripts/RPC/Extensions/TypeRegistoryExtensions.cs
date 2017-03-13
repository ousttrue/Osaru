using System;
using Osaru.Serialization;


namespace Osaru.RPC
{
    public static class TypeRegistoryExtensions
    {
        #region Action
        public static RPCAction<A0> RPCAction<A0>(
            this TypeRegistory r, Action<A0> p)
        {
            return new RPCAction<A0>(r
                , new RPCAction<A0>.Method(p));
        }

        public static RPCAction<A0, A1> RPCAction<A0, A1>(
            this TypeRegistory r, Action<A0, A1> p)
        {
            return new RPCAction<A0, A1>(r
                , new RPCAction<A0, A1>.Method(p));
        }

        public static RPCAction<A0, A1, A2> RPCAction<A0, A1, A2>(
            this TypeRegistory r, Action<A0, A1, A2> p)
        {
            return new RPCAction<A0, A1, A2>(r
                , new RPCAction<A0, A1, A2>.Method(p));
        }
        #endregion

        #region Func
        public static RPCFunc<R> RPCFunc<R>(
            this TypeRegistory r, Func<R> p)
        {
            return new RPCFunc<R>(r
                , new RPCFunc<R>.Method(p));
        }

        public static RPCFunc<A0, R> RPCFunc<A0, R>(
            this TypeRegistory r, Func<A0, R> p)
        {
            return new RPCFunc<A0, R>(r
                , new RPCFunc<A0, R>.Method(p));
        }

        public static RPCFunc<A0, A1, R> RPCFunc<A0, A1, R>(
            this TypeRegistory r, Func<A0, A1, R> p)
        {
            return new RPCFunc<A0, A1, R>(r
                , new RPCFunc<A0, A1, R>.Method(p));
        }
        #endregion
    }
}
