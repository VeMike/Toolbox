// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-22 16:06
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Text.RegularExpressions;
using Com.Toolbox.Utils.Probing;

namespace Toolbox.CommandLineMapper.Common
{
    /// <summary>
    ///     Defines a single argument passed on the command line.
    ///
    ///     e.g.: '-path C:\\the\\path.txt'
    /// </summary>
    public class Argument : IEquatable<Argument>
    {
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class. Using this
        ///     constructor creates an argument, that does not have
        ///     a <see cref="Value"/>
        /// </summary>
        /// <param name="commandPrefix">
        ///    The prefix of the command
        /// </param>
        /// <param name="command">
        ///     The command passed in the command line including
        ///     the prefix.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown if:
        ///     - <paramref name="commandPrefix"/> is null
        ///     - <paramref name="command"/> is null
        /// </exception>
        public Argument(string commandPrefix, string command) : this(commandPrefix,
                                                                     command,
                                                                     string.Empty)
        {
        }

        /// <summary>
        ///     Creates a new instance of the class. Using this
        ///     constructor creates an argument, that has
        ///     a <see cref="Value"/>
        /// </summary>
        /// <param name="commandPrefix">
        ///    The prefix of the command
        /// </param>
        /// <param name="command">
        ///     The command passed in the command line including
        ///     the prefix.
        /// </param>
        /// <param name="value">
        ///    The value of the command
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown if:
        ///     - <paramref name="commandPrefix"/> is null
        ///     - <paramref name="command"/> is null
        /// </exception>
        public Argument(string commandPrefix, string command, string value)
        {
            Guard.AgainstNullArgument(nameof(commandPrefix), commandPrefix);
            Guard.AgainstNullArgument(nameof(command), command);
            
            this.CommandPrefix = commandPrefix;
            this.Command = command;
            this.Value = value;

            this.HasValue = !string.IsNullOrEmpty(value);
        }

        #endregion
        
        #region Properties

        /// <summary>
        ///     The command passed on the command line
        /// </summary>
        public string Command { get; }
        
        /// <summary>
        ///     Returns the <see cref="Command"/> where the
        ///     <see cref="CommandPrefix"/> is replaced by
        ///     an empty string
        /// </summary>
        public string CommandWithoutPrefix => new Regex(Regex.Escape(this.CommandPrefix)).Replace(this.Command, 
                                                                                                         string.Empty, 
                                                                                                         1);
        
        /// <summary>
        ///     The prefix of the command (e.g. '-')
        /// </summary>
        public string CommandPrefix { get; }

        /// <summary>
        ///     The value of the <see cref="Command"/>.
        ///     This is <see cref="string.Empty"/> if the
        ///     command does not have/required a value.
        ///
        ///     If the command does not have a valid value
        ///     <see cref="HasValue"/> indicates this.
        /// </summary>
        public string Value { get; }
        
        /// <summary>
        ///     Indicates if the <see cref="Command"/> has
        ///     a <see cref="Value"/>.
        /// </summary>
        public bool HasValue { get; }
        
        /// <summary>
        ///     Gets or sets a value that indicates if this
        ///     argument was mapped to any object.
        /// </summary>
        public bool IsMapped { get; set; }

        #endregion

        #region Equality Members

        /// <inheritdoc />
        public bool Equals(Argument other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Command == other.Command && this.CommandPrefix == other.CommandPrefix && this.Value == other.Value;
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

            return Equals((Argument) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (this.Command != null ? this.Command.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CommandPrefix != null ? this.CommandPrefix.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Argument left, Argument right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Argument left, Argument right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region ToString

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(this.Command)}: {this.Command}, {nameof(this.CommandWithoutPrefix)}: {this.CommandWithoutPrefix}, {nameof(this.CommandPrefix)}: {this.CommandPrefix}, {nameof(this.Value)}: {this.Value}, {nameof(this.HasValue)}: {this.HasValue}";
        }

        #endregion
    }
}