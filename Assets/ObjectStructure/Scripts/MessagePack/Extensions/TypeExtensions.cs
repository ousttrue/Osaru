using System;
using System.Collections;
using System.Linq;


namespace NMessagePack
{
    public static class TypeExtensions
    {
        public static bool IsSerializable(this Type t)
        {
            if (typeof(IEnumerable).IsAssignableFrom(t))
            {
                // collection
                return true;
            }
            if (t.GetCustomAttributes(typeof(SerializableAttribute), true).Any())
            {
                // serializable and primitive
                return true;
            }
            if (t.IsEnum)
            {
                // enum
                return true;
            }
            if (!t.IsClass)
            {
                // struct
                return true;
            }

            // class without serializable...
            return false;
        }
    }
}
