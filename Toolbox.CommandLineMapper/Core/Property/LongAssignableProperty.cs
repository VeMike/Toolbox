// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 23:55
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Core.Property
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty{TAttribute}"/> that
    ///     assigns properties of type <see cref="long"/>
    /// </summary>
    internal class LongAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : AttributeBase
    {
        /// <inheritdoc />
        public LongAssignableProperty()
        {
            this.AssignableType = typeof(long);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="long"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (string.IsNullOrEmpty(value))
                return default(long);
            
            if (long.TryParse(value, out var longValue))
            {
                return longValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'long'");
        }
    }
}