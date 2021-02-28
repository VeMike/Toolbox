// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 15:52
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Toolbox.IniFileParser.Parsing.Events;

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     Parses an INI file.
    /// </summary>
    public interface IParser
    {
        #region Methods

        /// <summary>
        ///     Parses the passed text file 
        /// </summary>
        /// <param name="file">
        ///     The file that should be parsed
        /// </param>
        void Parse(ITextFile file);
        
        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets options for the parser
        /// </summary>
        ParserOptions Options { get; set; }

        #endregion

        #region Events

        /// <summary>
        ///     This event is raised whenever the
        ///     parser encounters an ini file section.
        ///
        ///     The value of <see cref="ContentEventArgs.Content"/>
        ///     will contain the value of the section. The value
        ///     will not contain the surrounding '[]' of the
        ///     section.
        /// </summary>
        event EventHandler<ContentEventArgs> Section;

        /// <summary>
        ///     This event is raised whenever the
        ///     parser encounters a comment in
        ///     the ini file.
        ///
        ///     The value of <see cref="ContentEventArgs.Content"/>
        ///     will contain the value of the comment. The
        ///     value will not contain the character that
        ///     indicates the comment.
        /// </summary>
        event EventHandler<ContentEventArgs> Comment;

        /// <summary>
        ///     This event is raised whenever the
        ///     parser encounters a property in the
        ///     ini file.
        ///
        ///     The value of <see cref="PropertyEventArgs.Property"/>
        ///     will contain the value of the property.
        /// </summary>
        event EventHandler<PropertyEventArgs> Property;

        #endregion
    }
}