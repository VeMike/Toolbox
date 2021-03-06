using System;

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     Represents a property of an ini file
    /// </summary>
    public interface IProperty : IEquatable<IProperty>
    {
        /// <summary>
        ///     The name of he property
        /// </summary>
        string Name { get; }
        
        /// <summary>
        ///     The value of the property
        /// </summary>
        string Value { get; }
    }
}