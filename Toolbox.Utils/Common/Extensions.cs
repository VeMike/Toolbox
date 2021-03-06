﻿// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 17:00
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace Toolbox.Utils.Common
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
    ///     A collection of extension methods for any <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Tries to parse a string value to the specified
        ///     type. The string is parsed using <see cref="CultureInfo.InvariantCulture"/>
        /// </summary>
        /// <param name="value">
        ///     The string that should be parsed
        /// </param>
        /// <param name="result">
        ///     The result of the parser
        /// </param>
        /// <typeparam name="T">
        ///     The type the string should be parsed into
        /// </typeparam>
        /// <returns>
        ///     'true' if the <paramref name="value"/> was
        ///     parsed successfully, 'false' if not.
        /// </returns>
        public static bool TryParse<T>(this string value, out T result)
        {
            result = default;
            var converter = TypeDescriptor.GetConverter(typeof(T));

            try
            {
                result = (T) converter.ConvertFromInvariantString(value);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
        ///     into an array.
        /// </summary>
        /// <param name="stream">
        ///     The stream whose contents shall be copied
        ///     to an array.
        ///
        ///     This stream will be copied from its
        ///     current position.
        /// </param>
        /// <returns>
        ///     A new array with the contents of <paramref name="stream"/>
        ///     read from the current position
        /// </returns>
        public static byte[] ToArray(this Stream stream)
        {
            return stream.ToArray(stream.Position);
        }

        /// <summary>
        ///     Reads the full contents of the passed
        ///     <see cref="Stream"/> and copies it
        ///     into an array.
        /// </summary>
        /// <param name="stream">
        ///     The stream whose contents shall be copied
        ///     to an array.
        /// </param>
        /// <param name="position">
        ///     The position from which the stream shall be
        ///     copied.
        /// </param>
        /// <returns>
        ///     A new array with the contents of <paramref name="stream"/>
        /// </returns>
        public static byte[] ToArray(this Stream stream, long position)
        {
            stream.Position = position;

            using var memory = new MemoryStream();
            stream.CopyTo(memory);

            return memory.ToArray();
        }
    }
}