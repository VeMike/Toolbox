// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 23:52
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
    ///     assigns properties of type <see cref="byte"/>
    /// </summary>
    internal class ByteAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : AttributeBase
    {
        /// <inheritdoc />
        public ByteAssignableProperty(object owner,
                                      PropertyInfo property,
                                      TAttribute attribute) : base(owner, 
                                                                   property, 
                                                                   attribute)
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="byte"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (string.IsNullOrEmpty(value))
                return default(byte);
            
            if (byte.TryParse(value, out var byteValue))
            {
                return byteValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'byte'");
        }
    }
}