// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-02-15 13:47
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;

namespace Toolbox.CommandLineMapper.Mapper
{
    /// <summary>
    ///     This exception is thrown whenever the <see cref="MapperOptions"/>
    ///     applied to a mapped are not valid.
    /// </summary>
    public sealed class OptionsException : Exception
    {
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        public OptionsException()
        {

        }

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="message">
        ///     The message of the exception
        /// </param>
        public OptionsException(string message) : base(message)
        {

        }

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="message">
        ///     The message of the exception
        /// </param>
        /// <param name="innerException">
        ///     The <see cref="Exception"/> that caused this
        ///     <see cref="OptionsException"/> to be raised.
        /// </param>
        public OptionsException(string message,
                 Exception innerException) : base(message,
                                                  innerException)
        {

        }

        #endregion
    }
}