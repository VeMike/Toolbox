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

        #region Methods

        /// <summary>
        ///     Gets a <see cref="Parameter"/> by the index assigned
        ///     to the parameter. Note, that 'index' does not refer
        ///     to the index of the parameter inside this list.
        ///     The 'index' here is the <see cref="Parameter.Index"/> 
        /// </summary>
        /// <param name="index">
        ///     The index for which a parameter shall be
        ///     received.
        /// </param>
        /// <param name="parameter">
        ///     Receives the parameter with index <paramref name="index"/>
        /// </param>
        /// <returns>
        ///     'true' if a parameter with the passed index is
        ///     found, 'false' if not.
        /// </returns>
        public bool TryGetParameter(int index, out Parameter parameter)
        {
            if (this.Find(p => p.Index == index) is { } param)
            {
                parameter = param;
                return true;
            }

            parameter = null;
            return false;
        }

        #endregion
    }
}