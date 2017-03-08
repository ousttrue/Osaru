using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if NETFX_CORE
using System.Reflection;
#endif

namespace ObjectStructure
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
            if (t.IsEnum())
            {
                // enum
                return true;
            }
            if (!t.IsClass())
            {
                // struct
                return true;
            }

            // class without serializable...
            return false;
        }

#if NETFX_CORE
        public static bool IsAssignableFrom(this Type t, Type type)
        {
            return t.GetTypeInfo().IsSubclassOf(type);
        }

        public static IEnumerable<Attribute> GetCustomAttributes(this Type t)
        {
            return t.GetTypeInfo().GetCustomAttributes();
        }
        public static IEnumerable<Attribute> GetCustomAttributes(this Type t, Type type, Boolean b)
        {
            return t.GetTypeInfo().GetCustomAttributes(type, b);
        }
        public static bool IsEnum(this Type t)
        {
            return t.GetTypeInfo().IsEnum;
        }
        public static bool IsClass(this Type t)
        {
            return t.GetTypeInfo().IsClass;
        }
        public static bool IsGenericType(this Type t)
        {
            return t.GetTypeInfo().IsGenericType;
        }
        public static bool IsInterface(this Type t)
        {
            return t.GetTypeInfo().IsInterface;
        }

        public static bool AttributeIsDefined<T>(this Type t)
            where T: Attribute
        {
            if (t == typeof(Single))
            {
                return true;
            }
            return t.GetTypeInfo().GetCustomAttribute<T>() != null;
        }
#else
        public static bool IsEnum(this Type t)
        {
            return t.IsEnum;
        }
        public static bool IsClass(this Type t)
        {
            return t.IsClass;
        }
        public static bool IsGenericType(this Type t)
        {
            return t.IsGenericType;
        }
        public static bool IsInterface(this Type t)
        {
            return t.IsInterface;
        }

        public static bool AttributeIsDefined<T>(this Type t)
            where T: Attribute
        {
            return Attribute.IsDefined(t, typeof(T));
        }
#endif
    }
}
