// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-01-24 13:53
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Com.Toolbox.Utils.Probing;

namespace Toolbox.UrlParser.Parsing
{
    /// <summary>
    ///     The result of the <see cref="IParser"/>
    /// </summary>
    public sealed class UrlParserResult : IUrlParserResult
    {
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="originalUrl">
        ///     The URL passed to the URL parser
        /// </param>
        /// <param name="pathParameters">
        ///     The path parameters included in the URL
        /// </param>
        /// <param name="queryParameters">
        ///     The query parameters included in the URL
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if any of the passed arguments
        ///     is 'null'
        /// </exception>
        public UrlParserResult(string originalUrl, ParameterList pathParameters, ParameterList queryParameters)
        {
            Guard.AgainstNullArgument(nameof(originalUrl), originalUrl);
            Guard.AgainstNullArgument(nameof(pathParameters), pathParameters);
            Guard.AgainstNullArgument(nameof(queryParameters), queryParameters);
            
            this.OriginalUrl = originalUrl;
            this.PathParameters = pathParameters;
            this.QueryParameters = queryParameters;
        }

        #endregion
        
        #region IUrlParserResult Implementation

        /// <inheritdoc />
        public string OriginalUrl { get; }

        /// <inheritdoc />
        public ParameterList PathParameters { get; }

        /// <inheritdoc />
        public ParameterList QueryParameters { get; }

        #endregion
    }
}