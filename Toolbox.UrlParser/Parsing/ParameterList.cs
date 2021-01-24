// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-01-24 13:21
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;

namespace Toolbox.UrlParser.Parsing
{
    /// <summary>
    ///     A list of parameters that were parsed from a URL
    /// </summary>
    public sealed class ParameterList : List<Parameter>
    {
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class.
        ///     The list is initially empty
        /// </summary>
        public ParameterList()
        {
            
        }
        
        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="parameters">
        ///     The parameters included in the list
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed collection is 'null'
        /// </exception>
        public ParameterList(IEnumerable<Parameter> parameters) : base(parameters)
        {
        }

        #endregion
    }
}