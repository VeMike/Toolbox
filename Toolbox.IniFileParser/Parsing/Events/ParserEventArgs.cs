// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 16:12
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Com.Toolbox.Utils.Probing;

namespace Toolbox.IniFileParser.Parsing.Events
{
    /// <summary>
    ///     The base class for event arguments
    ///     of parser events
    /// </summary>
    public class ParserEventArgs : EventArgs
    {
        #region Constructor

        /// <inheritdoc />
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
        /// <exception cref="ArgumentNullException">
        ///     Thrown if any of the passed arguments
        ///     is null
        /// </exception>
        public ParserEventArgs(int lineNumber, string originalLine)
        {
            Guard.AgainstNullArgument(nameof(originalLine), originalLine);
            
            this.LineNumber = lineNumber;
            this.OriginalLine = originalLine;
        }

        #endregion
        
        #region Properties

        /// <summary>
        ///     The number of the line that caused
        ///     the event to be raised.
        /// </summary>
        public int LineNumber { get; }

        /// <summary>
        ///     The original content of
        ///     the line
        /// </summary>
        public string OriginalLine { get; }

        #endregion

        #region Equality

        protected bool Equals(ParserEventArgs other)
        {
            return this.LineNumber == other.LineNumber && this.OriginalLine == other.OriginalLine;
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

            return this.Equals((ParserEventArgs) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.LineNumber * 397) ^ (this.OriginalLine != null ? this.OriginalLine.GetHashCode() : 0);
            }
        }

        public static bool operator ==(ParserEventArgs left, ParserEventArgs right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ParserEventArgs left, ParserEventArgs right)
        {
            return !Equals(left, right);
        }

        #endregion
        
        #region Overrides

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(this.LineNumber)}: {this.LineNumber}, {nameof(this.OriginalLine)}: {this.OriginalLine}";
        }

        #endregion
    }
}