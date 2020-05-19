// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-19 19:36
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Toolbox.CommandLineMapper.Core.Wrappers;

namespace Toolbox.CommandLineMapper.Mapper
{
    /// <summary>
    ///     A simple data class, that represents a single mapping
    ///     (key-value pair on the command line) that failed
    /// </summary>
    public class MappingError
    {
        /// <summary>
        ///     The name of the option as it was specified
        ///     on the command line
        /// </summary>
        public string OptionName { get; set; }
        
        /// <summary>
        ///     The value of the option as it was specified
        ///     on the command line
        /// </summary>
        public string OptionValue { get; set; }
        
        /// <summary>
        ///     The <see cref="Exception"/> that caused the mapping to
        ///     fail. This can either be:
        ///         - <see cref="PropertyNotFoundException"/>.
        ///         - <see cref="InvalidCastException"/>
        /// </summary>
        public Exception Cause { get; set; }
    }
}