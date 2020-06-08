// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-08 16:14
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Com.Toolbox.Utils.Dispatching;
using NUnit.Framework;
using Toolbox.Utils.Test.MockObjects.Dispatcher;

namespace Toolbox.Utils.Test.Tests.Dispatching
{
    [TestFixture]
    public class RegistrationDependencyDispatcherTests
    {
        #region Tests

        [Test]
        public void AddHandlerThrowsIfArgumentIsNUll()
        {
            var dispatcher = new RegistrationDependencyDispatcher<string>("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                dispatcher.AddHandler<object>(null);
            });
        }

        [Test]
        public void SameHandlerIsOnlyAddedOnce()
        {
            var dispatcher = new RegistrationDependencyDispatcher<string>("Foo");
            
            var handler = new CountingCallsDependencyCommandHandler();
            dispatcher.AddHandler(handler);
            dispatcher.AddHandler(handler);
            
            dispatcher.Dispatch(new EmptyCommand());
            
            Assert.AreEqual(1, CountingCallsDependencyCommandHandler.Calls);
        }

        #endregion
    }
}