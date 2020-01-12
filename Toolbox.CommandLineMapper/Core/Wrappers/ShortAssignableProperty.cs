// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 23:53
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty{TAttribute}"/> that
    ///     assigns properties of type <see cref="short"/>
    /// </summary>
    internal class ShortAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : Attribute
    {
        /// <inheritdoc />
        public ShortAssignableProperty(string name, 
                                       object owner, 
                                       PropertyInfo property,
                                       TAttribute attribute) : base(name, 
                                                                     owner, 
                                                                     property, 
                                                                     attribute)
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="short"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (short.TryParse(value, out var shortValue))
            {
                return shortValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'short'");
        }
    }
}