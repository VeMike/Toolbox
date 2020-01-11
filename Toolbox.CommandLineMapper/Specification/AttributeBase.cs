// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 18:45
// ===================================================================================================
// = Description :
// ===================================================================================================


using System;

namespace Toolbox.CommandLineMapper.Specification
{
    /// <summary>
    ///     Defines the base attribute to defines command line options
    /// </summary>
    public class AttributeBase : Attribute
    {
        #region Constructor

        /// <summary>
        ///     Default initialization of properties
        /// </summary>
        public AttributeBase()
        {
            this.HelpText = string.Empty;
            this.IsRequired = false;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the help text for a command line option.
        /// </summary>
        public string HelpText { get; set; }

        /// <summary>
        ///     Gets or sets the default value for a command line
        ///     option.
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating, if the option
        ///     is required.
        /// </summary>
        public bool IsRequired { get; set; }

        #endregion
    }
}