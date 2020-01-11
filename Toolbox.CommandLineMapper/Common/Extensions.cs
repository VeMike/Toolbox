// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 23:35
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Toolbox.CommandLineMapper.Common
{
    /// <summary>
    ///     A collection of extension methods for <see cref="string"/>
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        ///     Converts this <see cref="string"/> to a single
        ///     <see cref="char"/> by stripping all it's characters after
        ///     the first one.
        /// </summary>
        /// <param name="value">
        ///     The string converted to a single character
        /// </param>
        /// <returns>
        ///    The first character of the <paramref name="value"/>.
        ///    If the string is empty 'default(char)' is returned.
        /// </returns>
        public static char ToChar(this string value)
        {
            if (value.Length >= 1)
            {
                return value[0];
            }

            return default;
        }

        /// <summary>
        ///     Converts this <see cref="string"/> to a new
        ///     string consisting of a single <see cref="char"/>
        ///     by stripping all it's characters after
        ///     the first one.
        /// </summary>
        ///     The string converted to a single character
        /// <returns>
        ///    The first character of the <paramref name="value"/>.
        ///    If the string is empty <see cref="string.Empty"/> is returned.
        /// </returns>
        public static string ToSingleCharString(this string value)
        {
            return value.ToChar().ToString();
        }
    }
}