// ===================================================================================================
// = Author      :  Mike
// = Created     :  2019-08-10 16:30
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Com.Toolbox.Utils.ControlFlow;
using NUnit.Framework;

namespace Toolbox.Utils.Test.Tests.Probing
{
    [TestFixture]
    public class RetryTest
    {
        #region Tests

        [Test]
        [Description("Calls 'Retry.Do()' with a function that always returns false")]
        public void RetryDoBoolAllAttemptsFail()
        {
            Assert.Throws<RetryFailedException>(() => Retry.Do(() => false, 
                                                               TimeSpan.FromMilliseconds(0),
                                                               2),
                                                "'Retry.Do' did not throw after max. attempts were reached");
        }

        [Test]
        [Description("Calls 'Retry.Do()' with a function that always returns true")]
        public void RetryDoBoolAttemptSucceeds()
        {
            Assert.DoesNotThrow(() => Retry.Do(() => true, 
                                               TimeSpan.FromMilliseconds(0),
                                               2),
                                "'Retry.Do' did throw before max. attempts were reached");
        }

        [Test]
        [Description("Calls 'Retry.Do' with a function that always returns false with 3 retries")]
        public void RetryDoBoolMaxAttemptsReportedCorrectly()
        {
            var expectedAttempts = 3;

            try
            {
                Retry.Do(() => false,
                         TimeSpan.FromMilliseconds(0),
                         expectedAttempts);
            }
            catch (RetryFailedException e)
            {
                int actualAttempts = e.Attempts;

                Assert.AreEqual(expectedAttempts, actualAttempts);

                return;
            }

            Assert.Fail("No exception was thrown even though the max attempts were reached");
        }

        #endregion
    }
}