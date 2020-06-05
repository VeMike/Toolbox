using System;
using System.Threading;

namespace Com.Toolbox.Utils.ControlFlow
{
    /// <summary>
    ///     Provides mechanisms for retrying operations
    /// </summary>
    public static class Retry
    {
        #region Methods
        
        /// <summary>
        ///     Provides a generic retry-mechanism for methods, that return a boolean
        /// </summary>
        /// <param name="function">
        ///     The function that shall be retried, if it fails (i.e. returns false)
        /// </param>
        /// <param name="maxAttempts">
        ///     The maximum number of attempts that will be made to call the passed
        ///     method (default = 3).
        /// </param>
        /// <exception cref="RetryFailedException">
        ///     Thrown, if the maximum amount of retries was reached.
        /// </exception>
        public static void Until(Func<bool> function, int maxAttempts = 3)
        {
            Until(function, TimeSpan.Zero, maxAttempts);
        }
        
        /// <summary>
        ///     Provides a generic retry-mechanism for methods, that return a boolean
        /// </summary>
        /// <param name="function">
        ///     The function that shall be retried, if it fails (i.e. returns false)
        /// </param>
        /// <param name="retryInterval">
        ///     The interval for the retry mechanism. If the passed method fails,
        ///     this method will wait (Thread.Sleep()) for the specified amount
        ///     of time before it tries to call the method again.
        /// </param>
        /// <param name="maxAttempts">
        ///     The maximum number of attempts that will be made to call the passed
        ///     method (default = 3).
        /// </param>
        /// <exception cref="RetryFailedException">
        ///     Thrown, if the maximum amount of retries was reached.
        /// </exception>
        public static void Until(Func<bool> function, TimeSpan retryInterval, int maxAttempts = 3)
        {
            //Try to call the method the passed number of times
            for (var attempts = 0; attempts < maxAttempts; attempts++)
            {
                if (CallDelegateAndWait(function, retryInterval))
                {
                    return;
                }
            }
            //Throw an exception
            throw new RetryFailedException($"All '{maxAttempts}' to call the method failed", maxAttempts);
        }

        /// <summary>
        ///     Calls <paramref name="function"/> and waits for
        ///     <paramref name="retryInterval"/> if the function
        ///     did not return 'true'
        /// </summary>
        /// <param name="function">
        ///     The function whose call possibly fails
        /// </param>
        /// <param name="retryInterval">
        ///     The wait time (<see cref="Thread.Sleep(TimeSpan)"/>) after
        ///     the call of <paramref name="function"/>
        /// </param>
        /// <returns>
        ///     The return value of <paramref name="function"/>
        /// </returns>
        private static bool CallDelegateAndWait(Func<bool> function, TimeSpan retryInterval)
        {
            //Call the passed function and evaluate the result
            if (function())
                return true;
            Thread.Sleep(retryInterval);
            return false;
        }

        #endregion
    }
}