// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-01-24 12:43
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Toolbox.UrlParser.Parsing
{
    /// <summary>
    ///     The result of <see cref="IParser.Parse"/>
    /// </summary>
    public interface IUrlParserResult
    {
        #region Properties

        /// <summary>
        ///     The original URL that was passed to
        ///     the parser
        /// </summary>
        string OriginalUrl { get; }
        
        /// <summary>
        ///     The path parameters of the URL
        /// </summary>
        ParameterList PathParameters { get; }
        
        /// <summary>
        ///     The query parameters of the URL
        /// </summary>
        ParameterList QueryParameters { get; }

        #endregion
    }
}