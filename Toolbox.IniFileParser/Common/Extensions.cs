// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-03-06 16:16
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Linq;
using System.Text;

namespace Toolbox.IniFileParser.Common
{
    /// <summary>
    ///     A collection of extension methods for
    ///     <see cref="string"/> objects
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        ///     Replaces all <see cref="characters"/> from the
        ///     string.
        /// </summary>
        /// <param name="value">
        ///     The value from which all <paramref name="characters"/>
        ///     are replaced.
        /// </param>
        /// <param name="replacement">
        ///     The value each <paramref name="characters"/>
        ///     value is replaced with
        /// </param>
        /// <param name="characters">
        ///     The strings that should be replaced
        /// </param>
        /// <returns>
        ///     A new string with all characters
        ///     replaced
        /// </returns>
        public static string ReplaceAll(this string value, 
                                        string replacement,
                                        params string[] characters)
        {
            var builder = new StringBuilder(value);

            foreach (var occurrence in characters)
            {
                builder.Replace(occurrence, replacement);
            }

            return builder.ToString().Trim();
        }
        
        /// <summary>
        ///     Removes all <paramref name="characters"/> from
        ///     the start of the string
        /// </summary>
        /// <param name="value">
        ///     The string that should be replaced
        /// </param>
        /// <param name="characters">
        ///     The characters that should be removed
        ///     from the start of the string.
        /// </param>
        /// <returns>
        ///     A new string with all characters
        ///     replaced.
        /// </returns>
        public static string RemoveFromStart(this string value,
                                             params string[] characters)
        {
            foreach (var character in characters)
            {
                var index = value.IndexOf(character, StringComparison.InvariantCulture);

                if (index == -1)
                    continue;

                value = value.Remove(index, character.Length);
            }

            return value.Trim();
        }

        /// <summary>
        ///     Checks if the string starts with any of the
        ///     passed <paramref name="characters"/>
        /// </summary>
        /// <param name="value">
        ///     The value that shall be checked
        /// </param>
        /// <param name="characters">
        ///     The characters whose presence shall be checked
        /// </param>
        /// <returns>
        ///     'true' if the <paramref name="value"/>
        ///     starts with any of the passed
        ///     <paramref name="characters"/>, 'false'
        ///     otherwise.
        /// </returns>
        public static bool StartsWithAny(this string value,
                                         params string[] characters)
        {
            return characters.Any(value.StartsWith);
        }
    }
}