// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-01-24 12:41
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Toolbox.UrlParser.Parsing
{
    /// <summary>
    ///     Parses a URL.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        ///     Parses the passed URL string
        /// </summary>
        /// <param name="url">
        ///     The URL that should be parsed
        /// </param>
        /// <returns>
        ///     The result of the parse operation
        /// </returns>
        IUrlParserResult Parse(string url);
    }
}