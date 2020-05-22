// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 21:05
// ===================================================================================================
// = Description :
// ===================================================================================================


using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Core.Property
{
    /// <summary>
    ///     An implementation of <see cref="IAssignableProperty{TAttribute}"/> that
    ///     assigns properties of type <see cref="string"/>
    /// </summary>
    internal sealed class StringAssignableProperty<TAttribute> : AssignablePropertyBase<TAttribute> where TAttribute : AttributeBase
    {
        /// <inheritdoc />
        public StringAssignableProperty()
        {
            this.AssignableType = typeof(string);
        }

        /// <inheritdoc />
        protected override object Convert(string value) => value;
    }
}