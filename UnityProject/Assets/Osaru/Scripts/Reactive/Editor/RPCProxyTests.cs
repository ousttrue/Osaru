using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UniRx;
using System.Reflection;
using Osaru.Reactive;
using Osaru.Json;
using Osaru.Serialization;
using System;

namespace OsaruTest.Reactive
{
    public class RPCProxyTests
    {
        delegate int Add(int a, int b);

        [Test]
        public void ProxyMehodTest()
        {
            var r = new TypeRegistory();
            var factory = new RPCProxyFactory();
            var proxy = factory.FuncProxy<int, int, int>("Add", r);

            int? result = null;

            proxy(1, 2).Subscribe(x =>
            {
                result = 3;
            });

            while (!result.HasValue)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
