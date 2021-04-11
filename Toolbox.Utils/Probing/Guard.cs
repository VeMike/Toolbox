// ===================================================================================================
// = Author      :  Mike
// = Created     :  2019-07-26 23:32
// ===================================================================================================
// = Description : A simple collection of some clauses to check.
// ===================================================================================================

using System;
using System.Collections.Generic;

namespace Com.Toolbox.Utils.Probing
{
    /// <summary>
    ///     A simple collection of some clauses to check.
    /// </summary>
    public static class Guard
    {
        #region Null Guards

        /// <summary>
        ///     Guards against a null argument by throwing and exception
        /// </summary>
        /// <typeparam name="TArgument">
        ///     The type of the argument to check. The type
        ///     is restricted to classes so only reference
        ///     types get checked.
        /// </typeparam>
        /// <param name="paramName">
        ///     The name of the parameter to check
        /// </param>
        /// <param name="argument">
        ///     The argument to check
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="argument" /> is null
        /// </exception>
        public static void AgainstNullArgument<TArgument>(string paramName,
                                                          TArgument argument) where TArgument : class
        {
            if (argument is null)
                throw new ArgumentNullException(paramName, $"{paramName} is null");
        }

        /// <summary>
        ///     Guards against a null property value by throwing an exception
        /// </summary>
        /// <typeparam name="TProperty">
        ///     The type of the property to check. The type
        ///     is restricted to classes so only reference types
        ///     get checked.
        /// </typeparam>
        /// <param name="className">
        ///     The name of the class whose property is guarded
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property
        /// </param>
        /// <param name="property">
        ///     The property to check.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Thrown if <paramref name="property" /> is null
        /// </exception>
        public static void AgainstNullProperty<TProperty>(string className,
                                                          string propertyName,
                                                          TProperty property) where TProperty : class
        {
            if (property is null)
                throw new ArgumentException($"{className}.{propertyName} is null");
        }

        #endregion

        #region Emptiness Guards

        /// <summary>
        ///     Guards against an empty string
        /// </summary>
        /// <param name="value">
        ///     The string checked for emptiness
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Thrown if <paramref name="value" /> is empty
        /// </exception>
        public static void AgainstEmptyString(string value)
        {
            AgainstNullArgument(nameof(value), value);

            if(value.Equals(string.Empty))
                throw new ArgumentException("The passed string is empty");
        }

        /// <summary>
        ///     Guards against an empty collection
        /// </summary>
        /// <typeparam name="TElement">
        ///     The type of element in the collection
        /// </typeparam>
        /// <param name="collection">
        ///     The collection checked for emptiness
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Thrown if <paramref name="collection" /> is empty
        /// </exception>
        public static void AgainstEmptyCollection<TElement>(ICollection<TElement> collection)
        {
            AgainstNullArgument(nameof(collection), collection);

            if(collection.Count == 0)
                throw new ArgumentException($"The collection of '{typeof(TElement).Name}' is empty");
        }

        #endregion
    }
}