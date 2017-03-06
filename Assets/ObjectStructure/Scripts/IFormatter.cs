using System;
using System.Collections.Generic;

namespace ObjectStructure
{
    public interface IFormatter
    {
        void BeginList(int n);
        void EndList();
        void BeginMap(int n);
        void EndMap();

        void Null();

        void Key(String key);
        void Value(String value);

        void Value(Boolean value);

        void Value(SByte value);
        void Value(Int16 value);
        void Value(Int32 value);
        void Value(Int64 value);

        void Value(Byte value);
        void Value(UInt16 value);
        void Value(UInt32 value);
        void Value(UInt64 value);

        void Value(Single value);
        void Value(Double value);

        void Bytes(IEnumerable<Byte> raw, int count);

        /// <summary>
        /// add already formatted
        /// </summary>
        /// <param name="t"></param>
        void Dump(object formatted);
    }

    public interface IFormatter<T> : IFormatter
    {
        void Clear();
        IStore<T> GetStore();
    }

    public interface IStore<T>
    {
        void Clear();
        T Buffer();
    }

    public static class IStoreFormatterExtensions
    {
        public static void Reset<T>(this IFormatter<T> f)
        {
            f.Clear();
            f.GetStore().Clear();
        }
    }
}
