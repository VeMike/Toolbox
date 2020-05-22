// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 21:11
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Globalization;
using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Core.Property
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty{TAttribute}"/> that
    ///     assigns properties of type <see cref="double"/>
    /// </summary>
    internal class DoubleAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : AttributeBase
    {
        /// <inheritdoc />
        public DoubleAssignableProperty()
        {
            this.AssignableType = typeof(double);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="double"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (string.IsNullOrEmpty(value))
                return default(double);
            
            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleValue))
            {
                return doubleValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'double'");
        }
    }
}