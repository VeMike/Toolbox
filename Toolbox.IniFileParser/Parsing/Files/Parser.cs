// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-03-06 14:57
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Toolbox.IniFileParser.Common;
using Toolbox.IniFileParser.Parsing.Events;

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     An implementation of <see cref="IParser"/>
    /// </summary>
    public class Parser : IParser
    {
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class.
        ///
        ///     <see cref="Options"/> will be initialized
        ///     using the default instance of <see cref="ParserOptions"/>
        /// </summary>
        public Parser() : this(new ParserOptions())
        {
        }

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="options">
        ///     The options of the parser
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is null
        /// </exception>
        public Parser(ParserOptions options)
        {
            this.Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        #endregion

        #region IParser Implementation

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is
        ///     full
        /// </exception>
        public void Parse(ITextFile file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            this.ParseLineByLine(file.ReadLines());
        }

        /// <inheritdoc />
        public ParserOptions Options { get; }

        /// <inheritdoc />
        public event EventHandler<ContentEventArgs> Section;

        /// <inheritdoc />
        public event EventHandler<ContentEventArgs> Comment;

        /// <inheritdoc />
        public event EventHandler<PropertyEventArgs> Property;

        #endregion

        #region Methods

        /// <summary>
        ///     Parses the individual lines of a file
        /// </summary>
        /// <param name="lines">
        ///     The lines of the file
        /// </param>
        private void ParseLineByLine(IEnumerable<Line> lines)
        {
            foreach (var line in lines)
            {
                this.RaiseIfLineIsSection(line);
                this.RaiseIfLineIsComment(line);
                this.RaiseIfLineIsProperty(line);
            }
        }

        /// <summary>
        ///     Raises the <see cref="Section"/> event
        ///     if the passed line is an ini file section
        /// </summary>
        /// <param name="line">
        ///     A line of the ini file
        /// </param>
        private void RaiseIfLineIsSection(Line line)
        {
            if (line.Content.StartsWith(this.Options.SectionStart) &&
                line.Content.EndsWith(this.Options.SectionEnd))
            {
                var section = line.Content.ReplaceAll(string.Empty,
                                                      this.Options.SectionStart,
                                                      this.Options.SectionEnd);

                this.Section?.Invoke(this, new ContentEventArgs(line.Number,
                                                                line.Content,
                                                                section));
            }
        }

        /// <summary>
        ///     Raises the <see cref="Comment"/> event
        ///     if the passed line is a comment line
        /// </summary>
        /// <param name="line">
        ///     A line of the ini file
        /// </param>
        private void RaiseIfLineIsComment(Line line)
        {
            if (line.Content.StartsWithAny(this.Options.CommentChars.ToArray()))
            {
                var comment = line.Content.RemoveFromStart(this.Options.CommentChars.ToArray());
                
                this.Comment?.Invoke(this, new ContentEventArgs(line.Number,
                                                                line.Content,
                                                                comment));
            }
        }

        /// <summary>
        ///     Raises the <see cref="Property"/> event
        ///     if the passed line is a property
        /// </summary>
        /// <param name="line">
        ///     The line of the ini file
        /// </param>
        private void RaiseIfLineIsProperty(Line line)
        {
            if (line.Content.Contains(this.Options.PropertySeparator))
            {
                this.Property?.Invoke(this, new PropertyEventArgs(line.Number,
                                                                  line.Content,
                                                                  this.CreateProperty(line.Content)));
            }
        }

        /// <summary>
        ///     Creates a new instance of <see cref="KeyValuePair{TKey,TValue}"/>
        ///     from the passed <paramref name="rawLine"/> of the ini
        ///     file
        /// </summary>
        /// <param name="rawLine">
        ///     The raw line of an ini file
        /// </param>
        /// <returns>
        ///     A new instance of <see cref="KeyValuePair{TKey,TValue}"/>
        ///     that represents a single ini file property
        /// </returns>
        private KeyValuePair<string, string> CreateProperty(string rawLine)
        {
            var parts = rawLine.Split(new []{this.Options.PropertySeparator},
                                      StringSplitOptions.RemoveEmptyEntries);

            return parts.Length switch
            {
                /*
                 * The first index of the array is the key,
                 * the second index is the value. If the array
                 * does not have a second index, the value
                 * of the key is just empty
                 */
                >= 2 => new KeyValuePair<string, string>(parts[0].Trim(), parts[1].Trim()),
                1 => new KeyValuePair<string, string>(parts[0].Trim(), string.Empty),
                _ => new KeyValuePair<string, string>(string.Empty, string.Empty)
            };
        }

        #endregion
    }
}