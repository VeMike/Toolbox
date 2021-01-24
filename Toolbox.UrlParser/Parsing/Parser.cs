using System;
using System.Linq;
using System.Text.RegularExpressions;
using Com.Toolbox.Utils.Probing;

// ReSharper disable InvalidXmlDocComment

namespace Toolbox.UrlParser.Parsing
{
    /// <summary>
    ///     An implementation of <see cref="IParser"/> that uses
    ///     a defined URL pattern to parse a URL.
    /// </summary>
    public sealed class Parser : IParser
    {
        #region Attributes

        /// <summary>
        ///     The regular expression that matches a single
        ///     query parameter
        /// </summary>
        private static readonly Regex pathParamRegex;

        /// <summary>
        ///     The parsed parameters from the URL pattern
        ///     specified in the constructor
        /// </summary>
        private readonly ParameterList parsedPattern;

        #endregion
        
        #region Constructor

        /// <summary>
        ///     Performs static initialization
        /// </summary>
        static Parser()
        {
            pathParamRegex = new Regex(@"^{(.+)}$");
        }
        
        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="urlPattern">
        ///     The URL pattern used for parsing a URL. This pattern
        ///     just allows to specify path parameters of a URL.
        ///     It is not required to specify a pattern for query
        ///     parameters.
        ///
        ///     e.g.
        ///     'http://127.0.0.1:8000/items/{item_id}'
        ///
        ///     The '{item_id}' in the above defined URL would
        ///     be a path parameter. It would match the following
        ///     URL: 'http://127.0.0.1:8000/items/12'.
        ///
        ///     The parser result would now contain a parameter with
        ///     name 'item_id' a value of '12' and an index of '0'
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is null
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown if the passed string is empty
        /// </exception>
        public Parser(string urlPattern)
        {
            Guard.AgainstNullArgument(nameof(urlPattern), urlPattern);
            Guard.AgainstEmptyString(urlPattern);

            this.parsedPattern = ParsePattern(new Uri(urlPattern));
        }

        #endregion
        
        #region IParser Implementation

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is null
        /// </exception>
        public IUrlParserResult Parse(string url)
        {
            Guard.AgainstNullArgument(nameof(url), url);

            var queryParameters = this.ParseQueryParameters(new Uri(url));
            
            
            return new UrlParserResult(url, queryParameters, new ParameterList());
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Parses the passed URL pattern
        /// </summary>
        /// <param name="urlPattern">
        ///     The URL pattern to parse
        /// </param>
        /// <returns>
        ///     A list of query parameters that are specified in
        ///     the URL parameter.
        /// </returns>
        private static ParameterList ParsePattern(Uri urlPattern)
        {
            var parameterList = new ParameterList();
            var urlSegments = urlPattern.LocalPath.Split('/');
            for(int i = 0; i < urlSegments.Length; i++)
            {
                var match = pathParamRegex.Match(urlSegments[i]);

                if (match.Success)
                {
                    parameterList.Add(new Parameter(match.Groups[1].Value, 
                                                        string.Empty,
                                                        i));
                }
            }

            return parameterList;
        }
        
        /// <summary>
        ///     Parses all query parameters in the URL
        /// </summary>
        /// <param name="url">
        ///     The URL whose query parameters shall be parsed
        /// </param>
        /// <returns>
        ///     The parsed query parameters
        /// </returns>
        private ParameterList ParseQueryParameters(Uri url)
        {
            var result = new ParameterList();
            
            for (int i = 0; i < url.Segments.Length; i++)
            {
                var segment = url.Segments[i].Replace("/", 
                                                           string.Empty);
                
                if(segment.Equals(string.Empty))
                    continue;

                var parameter = this.parsedPattern.FirstOrDefault(p => p.Index == i);
                
                if(parameter == null)
                    continue;

                result.Add(new Parameter(parameter.Name, segment, i));
            }

            return result;
        }

        #endregion
    }
}