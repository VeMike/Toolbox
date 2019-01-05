using System;

namespace Utilities.ControlFlow
{
    /// <summary>
    ///     The <see cref="Exception"/>, that is thrown, whenever
    ///     the <see cref="Retry"/>-Mechanism has finished retrying,
    ///     but the operation sill was not successfull
    /// </summary>
    public class RetryFailedException : Exception
    {
        #region Constructor
        /// <summary>
        ///     The default constructor
        /// </summary>
        public RetryFailedException() : base()
        {

        }

        /// <summary>
        ///     The constructor
        /// </summary>
        /// <param name="message">
        ///     The error message of the exception
        /// </param>
        public RetryFailedException(string message) : base(message)
        {

        }
        #endregion
    }
}
