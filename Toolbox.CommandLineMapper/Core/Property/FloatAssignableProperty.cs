// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 23:48
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
    ///     assigns properties of type <see cref="float"/>
    /// </summary>
    internal class FloatAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : AttributeBase
    {
        /// <inheritdoc />
        public FloatAssignableProperty()
        {
            this.AssignableType = typeof(float);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="float"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (string.IsNullOrEmpty(value))
                return default(float);
            
            if (float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var floatValue))
            {
                return floatValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'float'");
        }
    }
}