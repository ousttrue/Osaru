using System;

namespace ObjectStructure
{
    static class ArraySegementExtensions
    {
        //self[index] = value;
        public static void Set<T>(this ArraySegment<T> self, int index, T value)
        {
            if (index < 0 || index >= self.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            self.Array[self.Offset + index] = value;
        }
    }
}
