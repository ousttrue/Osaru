using System;
using System.Collections.Generic;


namespace ObjectStructure
{
    public enum ParserValueType
    {
        Unknown, // for Null

        List,
        Map,

        Boolean,
        Integer,
        Float,
        Double,
        String,
        Bytes,
    }

    public interface IParser<T>
        where T: IParser<T>
    {
        ParserValueType ValueType { get; }
        bool IsNull { get; }

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

        int GetBytesSize();
        void GetBytes(Byte[] bytes);
    }
}
