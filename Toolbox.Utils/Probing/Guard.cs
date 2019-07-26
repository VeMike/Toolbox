// ===================================================================================================
// = Author      :  Mike
// = Created     :  2019-07-26 23:32
// ===================================================================================================
// = Description : A simple collection of some clauses to check.
// ===================================================================================================

using System;

namespace Com.Toolbox.Utils.Probing
{
    /// <summary>
    ///     A simple collection of some clauses to check.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        ///     Guards against a null argument by throwing and exception
        /// </summary>
        /// <typeparam name="TArgument">
        ///     The type of the argument to check. The type
        ///     is restricted to classes to only check reference
        ///     types.
        /// </typeparam>
        /// <param name="paramName">
        ///     The name of the parameter to check
        /// </param>
        /// <param name="argument">
        ///     The argument to check
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="argument"/> is null
        /// </exception>
        public static void AgainstNullArgument<TArgument>(string paramName, 
                                                          TArgument argument) where TArgument : class
        {
            if (argument is null)
            {
                throw new ArgumentNullException(paramName, $"{paramName} is null");
            }
        }

        public static void AgainstNullProperty<TProperty>(string propertyName,
                                                          TProperty property) where TProperty : class
        {
            throw new NotImplementedException();
        }
    }
}