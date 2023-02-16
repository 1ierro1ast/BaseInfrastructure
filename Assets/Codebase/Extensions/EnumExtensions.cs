using System;

namespace Codebase.Extensions
{
    public static class EnumExtensions
    {
        public static T GetNext<T>(this T enumValue) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            T[] values = (T[])Enum.GetValues(enumValue.GetType());
            int j = Array.IndexOf(values, enumValue) + 1;
            return values.Length == j ? values[0] : values[j];
        }

        public static T GetPrevious<T>(this T enumValue) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            T[] values = (T[])Enum.GetValues(enumValue.GetType());
            int j = Array.IndexOf(values, enumValue) - 1;
            return values.Length == j ? values[0] : values[j];
        }
    }
}