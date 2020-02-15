// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 23:48
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty{TAttribute}"/> that
    ///     assigns properties of type <see cref="float"/>
    /// </summary>
    internal class FloatAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : Attribute
    {
        /// <inheritdoc />
        public FloatAssignableProperty(object owner,
                                       PropertyInfo property,
                                       TAttribute attribute) : base(owner, 
                                                                    property, 
                                                                    attribute)
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="float"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (float.TryParse(value, out var floatValue))
            {
                return floatValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'float'");
        }
    }
}