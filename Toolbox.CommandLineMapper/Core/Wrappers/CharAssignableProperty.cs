// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 23:50
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;
using Toolbox.CommandLineMapper.Common;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty"/> that
    ///     assigns properties of type <see cref="char"/>
    /// </summary>
    internal class CharAssignableProperty : AssignablePropertyBase
    {
        /// <inheritdoc />
        public CharAssignableProperty(string name, 
                                      object owner, 
                                      PropertyInfo property) : base(name, 
                                                                    owner, 
                                                                    property)
        {
        }

        /// <inheritdoc />
        /// <exception cref="InvalidCastException">
        ///     Thrown  if the <see cref="value"/> can not be cast to <see cref="char"/>
        /// </exception>
        protected override object Convert(string value) => value.ToSingleCharString();
    }
}