// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-10 20:37
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.IO;
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
    }
}