// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 17:00
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Collections.Generic;

namespace Com.Toolbox.Utils.Common
{
    /// <summary>
    ///     A collection of extension methods for any <see cref="object"/>
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Creates an <see cref="IEnumerable{T}"/> from any
        ///     type of object
        /// </summary>
        /// <typeparam name="T">
        ///     The type that shall be enumerated
        /// </typeparam>
        /// <param name="item">
        ///     The item that is enumerated
        /// </param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}"/> containing the
        ///     single element <paramref name="item"/>
        /// </returns>
        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }
    }
}