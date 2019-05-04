using System;

namespace Com.Toolbox.Utils.ControlFlow
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
        public RetryFailedException(int attempts) : this(string.Empty, attempts)
        {

        }

        /// <summary>
        ///     The constructor
        /// </summary>
        /// <param name="message">
        ///     The error message of the exception
        /// </param>
        public RetryFailedException(string message, int attempts) : base(message)
        {
            this.Attempts = attempts;
        }
        #endregion

        /// <summary>
        ///     The attempty that were made before the operation
        ///     was considered failed
        /// </summary>
        public int Attempts { get; set; }
    }
}
