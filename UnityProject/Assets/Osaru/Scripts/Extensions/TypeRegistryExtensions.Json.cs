using Osaru.Serialization;
using System;


namespace Osaru
{
    public static partial class TypeRegistryExtensions
    {
        public static ArraySegment<Byte> SerializeToJsonBytes<T>(this TypeRegistry r, T value)
        {
            var s = r.GetSerializer<T>();
            return s.SerializeToJsonBytes(value);
        }
        public static string SerializeToJson<T>(this TypeRegistry r, T value)
        {
            var s = r.GetSerializer<T>();
            return s.SerializeToJson(value);
        }
    }
}
