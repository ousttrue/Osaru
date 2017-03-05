using System;
using System.Collections.Generic;

namespace ObjectStructure
{
    public interface IFormatter
    {
        void Clear();
        object Result();

        void OpenList(int n);
        void CloseLIst();
        void OpenMap(int n);
        void CloseMap();

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

        void Raw(IList<Byte> raw);
    }
}
