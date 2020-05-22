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
        public static IEnumerable<string> SimpleSplitArguments(this string value, 
                                                               string splitChar = " ", 
                                                               int skip = 0)
        {
            if(string.IsNullOrEmpty(splitChar))
                throw new ArgumentException($"'{nameof(splitChar)}' can not be null or empty");
            
            return value.Split(new[] {splitChar}, StringSplitOptions.RemoveEmptyEntries).Skip(skip);
        }
    }

    /// <summary>
    ///     A collection of extension methods for enumerations
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Creates single instances of <see cref="Argument"/> from
        ///     the passed collection of strings
        /// </summary>
        /// <param name="args">
        ///     The list of single arguments
        /// </param>
        /// <param name="argumentPrefix">
        ///    The prefix of a command (e.g. '-')
        /// </param>
        /// <returns>
        ///    A collection of new <see cref="Argument"/>
        /// </returns>
        public static IEnumerable<Argument> ToArgument(this IEnumerable<string> args, string argumentPrefix)
        {
            if(string.IsNullOrEmpty(argumentPrefix))
                throw new ArgumentException($"The '{argumentPrefix}' can not be null or empty");
            
            var argumentList = args.ToList();
            
            for (int i = 0; i < argumentList.Count;)
            {
                var command = argumentList[i];
                string value = null;

                /*
                 * This means we have reached the last iteration. Here, the last element must be a
                 * command without a value
                 */
                if (i != argumentList.Count - 1)
                {
                    value = argumentList[i + 1];
                }
                
                /*
                 * If the prefix starts with the command prefix, we have encountered
                 * the next command. Therefore the current command does not have a
                 * value. It is just defined by its presence.
                 */
                if (value?.StartsWith(argumentPrefix) ?? true)
                {
                    i++;
                    yield return new Argument(argumentPrefix, command);
                }
                else
                {
                    i += 2;
                    yield return new Argument(argumentPrefix, command, value);
                }
            }
        }
    }
}