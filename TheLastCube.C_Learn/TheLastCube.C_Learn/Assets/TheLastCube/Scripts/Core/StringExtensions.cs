using System;

namespace Core.StringExtensions
{
    public static class StringExtensions
    {
        //string을 enum으로 변환 함수
        public static T StringToEnum<T>(this string value) where T : struct
        {
            if (Enum.TryParse<T>(value, true, out var enumValue))
            {
                return enumValue;
            }
            else
            {
                throw new ArgumentNullException(nameof(value));
            }
        }
    }
}
