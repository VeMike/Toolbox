using System;

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     Represents a section of an ini file
    /// </summary>
    public interface ISection
    {
        /// <summary>
        ///     The name of the section
        /// </summary>
        string Name { get; }
        
        /// <summary>
        ///     Gets the number of <see cref="IProperty"/>s of
        ///     this section.
        /// </summary>
        int Properties { get; }
        
        /// <summary>
        ///     Gets the <see cref="IProperty"/> with
        ///     the passed name
        /// </summary>
        /// <param name="name">
        ///     The name of the property
        /// </param>
        IProperty this[string name] { get; }
        
        /// <summary>
        ///     Gets the <see cref="IProperty"/> at the
        ///     passed index
        /// </summary>
        /// <param name="index">
        ///     The index of the property
        /// </param>
        IProperty this[int index] { get; }

        /// <summary>
        ///     Adds the passed property to the
        ///     <see cref="ISection"/>
        /// </summary>
        /// <param name="property">
        ///     The property to add
        /// </param>
        void Add(IProperty property);

        /// <summary>
        ///     Removes the <see cref="IProperty"/> at the
        ///     specified index
        /// </summary>
        /// <param name="index">
        ///     The index of the property
        /// </param>
        void Remove(int index);

        /// <summary>
        ///     Remove the passed <see cref="IProperty"/>
        /// </summary>
        /// <param name="property">
        ///     The property to remove
        /// </param>
        void Remove(IProperty property);

        /// <summary>
        ///     Checks if this section contains a
        ///     property with the passed name
        /// </summary>
        /// <param name="name">
        ///     The name of the property
        /// </param>
        /// <returns>
        ///     'true' if this section contains a
        ///     property with the passed name,
        ///     'false' otherwise.
        /// </returns>
        bool Contains(string name);

        /// <summary>
        ///     Creates a 'ToString' representation of
        ///     this instance
        /// </summary>
        /// <param name="sectionStart">
        ///     The start character of the section
        /// </param>
        /// <param name="sectionEnd">
        ///     The end character of the section
        /// </param>
        /// <param name="propertySeparator">
        ///     The property separator
        /// </param>
        /// <returns>
        ///     A string representation of this instance
        /// </returns>
        string ToString(string sectionStart, 
                        string sectionEnd,
                        string propertySeparator);
    }
}