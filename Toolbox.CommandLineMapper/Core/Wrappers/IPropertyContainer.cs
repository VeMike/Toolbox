// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 20:02
// ===================================================================================================
// = Description : 
// ===================================================================================================

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     Wraps around any <see cref="object"/> that has
    ///     <see cref="IAssignableProperty"/>s
    /// </summary>
    internal interface IPropertyContainer
    {
        #region Properties

        /// <summary>
        ///     The <see cref="object"/> whose properties are
        ///     inside container instance
        /// </summary>
        object Source { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets a <see cref="IAssignableProperty"/> with the
        ///     passed name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        ///     The <see cref="IAssignableProperty"/> with the
        ///     given <paramref name="name"/>
        /// </returns>
        /// <exception cref="PropertyNotFoundException">
        ///     Thrown if no property with the <paramref name="name"/>
        ///      was found
        /// </exception>
        IAssignableProperty GetProperty(string name);

        #endregion
    }
}