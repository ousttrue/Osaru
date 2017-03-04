using System;

namespace ObjectStructure
{
    public interface IFormatter
    {
        void OpenList(int n);
        void CloseLIst();
        void OpenMap(int n);
        void CloseMap();

        void Key(String key);
        void Value(String key);

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
    }
}
