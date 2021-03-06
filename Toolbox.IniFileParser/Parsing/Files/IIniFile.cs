// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 18:50
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     Represents a single ini file
    /// </summary>
    public interface IIniFile
    {
        /// <summary>
        ///     The <see cref="IParser"/> that parses the
        ///     ini file
        /// </summary>
        IParser Parser { get; }
        
        /// <summary>
        ///     Gets the <see cref="ISection"/>
        ///     with the specified name
        /// </summary>
        /// <param name="name">
        ///     The name of the section
        /// </param>
        ISection this[string name] { get; }
        
        /// <summary>
        ///     Gets the <see cref="ISection"/> at the specified
        ///     index.
        /// </summary>
        /// <param name="index">
        ///     The zero based index of the <see cref="ISection"/>
        /// </param>
        ISection this[int index] { get; }
        
        /// <summary>
        ///     Adds the specified <see cref="ISection"/>
        ///     to the ini file
        /// </summary>
        /// <param name="section">
        ///     The section to add
        /// </param>
        ISection this[ISection section] { set; }

        /// <summary>
        ///     Removes the <see cref="ISection"/> at the
        ///     specified index
        /// </summary>
        /// <param name="index">
        ///     The index of the section
        /// </param>
        void Remove(int index);

        /// <summary>
        ///     Remove the passed <see cref="ISection"/>
        /// </summary>
        /// <param name="section">
        ///     The section to remove
        /// </param>
        void Remove(ISection section);

        /// <summary>
        ///     Gets the amount of <see cref="ISection"/>s
        ///     of this ini file
        /// </summary>
        int Sections { get; }
    }
}