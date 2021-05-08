// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-04-11 13:50
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;

namespace Toolbox.Utils.Collection
{
    /// <summary>
    ///     A collection of extension methods for list
    ///     data types
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        ///     Adds a range of elements to the end of
        ///     a list.
        /// </summary>
        /// <param name="list">
        ///     The list to add the range of items to
        /// </param>
        /// <param name="items">
        ///     The items to add
        /// </param>
        /// <typeparam name="T">
        ///     The type of element of the list.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="items"/> is null.
        /// </exception>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));
            
            if(list is List<T> specificList)
            {
                specificList.AddRange(items);
            }
            else
            {
                foreach (var item in items)
                {
                    list.Add(item);
                }
            }
        }
    }
}