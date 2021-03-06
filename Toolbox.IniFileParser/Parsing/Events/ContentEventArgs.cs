// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 16:30
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;

namespace Toolbox.IniFileParser.Parsing.Events
{
    /// <summary>
    ///     Event arguments for the event
    ///     raised if the parser encounters
    ///     a line with content (e.g. section,
    ///     comment).
    /// </summary>
    public class ContentEventArgs : ParserEventArgs
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
        /// <param name="content">
        ///     The value of the content
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if any of the passed arguments is null
        /// </exception>
        public ContentEventArgs(int lineNumber, 
                                string originalLine,
                                string content) : base(lineNumber, originalLine)
        {
            this.Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The value of the content
        /// </summary>
        public string Content { get; }

        #endregion

        #region Equality

        protected bool Equals(ContentEventArgs other)
        {
            return this.Content == other.Content;
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

            return this.Equals((ContentEventArgs) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (this.Content != null ? this.Content.GetHashCode() : 0);
        }

        public static bool operator ==(ContentEventArgs left, ContentEventArgs right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ContentEventArgs left, ContentEventArgs right)
        {
            return !Equals(left, right);
        }

        #endregion
        
        #region Overrides

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(this.Content)}: {this.Content}";
        }

        #endregion
    }
}