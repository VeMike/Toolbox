// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 20:39
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;
using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Core.Property
{
    /// <summary>
    ///     The base implementation/skelleton for assignable
    ///     properties.
    /// </summary>
    internal abstract class AssignablePropertyBase<TAttribute> : IAssignableProperty<TAttribute> where TAttribute : AttributeBase
    {
        #region IAssignableProperty Implementation

        /// <inheritdoc />
        public string Name => this.DeterminePropertyName();

        /// <inheritdoc />
        public object Owner { get; set; }

        /// <inheritdoc />
        public TAttribute Attribute { get; set; }

        /// <inheritdoc />
        public Type AssignableType { get; protected set; }

        /// <inheritdoc />
        public PropertyInfo Property { get; set; }

        /// <inheritdoc />
        public void Assign(string value)
        {
            var converted = this.Convert(value);

            if (converted.GetType() != this.Property.PropertyType)
            {
                throw new TypeMismatchException("The type of the property does not match the type returned by 'Convert()'",
                                                this.Property.PropertyType,
                                                converted.GetType());
            }

            this.Property.SetValue(this.Owner, converted);
        }

        #endregion

        #region Implementation Members

        /// <summary>
        ///     Converts the passed <paramref name="value"/> to
        ///     the type of <see cref="object"/>, that is assignable
        ///     to the <see cref="PropertyInfo"/> wrapped by this class.
        ///
        ///     The <see cref="PropertyInfo.PropertyType"/> needs to match
        ///     the type of the object returned here.
        /// </summary>
        /// <param name="value">
        ///     The string representation of the assignable <see cref="object"/>
        /// </param>
        /// <returns>
        ///     The <paramref name="value"/> converted to an appropriate <see cref="object"/>
        /// </returns>
        protected abstract object Convert(string value);

        /// <summary>
        ///     Determines the name of this property.
        ///
        ///     -    If the applied <see cref="AttributeBase"/> has a <see cref="AttributeBase.LongName"/>
        ///          this name is used.
        ///     -    If the applied <see cref="AttributeBase"/> dies not have a <see cref="AttributeBase.LongName"/>,
        ///          the full name of the property with the applied attribute is used
        /// </summary>
        /// <returns>
        ///    The name of the property
        /// </returns>
        private string DeterminePropertyName()
        {
            return !string.IsNullOrEmpty(this.Attribute.LongName) ? 
                this.Attribute.LongName.ToLower() : 
                this.Property.Name.ToLower();
        }

        #endregion
    }
}