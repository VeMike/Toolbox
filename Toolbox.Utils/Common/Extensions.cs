// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 17:00
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.IO;

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

    /// <summary>
    ///     A collection of extension methods for any <see cref="Stream"/>
    ///     object
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        ///     Reads the full contents of the passed
        ///     <see cref="Stream"/> and copies it
        ///     into an array
        /// </summary>
        /// <param name="stream">
        ///     The stream whose contents shall be copied
        ///     to an array
        /// </param>
        /// <returns>
        ///     A new array with the contents of <paramref name="stream"/>
        /// </returns>
        public static byte[] ToArray(this Stream stream)
        {
            stream.Position = 0;
            
            using (var memory = new MemoryStream())
            {
                stream.CopyTo(memory);

                return memory.ToArray();
            }
        }
    }
}