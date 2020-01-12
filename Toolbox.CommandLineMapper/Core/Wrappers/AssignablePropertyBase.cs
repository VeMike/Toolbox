// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 20:39
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;
using Com.Toolbox.Utils.Probing;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     The base implementation/skelleton for assignable
    ///     properties.
    /// </summary>
    internal abstract class AssignablePropertyBase<TAttribute> : IAssignableProperty<TAttribute> where TAttribute : Attribute
    {
        #region Attributes

        /// <summary>
        ///     The <see cref="PropertyInfo"/> the class wraps around.
        /// </summary>
        private readonly PropertyInfo property;

        #endregion

        #region Constructor

        /// <summary>
        ///     Provides default initialization for assignable
        ///     properties
        /// </summary>
        /// <param name="name">
        ///     The name of this <see cref="IAssignableProperty{TAttribute}"/>
        /// </param>
        /// <param name="owner">
        ///     The <see cref="object"/> that owns the property
        /// </param>
        /// <param name="property">
        ///     The <see cref="PropertyInfo"/> wrapped by this instance
        /// </param>
        /// <param name="attribute">
        ///     The <see cref="Attribute"/> the property has applied
        /// </param>
        protected AssignablePropertyBase(string name, 
                                         object owner, 
                                         PropertyInfo property,
                                         TAttribute attribute)
        {
            Guard.AgainstNullArgument(nameof(name), name);
            Guard.AgainstNullArgument(nameof(owner), owner);
            Guard.AgainstNullArgument(nameof(property), property);
            Guard.AgainstNullArgument(nameof(attribute), attribute);

            this.Name = name;
            this.Owner = owner;
            this.property = property;
            this.Attribute = attribute;
        }

        #endregion

        #region IAssignableProperty Implementation

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public object Owner { get; }

        /// <inheritdoc />
        public TAttribute Attribute { get; }

        /// <inheritdoc />
        public void Assign(string value)
        {
            var converted = this.Convert(value);

            if (converted.GetType() != this.property.PropertyType)
            {
                throw new TypeMismatchException("The type of the property does not match the converted type",
                                                this.property.PropertyType,
                                                converted.GetType());
            }

            this.property.SetValue(this.Owner, converted);
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

        #endregion
    }
}