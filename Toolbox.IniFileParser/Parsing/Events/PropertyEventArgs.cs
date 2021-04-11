// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 16:38
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;

namespace Toolbox.IniFileParser.Parsing.Events
{
    /// <summary>
    ///     Event arguments for the event
    ///     raised if the parser encounters
    ///     a property.
    /// </summary>
    public class PropertyEventArgs : ParserEventArgs
    {
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="lineNumber">
        ///     The number of the line that caused
        ///     the event to be raised.
        /// </param>
        /// <param name="originalLine">
        ///     The original content of
        ///     the line
        /// </param>
        /// <param name="property">
        ///     The property of a ini
        ///     file section
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if any of the passed arguments are null
        /// </exception>
        public PropertyEventArgs(int lineNumber, 
                                 string originalLine,
                                 KeyValuePair<string, string> property) : base(lineNumber, originalLine)
        {
            this.Property = property;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The property of a ini file section.
        ///
        ///     e.g.:
        ///     Property: 'Configuration=Foo'
        ///
        ///     The 'Key' of the property will contain
        ///     'Configuration', the 'Value' will be
        ///     'Foo'.
        /// </summary>
        public KeyValuePair<string, string> Property { get; }

        #endregion

        #region Equality

        protected bool Equals(PropertyEventArgs other)
        {
            return base.Equals(other) && this.Property.Equals(other.Property);
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

            return this.Equals((PropertyEventArgs) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ this.Property.GetHashCode();
            }
        }

        public static bool operator ==(PropertyEventArgs left, PropertyEventArgs right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PropertyEventArgs left, PropertyEventArgs right)
        {
            return !Equals(left, right);
        }

        #endregion
        
        #region Overrides

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(this.Property)}: {this.Property}";
        }

        #endregion
    }
}