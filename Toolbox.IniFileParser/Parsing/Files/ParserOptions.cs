// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 17:50
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Collections.Generic;

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     Contains options for a parser.
    /// </summary>
    public sealed class ParserOptions
    {
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        public ParserOptions()
        {
            this.CommentChars = new List<string>
            {
                ";",
                "#",
                "//"
            };
        }

        #endregion

        #region Properties

        /// <summary>
        ///     A collection of characters that indicate
        ///     comment line.
        ///
        ///     The default characters that indicate comments
        ///     are:
        ///         - ';'
        ///         - '#'
        ///         - '//'
        /// </summary>
        public IList<string> CommentChars { get; }

        /// <summary>
        ///     The character that indicates a section start
        /// </summary>
        public string SectionStart { get; } = "[";

        /// <summary>
        ///     The character that indicates a section end
        /// </summary>
        public string SectionEnd { get; } = "]";

        /// <summary>
        ///     The character that indicates the
        ///     delimiter of ini properties
        /// </summary>
        public string PropertySeparator { get; } = "=";

        /// <summary>
        ///     Tells the parser if comments should
        ///     be skipped. If this is set to 'true'
        ///     no events will be raised if the
        ///     parser encounters a comment line.
        ///
        ///     Default: 'true'
        /// </summary>
        public bool SkipComments { get; } = true;

        #endregion
    }
}