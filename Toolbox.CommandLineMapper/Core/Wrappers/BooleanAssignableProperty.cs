// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 21:20
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty"/> that
    ///     assigns properties of type <see cref="bool"/>
    /// </summary>
    internal class BooleanAssignableProperty : AssignablePropertyBase
    {
        /// <inheritdoc />
        public BooleanAssignableProperty(string name, 
                                         object owner, 
                                         PropertyInfo property) : base(name, 
                                                                       owner, 
                                                                       property)
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="bool"/>
        /// </exception>
        protected override object Convert(string value)
        {
            if (bool.TryParse(value, out var boolValue))
            {
                return boolValue;
            }

            throw new InvalidCastException($"Can not cast '{value}' to 'bool'");
        }
    }
}