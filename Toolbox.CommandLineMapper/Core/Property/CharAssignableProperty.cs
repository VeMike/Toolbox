// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 23:50
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Core.Property
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty{TAttribute}"/> that
    ///     assigns properties of type <see cref="char"/>
    /// </summary>
    internal class CharAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : AttributeBase
    {
        /// <inheritdoc />
        public CharAssignableProperty()
        {
            this.AssignableType = typeof(char);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="char"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (string.IsNullOrEmpty(value))
                return default(char);

            if (value.Length != 1)
            {
                throw new InvalidCastException($"A string with more than one character can not be cast to 'char'");
            }

            return value[0];
        }
    }
}