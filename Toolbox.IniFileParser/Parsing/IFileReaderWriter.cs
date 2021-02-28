// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 18:53
// ===================================================================================================
// = Description :
// ===================================================================================================

using Toolbox.IniFileParser.Parsing.Files;

namespace Toolbox.IniFileParser.Parsing
{
    /// <summary>
    ///     Reads and writes ini files
    /// </summary>
    public interface IFileReaderWriter
    {
        #region Methods

        /// <summary>
        ///     Parses the contents of an ini
        ///     file to an instance of <see cref="IIniFile"/>
        /// </summary>
        /// <param name="txtFile">
        ///     The text file that should be
        ///     parsed
        /// </param>
        /// <returns>
        ///     A new instance of <see cref="IIniFile"/>
        /// </returns>
        IIniFile Read(ITextFile txtFile);

        /// <summary>
        ///     Writes the contents of the
        ///     passed ini file to a text
        ///     file.
        /// </summary>
        /// <param name="iniFile">
        ///     The ini file that should be written
        ///     to a text file.
        /// </param>
        /// <returns>
        ///     A new text file.
        /// </returns>
        ITextFile Write(IIniFile iniFile);

        #endregion
    }
}