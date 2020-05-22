// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 20:06
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;

namespace Toolbox.CommandLineMapper.Core.Property
{
    /// <summary>
    ///     Thrown if a property can not be found in
    ///     <see cref="IPropertyContainer"/> using a
    ///     name.
    /// </summary>
    public class PropertyNotFoundException : Exception
    {
        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="message">
        ///     The message of the exception
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property, that was not found
        /// </param>
        public PropertyNotFoundException(string message, string propertyName) : base(message)
        {
            this.PropertyName = propertyName ?? string.Empty;
        }

        /// <summary>
        ///     The name of the property, that was not found
        /// </summary>
        public string PropertyName { get; }
    }
}