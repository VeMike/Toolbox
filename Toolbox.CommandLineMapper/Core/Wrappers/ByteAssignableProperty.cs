// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 23:52
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty"/> that
    ///     assigns properties of type <see cref="byte"/>
    /// </summary>
    internal class ByteAssignableProperty : AssignablePropertyBase
    {
        /// <inheritdoc />
        public ByteAssignableProperty(string name, 
                                      object owner, 
                                      PropertyInfo property) : base(name, 
                                                                    owner, 
                                                                    property)
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="byte"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (byte.TryParse(value, out var byteValue))
            {
                return byteValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'byte'");
        }
    }
}