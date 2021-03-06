// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 19:04
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     Represents a single line of
    ///     a text file
    /// </summary>
    public class Line
    {
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="content">
        ///     The content of the line
        /// </param>
        /// <param name="number">
        ///     The number of the line. The first
        ///     line is at index 0
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if any of the passed arguments is null
        /// </exception>
        public Line(int number, string content)
        {
            this.Content = content ?? throw new ArgumentNullException(nameof(content));
            this.Number = number;
        }

        #endregion
        
        #region Properties

        /// <summary>
        ///     The content of the line
        /// </summary>
        public string Content { get; }
        
        /// <summary>
        ///     The number of the line. The first
        ///     line is at index 0
        /// </summary>
        public int Number { get; }

        #endregion

        #region Equality

        protected bool Equals(Line other)
        {
            return this.Content == other.Content && this.Number == other.Number;
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

            return this.Equals((Line) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Content != null ? this.Content.GetHashCode() : 0) * 397) ^ this.Number;
            }
        }

        public static bool operator ==(Line left, Line right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Line left, Line right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(this.Content)}: {this.Content}, {nameof(this.Number)}: {this.Number}";
        }

        #endregion
    }
}