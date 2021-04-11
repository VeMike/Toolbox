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
        #region Properties

        /// <summary>
        ///     Gets or sets the help text for a command line option.
        /// </summary>
        public string HelpText { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the default value for a command line
        ///     option.
        /// </summary>
        public string Default { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets a value indicating, if the option
        ///     is required.
        /// </summary>
        public bool IsRequired { get; set; } = false;

        /// <summary>
        ///     Gets or sets the short name of a command line
        ///     option. This is just a single character.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        ///     Gets or sets the long name of a command line
        ///     option. This should be just a single word.
        /// </summary>
        public string LongName { get; set; }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(this.HelpText)}: {this.HelpText}, {nameof(this.Default)}: {this.Default}, {nameof(this.IsRequired)}: {this.IsRequired}, {nameof(this.ShortName)}: {this.ShortName}, {nameof(this.LongName)}: {this.LongName}";
        }
    }
}