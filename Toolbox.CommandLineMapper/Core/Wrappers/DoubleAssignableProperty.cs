// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 21:11
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty{TAttribute}"/> that
    ///     assigns properties of type <see cref="double"/>
    /// </summary>
    internal class DoubleAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : Attribute
    {
        /// <inheritdoc />
        public DoubleAssignableProperty(string name, 
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
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="double"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (double.TryParse(value, out var doubleValue))
            {
                return doubleValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'double'");
        }
    }
}