// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-02-15 13:40
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;

namespace Toolbox.CommandLineMapper.Mapper
{
    /// <summary>
    ///     Provides some options to configure the behaviour of
    ///     a <see cref="ICommandLineMapper"/>s mapping. 
    /// </summary>
    public class MapperOptions
    {
        /// <summary>
        ///     The prefix used for command line option.
        ///
        ///     e.g.:
        ///     '-myOption theValue -otherOption value', where the prefix
        ///     would be '-'.
        ///
        ///     The default value is '-'
        /// </summary>
        public string OptionPrefix { get; set; } = "-";

        /// <summary>
        ///     Indicates if the mapper should continue
        ///     mapping an object if one of its properties
        ///     can not be mapped to any of the command
        ///     line arguments. 
        ///
        ///     Default: 'false'
        /// </summary>
        public bool ContinueOnError { get; set; } = false;

        /// <summary>
        ///     Checks if all applied option values are valid. Those
        ///     checks include:
        ///
        ///     - <see cref="OptionPrefix"/> can not be null or empty
        /// </summary>
        /// <exception cref="OptionsException">
        ///     Thrown if any of the options are not valid
        /// </exception>
        public void ValidateOptions()
        {
            if (string.IsNullOrEmpty(this.OptionPrefix))
            {
                throw new OptionsException($"The value of '{nameof(this.OptionPrefix)}' is null or empty",
                                           nameof(this.OptionPrefix));
            }
        }
    }
}