// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 23:35
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace Toolbox.CommandLineMapper.Common
{
    /// <summary>
    ///     A collection of extension methods for <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Splits a single string with command line arguments
        ///     into single parts with a single argument
        ///     e.g.
        ///         Pass: '-p SomeOptionValue -a AnotherValue'
        ///         Result: ['-p', 'SomeOptionValue', '-a', 'AnotherValue']
        /// </summary>
        /// <param name="value">
        ///    The single string that should be splitted
        /// </param>
        /// <param name="splitChar">
        ///    The character used to split <paramref name="value"/>
        /// </param>
        /// <param name="skip">
        ///    The amount of characters that should be skipped
        ///    from the resulting enumeration. 
        /// </param>
        /// <returns>
        ///    The single parts of the passed <paramref name="value"/>
        ///    split at <paramref name="splitChar"/>
        /// </returns>
        public static IEnumerable<string> SplitArguments(this string value, 
                                                         string splitChar = " ", 
                                                         int skip = 0)
        {
            if(string.IsNullOrEmpty(splitChar))
                throw new ArgumentException($"'{nameof(splitChar)}' can not be null or empty");
            
            return value.Split(new[] {splitChar}, StringSplitOptions.RemoveEmptyEntries).Skip(skip);
        }
    }
}