// ===================================================================================================
// = Author      :  Mike
// = Created     :  2019-08-10 16:30
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.Utils.Probing;

namespace Toolbox.Utils.Test.Tests.Probing
{
    [TestFixture]
    public class RetryTest
    {
        #region Tests

        [Test]
        public void RetryTimeSpanAllAttemptsFail()
        {
            Assert.Throws<RetryFailedException>(() => Retry.Until(() => false, 
                                                               TimeSpan.Zero,
                                                               2),
                                                "'Retry.Do' did not throw after max. attempts were reached");
        }
        
        [Test]
        public void RetryImmediateAllAttemptsFail()
        {
            Assert.Throws<RetryFailedException>(() => Retry.Until(() => false,
                                                                  2),
                                                "'Retry.Do' did not throw after max. attempts were reached");
        }

        [Test]
        public void RetryTimeSpanAttemptSucceed()
        {
            Assert.DoesNotThrow(() => Retry.Until(() => true, 
                                               TimeSpan.Zero,
                                               2),
                                "'Retry.Do' did throw before max. attempts were reached");
        }
        
        [Test]
        public void RetryImmediateAttemptSucceed()
        {
            Assert.DoesNotThrow(() => Retry.Until(() => true,
                                                  2),
                                "'Retry.Do' did throw before max. attempts were reached");
        }

        [Test]
        public void RetryTimeSpanMaxAttemptsReportedCorrectly()
        {
            var expectedAttempts = 3;

            try
            {
                Retry.Until(() => false,
                         TimeSpan.Zero,
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