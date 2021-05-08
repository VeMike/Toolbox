// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-01-24 12:50
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Toolbox.Utils.Probing;

// ReSharper disable InvalidXmlDocComment

namespace Toolbox.UrlParser.Parsing
{
    /// <summary>
    ///     A single URL parameter (e.g. query parameter
    ///     or path parameter)
    /// </summary>
    public sealed class Parameter : IEquatable<Parameter>
    {
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="name">
        ///     The name of the url parameter
        ///
        ///     <seealso cref="Name"/>
        /// </param>
        /// <param name="value">
        ///     The value of the parameter
        ///
        ///     <seealso cref="Value"/>
        /// </param>
        /// <param name="index">
        ///     The index of the parameter
        ///
        ///     <seealso cref="Index"/>
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="name"/> or
        ///     <paramref name="value"/> is null
        /// </exception>
        public Parameter(string name, string value, int index)
        {
            Guard.AgainstNullArgument(nameof(name), name);
            Guard.AgainstNullArgument(nameof(value), value);
            Guard.AgainstEmptyString(name);

            this.Name = name;
            this.Index = index;
            this.Value = value;
        }

        #endregion
        
        #region Properties

        /// <summary>
        ///     The name of the url parameter.
        ///
        ///     e.g.:
        ///     For the URL 'http://127.0.0.1:8000/foo/?skip=0&limit=10',
        ///     'skip' and 'limit' would be the parameter names.
        ///
        ///     
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The value of the url parameter.
        ///
        ///     e.g.:
        ///     For the URL 'http://127.0.0.1:8000/foo/?skip=0&limit=10',
        ///     '0' (skip) and '10' (limit) would be the parameter values.
        /// </summary>
        public string Value { get; }

        /// <summary>
        ///     The segment or query parameter index of the url parameter.
        ///
        ///     e.g.:
        ///     For the URL 'http://127.0.0.1:8000/foo/?skip=0&limit=10',
        ///     'skip=0' would have index '0', 'limit=10' would have
        ///     index '1'
        /// </summary>
        public int Index { get; }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(this.Name)}: {this.Name}, {nameof(this.Value)}: {this.Value}, {nameof(this.Index)}: {this.Index}";
        }

        /// <inheritdoc />
        public bool Equals(Parameter other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Name == other.Name && this.Value == other.Value && this.Index == other.Index;
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

            return this.Equals((Parameter) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.Index;
                return hashCode;
            }
        }

        public static bool operator ==(Parameter left, Parameter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Parameter left, Parameter right)
        {
            return !Equals(left, right);
        }
        
        #endregion
    }
}