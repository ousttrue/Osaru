using System;
using System.Collections.Generic;


namespace ObjectStructure
{
    public enum JsonValueType
    {
        Unknown,

        String,
        Number,
        Object,
        Array,
        Boolean,

        Close, // internal use
    }

    public interface IParser<T>
        where T: IParser<T>
    {
        String GetString();

        Byte GetByte();
        UInt16 GetUInt16();
        UInt32 GetUInt32();
        UInt64 GetUInt64();

        SByte GetSByte();
        Int16 GetInt16();
        Int32 GetInt32();
        Int64 GetInt64();

        Single GetSingle();
        Double GetDouble();

        IEnumerable<T> ArrayItems { get; }
        T this[int index] { get; }

        IEnumerable<KeyValuePair<String, T>> ObjectItems { get; }
        T this[string key] { get; }

        JsonValueType ValueType { get; }
    }
}
