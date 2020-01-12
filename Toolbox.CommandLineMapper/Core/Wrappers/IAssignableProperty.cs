// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 19:56
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     Defines a wrapper around <see cref="PropertyInfo"/> that is assignable
    ///     from a string value
    /// </summary>
    internal interface IAssignableProperty<TAttribute> where TAttribute : Attribute
    {
        #region Properties

        /// <summary>
        ///     Gets the name of the property this instance
        ///     wraps around.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the object the property belongs to
        /// </summary>
        object Owner { get; }

        /// <summary>
        ///     The <see cref="Attribute"/> this <see cref="IAssignableProperty{TAttribute}"/>
        ///     has applied
        /// </summary>
        TAttribute Attribute { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Assigns the <paramref name="value"/> to the property
        ///     wrapped by this instance
        /// </summary>
        /// <param name="value">
        ///     The value assigned to the property
        /// </param>
        void Assign(string value);

        #endregion


    }
}