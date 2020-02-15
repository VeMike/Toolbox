// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-10 20:37
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Toolbox.Utils.Common
{
    /// <summary>
    ///     A collection of utility methods, that operate
    ///     on <see cref="string"/>
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        ///     Combines a collection of strings into a URL. This
        ///     basically does the same thing as any of the <see cref="Path.Combine(string,string)"/>
        ///     overloads, but for urls
        /// </summary>
        /// <param name="segments">
        ///     The single parts of thr url, that should be combined
        /// </param>
        /// <returns>
        ///     The passed <paramref name="segments"/> combined as url
        /// </returns>
        public static string UrlCombine(params string[] segments)
        {
            const char URL_CHAR = '/';

            var builder = new StringBuilder();

            foreach (var segment in segments)
            {
                if(string.IsNullOrEmpty(segment))
                    continue;
                
                builder.Append(URL_CHAR);
                builder.Append(segment.Trim(URL_CHAR));
            }

            return builder.ToString();
        }

        /// <summary>
        ///     Removes the first occuring value of <see cref="removeString"/>
        ///     from <paramref name="sourceString"/> and returns the resulting
        ///     <see cref="string"/>
        /// </summary>
        /// <param name="sourceString">
        ///     The string from whom the first occuring <see cref="removeString"/>
        ///     is removed.
        /// </param>
        /// <param name="removeString">
        ///     The string removed from the start of <see cref="sourceString"/>
        /// </param>
        /// <returns>
        ///     A new string with <paramref name="removeString"/> removed
        ///     from the start of <paramref name="sourceString"/>.
        ///
        ///     If <see cref="removeString"/> is null or empty, the passed
        ///     <see cref="sourceString"/> will be returned unchanged.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Thrown is <see cref="sourceString"/> is null or empty
        /// </exception>
        public static string TrimStart(string sourceString, string removeString)
        {
            if(string.IsNullOrEmpty(sourceString))
                throw new ArgumentException($"The '{nameof(sourceString)}' can not be null or empty");

            if (string.IsNullOrEmpty(removeString))
                return sourceString;

            int index = sourceString.IndexOf(removeString,
                                             StringComparison.InvariantCulture);

            return index < 0 ? sourceString : 
                               sourceString.Remove(index, removeString.Length);
        }

        /// <summary>
        ///     Removes the last occuring value of <see cref="removeString"/>
        ///     from <paramref name="sourceString"/> and returns the resulting
        ///     <see cref="string"/>
        /// </summary>
        /// <param name="sourceString">
        ///     The string from whom the last occuring <see cref="removeString"/>
        ///     is removed.
        /// </param>
        /// <param name="removeString">
        ///     The string removed from the end of <see cref="sourceString"/>
        /// </param>
        /// <returns>
        ///     A new string with <paramref name="removeString"/> removed
        ///     from the end of <paramref name="sourceString"/>.
        ///
        ///     If <see cref="removeString"/> is null or empty, the passed
        ///     <see cref="sourceString"/> will be returned unchanged.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Thrown is <see cref="sourceString"/> is null or empty
        /// </exception>
        public static string TrimEnd(string sourceString, string removeString)
        {
            if(string.IsNullOrEmpty(sourceString))
                throw new ArgumentException($"The '{nameof(sourceString)}' can not be null or empty");

            if (string.IsNullOrEmpty(removeString))
                return sourceString;

            int index = sourceString.LastIndexOf(removeString,
                                             StringComparison.InvariantCulture);

            return index < 0 ? sourceString : 
                               sourceString.Remove(index, removeString.Length);
        }
    }
}