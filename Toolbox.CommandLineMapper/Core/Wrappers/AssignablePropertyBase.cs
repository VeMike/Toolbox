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
        /// <param name="owner">
        ///     The <see cref="object"/> that owns the property
        /// </param>
        /// <param name="property">
        ///     The <see cref="PropertyInfo"/> wrapped by this instance
        /// </param>
        /// <param name="attribute">
        ///     The <see cref="Attribute"/> the property has applied
        /// </param>
        protected AssignablePropertyBase(object owner,
                                         PropertyInfo property,
                                         TAttribute attribute)
        {
            Guard.AgainstNullArgument(nameof(owner), owner);
            Guard.AgainstNullArgument(nameof(property), property);
            Guard.AgainstNullArgument(nameof(attribute), attribute);

            this.Owner = owner;
            this.property = property;
            this.Attribute = attribute;
        }

        #endregion

        #region IAssignableProperty Implementation

        /// <inheritdoc />
        public string Name => this.property.Name.ToLower();

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
                throw new TypeMismatchException("The type of the property does not match the type returned by 'Convert()'",
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

        #region IEquatable Implementation

        public bool Equals(IAssignableProperty<TAttribute> other)
        {
            return other != null && 
                   this.Name == other.Name && 
                   Equals(this.Owner, other.Owner);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((AssignablePropertyBase<TAttribute>) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Name != null ? this.Name.GetHashCode() : 0) * 397) ^ 
                       (this.Owner != null ? this.Owner.GetHashCode() : 0);
            }
        }

        #endregion

        #region Operators

        public static bool operator ==(AssignablePropertyBase<TAttribute> left,
                                       AssignablePropertyBase<TAttribute> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AssignablePropertyBase<TAttribute> left,
                                       AssignablePropertyBase<TAttribute> right)
        {
            return !Equals(left, right);
        } 

        #endregion
    }
}