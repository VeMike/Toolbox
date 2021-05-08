using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Toolbox.Utils.Probing;

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
        ///     The parsed parameters from the URL pattern
        ///     specified in the constructor
        /// </summary>
        private readonly ParameterList parsedPattern;

        /// <summary>
        ///     The individual segments of the pattern passed
        ///     to the constructor.
        /// </summary>
        private IList<string> patternSegments;

        #endregion
        
        #region Constructor

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
        ///     '/items/{item_id}'
        ///
        ///     The '{item_id}' in the above defined URL would
        ///     be a path parameter. It would match the following
        ///     URL: '/items/12'.
        ///
        ///     The parser result would now contain a parameter with
        ///     name 'item_id' a value of '12' and an index of '0'.
        ///
        ///     The host part shall not be specified.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is null
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown if the passed string is empty
        /// </exception>
        /// <exception cref="UriFormatException">
        ///     Thrown if the host part of the URL is specified.
        /// 
        ///     The URL: 'http://localhost/items/{item_id}' would
        ///     cause the exception.
        ///
        ///     The relative URL /items/{item_id} would not cause
        ///     the exception.
        /// </exception>
        public Parser(string urlPattern)
        {
            Guard.AgainstNullArgument(nameof(urlPattern), urlPattern);
            Guard.AgainstEmptyString(urlPattern);

            this.parsedPattern = this.ParsePattern(new Uri(urlPattern, UriKind.Relative).OriginalString);
        }

        #endregion
        
        #region IParser Implementation

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is null
        /// </exception>
        /// <exception cref="UriFormatException">
        ///     Thrown if the host part of the URL is specified.
        /// 
        ///     The URL: 'http://localhost/items/{item_id}' would
        ///     cause the exception.
        ///
        ///     The relative URL /items/{item_id} would not cause
        ///     the exception.
        /// </exception>
        public IUrlParserResult Parse(string url)
        {
            Guard.AgainstNullArgument(nameof(url), url);

            var queryParameters = this.ParsePathParameters(new Uri(url, UriKind.Relative).OriginalString);
            
            return new UrlParserResult(url, queryParameters, ParseQueryParameters(url));
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
        private ParameterList ParsePattern(string urlPattern)
        {
            var parameterList = new ParameterList();
            this.patternSegments = urlPattern.Split('/');
            for(var i = 0; i < this.patternSegments.Count; i++)
            {
                if (IsPatternSegment(this.patternSegments[i]))
                {
                    var paramName = this.patternSegments[i].Replace("{", "").Replace("}", "");
                    
                    parameterList.Add(new Parameter(paramName, 
                                                        string.Empty,
                                                        i));
                }
            }

            return parameterList;
        }
        
        /// <summary>
        ///     Parses all path parameters in the URL
        /// </summary>
        /// <param name="url">
        ///     The URL whose query parameters shall be parsed
        /// </param>
        /// <returns>
        ///     The parsed path parameters
        /// </returns>
        private ParameterList ParsePathParameters(string url)
        {
            var result = new ParameterList();

            //The user did not specify any pattern in the constructor
            if (this.parsedPattern.Count == 0)
                return result;
            
            var urlSegments = url.Split('/');

            this.CheckIfPatternSegmentsMatchUrl(urlSegments);
            
            for (var i = 0; i < urlSegments.Length; i++)
            {
                var segment = urlSegments[i].Replace("/", 
                                                     string.Empty);
                
                if(segment.Equals(string.Empty))
                    continue;

                if (this.parsedPattern.TryGetParameter(i, out var param))
                {
                    result.Add(new Parameter(param.Name, segment, i));
                }
            }

            return result;
        }

        /// <summary>
        ///     Parses all query parameters in the URL
        /// </summary>
        /// <param name="url">
        ///     The URL from whose query parameters shall be extracted.
        /// </param>
        /// <returns>
        ///     The query parameters of the URL
        /// </returns>
        private static ParameterList ParseQueryParameters(string url)
        {
            var result = new ParameterList();
            var querySplit = url.Split('?');
            
            //There are at least two parts now if the url has a query
            if (querySplit.Length < 2)
                return result;

            var queryParams = querySplit[1].Split('&');

            for (var i = 0; i < queryParams.Length; i++)
            {
                var queryParam = queryParams[i].Split('=');

                if (queryParam.Length != 2)
                    continue;
                
                result.Add(new Parameter(queryParam[0], queryParam[1], i));
            }

            return result;
        }

        /// <summary>
        ///     Checks if the URL passed to <see cref="Parse"/> matches the
        ///     pattern that was passed to the constructor of the class
        /// </summary>
        /// <param name="urlSegments">
        ///     The segments of the URL passed to <see cref="Parse"/>
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Thrown if the URL passed to <see cref="Parse"/> does not
        ///     match the URL passed to the constructor
        /// </exception>
        private void CheckIfPatternSegmentsMatchUrl(string[] urlSegments)
        {
            if (this.patternSegments.Count != urlSegments.Length)
            {
                throw new
                        ArgumentException($"The number of segments of the pattern ('{this.patternSegments.Count}') does not match the segments of the URL passed to 'Parse' ('{urlSegments.Length}')");
            }

            for (var i = 0; i < urlSegments.Length; i++)
            {
                //The URL segment is a parameter, we just ignore that
                if(IsPatternSegment(this.patternSegments[i]))
                    continue;

                if (!urlSegments[i].Equals(this.patternSegments[i]))
                {
                    throw new
                            ArgumentException($"The URL segments at index '{i}' do not match. The URL passed to 'Parse' does not match the URL pattern of this class");
                }
            }
        }

        /// <summary>
        ///     Checks if the passed segment is a pattern
        ///     segment. A pattern segment starts with '{'
        ///     and ends with '}'
        /// </summary>
        /// <param name="segment">
        ///     The segment to check
        /// </param>
        /// <returns>
        ///     'True' if the segment is a pattern segment, 'False'
        ///     if not.
        /// </returns>
        private static bool IsPatternSegment(string segment)
        {
            return segment.StartsWith("{") && segment.EndsWith("}");
        }

        #endregion
    }
}