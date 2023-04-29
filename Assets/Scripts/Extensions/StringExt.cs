using System;

namespace Extensions
{
    public static class StringExt
    {
        public static T ConvertToEnum<T>(this string text) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), text, true);
        }
    }
}