// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 21:05
// ===================================================================================================
// = Description :
// ===================================================================================================


using System.Reflection;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty"/> that
    ///     assigns properties of type <see cref="string"/>
    /// </summary>
    internal sealed class StringAssignableProperty : AssignablePropertyBase
    {
        /// <inheritdoc />
        public StringAssignableProperty(string name, 
                                        object owner, 
                                        PropertyInfo property) : base(name, 
                                                                      owner, 
                                                                      property)
        {
        }

        /// <inheritdoc />
        protected override object Convert(string value) => value;
    }
}