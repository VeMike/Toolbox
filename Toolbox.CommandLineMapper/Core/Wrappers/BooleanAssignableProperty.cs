// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 21:20
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;
using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty{TAttribute}"/> that
    ///     assigns properties of type <see cref="bool"/>
    /// </summary>
    internal class BooleanAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : AttributeBase
    {
        /// <inheritdoc />
        public BooleanAssignableProperty(object owner,
                                         PropertyInfo property,
                                         TAttribute attribute) : base(owner, 
                                                                      property, 
                                                                      attribute)
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="bool"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (string.IsNullOrEmpty(value))
                return default(bool);
            
            if (bool.TryParse(value, out var boolValue))
            {
                return boolValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'bool'");
        }
    }
}