// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 21:20
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Core.Property
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty{TAttribute}"/> that
    ///     assigns properties of type <see cref="bool"/>
    /// </summary>
    internal class BooleanAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : AttributeBase
    {
        /// <inheritdoc />
        public BooleanAssignableProperty()
        {
            this.AssignableType = typeof(bool);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="bool"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (value is null)
                return default(bool);
            
            /*
             * For boolean properties, an empty string indicates, that an argument
             * is present on the command line. Boolean arguments do no really required
             * a value.
             */
            if (value.Equals(string.Empty))
                return true;

            if (bool.TryParse(value, out var boolValue))
            {
                return boolValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'bool'");
        }
    }
}