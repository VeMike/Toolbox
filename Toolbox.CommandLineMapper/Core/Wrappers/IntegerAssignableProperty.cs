// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 21:07
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty{TAttribute}"/> that
    ///     assigns properties of type <see cref="int"/>
    /// </summary>
    internal class IntegerAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : Attribute
    {
        /// <inheritdoc />
        public IntegerAssignableProperty(string name, 
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
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="int"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (int.TryParse(value, out var intValue))
            {
                return intValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'int'");
        }
    }
}