// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 19:56
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Reflection;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     Defines a wrapper around <see cref="PropertyInfo"/> that is assignable
    ///     from a string value
    /// </summary>
    internal interface IAssignableProperty
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