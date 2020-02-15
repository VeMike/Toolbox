// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 21:05
// ===================================================================================================
// = Description :
// ===================================================================================================


using System;
using System.Reflection;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty{TAttribute}"/> that
    ///     assigns properties of type <see cref="string"/>
    /// </summary>
    internal sealed class StringAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : Attribute
    {
        /// <inheritdoc />
        public StringAssignableProperty(object owner,
                                        PropertyInfo property,
                                        TAttribute attribute) : base(owner, 
                                                                     property, 
                                                                     attribute)
        {
        }

        /// <inheritdoc />
        protected override object Convert(string value) => value;
    }
}