namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     Represents a property of an ini file
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        ///     The name of he property
        /// </summary>
        string Name { get; }
        
        /// <summary>
        ///     The value of the property
        /// </summary>
        string Value { get; }

        /// <summary>
        ///     Creates a string representation of
        ///     this instance
        /// </summary>
        /// <param name="separator">
        ///     The separator of the property
        /// </param>
        /// <returns>
        ///     A string representation of this
        ///     instance
        /// </returns>
        string ToString(string separator);
    }
}