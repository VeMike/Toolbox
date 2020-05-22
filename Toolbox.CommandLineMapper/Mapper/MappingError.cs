// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-19 19:36
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Toolbox.CommandLineMapper.Common;
using Toolbox.CommandLineMapper.Core.Property;

namespace Toolbox.CommandLineMapper.Mapper
{
    /// <summary>
    ///     A simple data class, that represents a single mapping
    ///     (key-value pair on the command line) that failed
    /// </summary>
    public class MappingError
    {
        /// <summary>
        ///     The argument that failed to be mapped to
        ///     an object
        /// </summary>
        public Argument Argument { get; set; }
        
        /// <summary>
        ///     The <see cref="Exception"/> that caused the mapping to
        ///     fail. This can either be:
        ///         - <see cref="PropertyNotFoundException"/>.
        ///         - <see cref="InvalidCastException"/>
        /// </summary>
        public Exception Cause { get; set; }
    }
}