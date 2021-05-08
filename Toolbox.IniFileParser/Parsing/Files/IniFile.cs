// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-03-13 16:38
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Text;
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
        private readonly List<ISection> sections = new();

        /// <summary>
        ///     The current section parsed by the <see cref="Parser"/>.
        ///
        ///     This is always 'null' if the ini file is created
        ///     manually without parsing (default constructor).
        /// </summary>
        private string currentSection;
        
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
        public int Sections => this.sections.Count;

        /// <inheritdoc />
        public ISection this[string name]
        {
            get
            {
                if (this.Contains(name))
                    return this.sections.Find(s => s.Name.Equals(name));
                throw new ArgumentException($"A section with name '{name}' not found");
            }
        }

        /// <inheritdoc />
        public ISection this[int index]
        {
            get
            {
                this.CheckIndex(index);

                return this.sections[index];
            }
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is null
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown if the passed section is already in
        ///     the ini file.
        /// </exception>
        public void Add(ISection section)
        {
            if (section is null)
                throw new ArgumentNullException(nameof(section));
            if (this.Contains(section.Name))
                throw new ArgumentException($"The section '{section.Name}' is already in the ini file");

            this.sections.Add(section);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if there is no section at the passed index
        /// </exception>
        public void Remove(int index)
        {
            this.CheckIndex(index);

            this.sections.RemoveAt(index);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is null
        /// </exception>
        public void Remove(ISection section)
        {
            if (section is null)
                throw new ArgumentNullException(nameof(section));

            this.sections.Remove(section);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is null
        /// </exception>
        public bool Contains(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return !(this.sections.Find(s => s.Name.Equals(name)) is null);
        }

        /// <inheritdoc />
        public string ToString(string sectionStart, 
                               string sectionEnd, 
                               string propertySeparator)
        {
            var builder = new StringBuilder();

            foreach (var section in this.sections)
            {
                builder.Append(section.ToString(sectionStart,
                                                sectionEnd,
                                                propertySeparator));
            }

            return builder.ToString();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ToString("[", "]", "=");
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Checks if the passed index is valid
        /// </summary>
        /// <param name="index">
        ///     The index to check
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if the passed index is out of range
        /// </exception>
        private void CheckIndex(int index)
        {
            if (index >= this.Sections || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
        }
        
        /// <summary>
        ///     Adds all event handlers to the parser
        /// </summary>
        private void RegisterEventHandlers()
        {
            this.Parser.Section += this.OnSection;
            this.Parser.Property += this.OnProperty;
        }

        /// <summary>
        ///     Called whenever the <see cref="Parser"/> encounters
        ///     a new property inside a section in the ini file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if a property in a ini file is
        ///     encountered without a prior section
        /// </exception>
        private void OnProperty(object sender, PropertyEventArgs e)
        {
            /*
             * If 'currentSection' is 'null' here, the parser
             * encountered a property before a section. An
             * ini file that has properties without a section
             * is not valid.
             */
            if (this.currentSection is null)
                throw new InvalidOperationException($"Section for property '{e.Property}' not found");
            
            this[this.currentSection].Add(new Property(e.Property.Key, e.Property.Value));
        }

        /// <summary>
        ///     Called whenever the <see cref="Parser"/> encounters
        ///     a new section in the ini file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSection(object sender, ContentEventArgs e)
        {
            this.currentSection = e.Content;
            this.sections.Add(new Section(e.Content));
        }

        #endregion
    }
}