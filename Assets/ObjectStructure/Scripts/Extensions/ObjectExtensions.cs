using System;


namespace NMessagePack
{
    public static class ObjectExtensions
    {

        public static T ConvertTo<T>(this object o)
        {
            try
            {
                if (typeof(T).IsAssignableFrom(o.GetType()))
                {
                    return (T)o;
                }
                else if (typeof(T).IsEnum)
                {
                    return (T)Convert.ChangeType(o, Enum.GetUnderlyingType(typeof(T)));
                }
                else
                {
                    return (T)Convert.ChangeType(o, typeof(T));
                }
            }
            catch (Exception ex)
            {
                return (T)o;
            }
        }
    }
}
