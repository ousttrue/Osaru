using Osaru.Serialization;
using System;


namespace Osaru.Json
{
    public static class TypeRegistoryExtensions
    {
        public static String SerializeToJson<T>(this TypeRegistory r, T value)
        {
            var s = r.GetSerializer<T>();
            return s.SerializeToJson(value);
        }
    }
}
