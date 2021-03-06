using System;

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     Represents a section of an ini file
    /// </summary>
    public interface ISection : IEquatable<ISection>
    {
        /// <summary>
        ///     The name of the section
        /// </summary>
        string Name { get; }
        
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
        ///     Adds the specified <see cref="IProperty"/>
        ///     to this section.
        /// </summary>
        /// <param name="property"></param>
        IProperty this[IProperty property] { set; }

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
        ///     Gets the number of <see cref="IProperty"/>s of
        ///     this section.
        /// </summary>
        int Properties { get; }
    }
}