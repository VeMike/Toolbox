// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 18:48
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Collections.Generic;

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     Represents a plain text file 
    /// </summary>
    public interface ITextFile
    {
        #region Methods

        /// <summary>
        ///     Reads all contents of the file
        ///     line by line
        /// </summary>
        /// <returns>
        ///     A collection of all lines of
        ///     the file
        /// </returns>
        IEnumerable<Line> ReadLines();

        /// <summary>
        ///     Writes the passed lines to a
        ///     file
        /// </summary>
        /// <param name="lines">
        ///     The lines that should be written.
        /// </param>
        void WriteLines(IEnumerable<Line> lines);

        #endregion
    }
}