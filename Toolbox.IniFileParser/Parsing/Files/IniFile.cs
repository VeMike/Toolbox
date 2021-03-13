// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-03-13 16:38
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using Toolbox.IniFileParser.Parsing.Events;

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     An implementation of <see cref="IIniFile"/>
    /// </summary>
    public class IniFile : IIniFile
    {
        #region Attributes

        /// <summary>
        ///     The sections of this ini file.
        /// </summary>
        private readonly IList<ISection> sections = new List<ISection>();

        #endregion
        
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class.
        ///
        ///     Use this constructor whenever a new
        ///     ini file should be created with no
        ///     initial content.
        /// </summary>
        public IniFile()
        {
            this.Parser = new Parser();
        }

        /// <summary>
        ///     Creates a new instance of the class.
        ///
        ///     Use this constructor whenever an ini file
        ///     should be parsed from an existing ini file.
        ///
        ///     The default <see cref="Toolbox.IniFileParser.Parsing.Files.Parser"/>
        ///     will be used to parse the passed file
        /// </summary>
        /// <param name="file">
        ///     The text file that should be parsed into
        ///     an ini file
        /// </param>
        public IniFile(ITextFile file) : this(new Parser(),
                                              file)
        {
            
        }
        
        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="parser">
        ///     The parser that reads an ini file
        /// </param>
        /// <param name="file">
        ///     The text file that should be parsed into
        ///     an ini file
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if any of the passed arguments is null
        /// </exception>
        public IniFile(IParser parser, ITextFile file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            
            this.Parser = parser ?? throw new ArgumentNullException(nameof(parser));

            this.RegisterEventHandlers();
            
            this.Parser.Parse(file);
        }

        #endregion
        
        #region IIniFIle Implementation

        /// <inheritdoc />
        public IParser Parser { get; }

        /// <inheritdoc />
        public int Sections { get; }

        /// <inheritdoc />
        public ISection this[string name] => throw new NotImplementedException();

        /// <inheritdoc />
        public ISection this[int index] => throw new NotImplementedException();

        /// <inheritdoc />
        public void Add(ISection section)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Remove(int index)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Remove(ISection section)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Contains(string name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string ToString(string sectionStart, string sectionEnd, string propertySeparator)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds all event handlers to the parser
        /// </summary>
        private void RegisterEventHandlers()
        {
            this.Parser.Section += this.OnSection;
            this.Parser.Property += this.OnProperty;
            //this.Parser.Comment += this.OnComment;
        }

        /// <summary>
        ///     Called whenever the <see cref="Parser"/> encounters
        ///     a new comment in the ini file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnComment(object sender, ContentEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Called whenever the <see cref="Parser"/> encounters
        ///     a new property inside a section in the ini file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnProperty(object sender, PropertyEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Called whenever the <see cref="Parser"/> encounters
        ///     a new section in the ini file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSection(object sender, ContentEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}