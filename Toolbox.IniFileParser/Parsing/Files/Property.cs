// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-03-06 19:44
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     An implementation of <see cref="IProperty"/>
    /// </summary>
    public class Property : IProperty
    {
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="name">
        ///     The name of the property
        /// </param>
        /// <param name="value">
        ///     The value of the property
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if any of the arguments is null
        /// </exception>
        public Property(string name, string value)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion
        
        #region IProperty Implementation

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Value { get; }

        #endregion

        #region Equality Members
        
        protected bool Equals(Property other)
        {
            return this.Name == other.Name && this.Value == other.Value;
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

            return obj.GetType() == this.GetType() && this.Equals((Property) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Name != null ? this.Name.GetHashCode() : 0) * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Property left, Property right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Property left, Property right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region ToString

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ToString("=");
        }

        public string ToString(string separator)
        {
            return $"{this.Name}{separator}{this.Value}";
        }

        #endregion
    }
}