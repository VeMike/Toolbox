// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 20:02
// ===================================================================================================
// = Description : 
// ===================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     Wraps around any <see cref="object"/> that has
    ///     <see cref="IAssignableProperty{TAttribute}"/>s.
    ///
    ///     When enumerated, gets all the names of properties
    ///     currently held by this instance
    /// </summary>
    internal interface IPropertyContainer<TAttribute> : IEnumerable<string> where TAttribute : Attribute
    {
        #region Properties

        /// <summary>
        ///     The <see cref="object"/> whose properties are
        ///     inside container instance
        /// </summary>
        object Source { get; }
        
        /// <summary>
        ///     Gets the amount of properties this
        ///     container holds
        /// </summary>
        int Properties { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets a <see cref="IAssignableProperty{TAttribute}"/> with the
        ///     passed name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     The <see cref="IAssignableProperty{TAttribute}"/> with the
        ///     given <paramref name="name"/>
        /// </returns>
        /// <exception cref="PropertyNotFoundException">
        ///     Thrown if no property with the <paramref name="name"/>
        ///      was found
        /// </exception>
        IAssignableProperty<TAttribute> GetProperty(string name);

        #endregion
    }
}