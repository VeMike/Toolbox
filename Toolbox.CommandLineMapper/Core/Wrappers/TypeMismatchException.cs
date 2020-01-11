// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 20:56
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     Thrown if the type of objects assigned to an
    ///     <see cref="IAssignableProperty"/> does not
    ///     mach the actual type of the property.
    /// </summary>
    internal class TypeMismatchException : Exception
    {
        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="message">
        ///     The exception message
        /// </param>
        /// <param name="expected">
        ///     The <see cref="Type"/> the property actually has.
        /// </param>
        /// <param name="actual">
        ///     The actual <see cref="Type"/> that was tried to assign
        /// </param>
        public TypeMismatchException(string message, 
                                     Type expected, 
                                     Type actual) : base(message)
        {
            this.Actual = actual;
            this.Expected = expected;
        }

        #region Properties

        /// <summary>
        ///     The <see cref="Type"/> the property actually has.
        /// </summary>
        public Type Expected { get; }

        /// <summary>
        ///     The actual <see cref="Type"/> that was tried to assign
        /// </summary>
        public Type Actual { get; }

        #endregion
    }
}