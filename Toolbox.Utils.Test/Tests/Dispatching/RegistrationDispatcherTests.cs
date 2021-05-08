// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-08 15:15
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.Utils.Dispatching;
using Toolbox.Utils.Test.MockObjects.Dispatcher;

namespace Toolbox.Utils.Test.Tests.Dispatching
{
    [TestFixture]
    public class RegistrationDispatcherTests
    {
        #region Tests

        [Test]
        public void AddHandlerThrowsIfArgumentIsNUll()
        {
            var dispatcher = new RegistrationDispatcher();

            Assert.Throws<ArgumentNullException>(() =>
            {
                dispatcher.AddHandler<object>(null);
            });
        }

        [Test]
        public void SameHandlerIsOnlyAddedOnce()
        {
            var dispatcher = new RegistrationDispatcher();
            
            var handler = new CountingCallsCommandHandler();
            dispatcher.AddHandler(handler);
            dispatcher.AddHandler(handler);
            
            dispatcher.Dispatch(new EmptyCommand());
            
            Assert.AreEqual(1, CountingCallsCommandHandler.Calls);
        }

        #endregion
    }
}