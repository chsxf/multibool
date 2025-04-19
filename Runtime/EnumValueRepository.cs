using System;
using System.Collections.Generic;

namespace chsxf
{
    internal static class EnumValueRepository<T> where T : struct, Enum
    {
        private static Dictionary<T, int> values;

        public static int GetIntValue(T _enum) {
            if (values == null) {
                values = new Dictionary<T, int>();
                T[] enumValues = (T[]) Enum.GetValues(typeof(T));
                foreach (T value in enumValues) {
                    values[value] = Convert.ToInt32(value);
                }
            }
            return values[_enum];
        }
    }
}
