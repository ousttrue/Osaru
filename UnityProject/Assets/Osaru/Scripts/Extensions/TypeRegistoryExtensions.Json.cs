using Osaru.Serialization;
using System;


namespace Osaru
{
    public static partial class TypeRegistoryExtensions
    {
        public static ArraySegment<Byte> SerializeToJsonBytes<T>(this TypeRegistory r, T value)
        {
            var s = r.GetSerializer<T>();
            return s.SerializeToJsonBytes(value);
        }
        public static string SerializeToJson<T>(this TypeRegistory r, T value)
        {
            var s = r.GetSerializer<T>();
            return s.SerializeToJson(value);
        }
    }
}
