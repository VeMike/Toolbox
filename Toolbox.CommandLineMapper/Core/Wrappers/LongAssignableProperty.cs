// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 23:55
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty{TAttribute}"/> that
    ///     assigns properties of type <see cref="long"/>
    /// </summary>
    internal class LongAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : Attribute
    {
        /// <inheritdoc />
        public LongAssignableProperty(object owner,
                                      PropertyInfo property,
                                      TAttribute attribute) : base(owner, 
                                                                   property, 
                                                                   attribute)
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="long"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (long.TryParse(value, out var longValue))
            {
                return longValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'long'");
        }
    }
}