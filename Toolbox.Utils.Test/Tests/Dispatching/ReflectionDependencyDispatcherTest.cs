// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 20:11
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;
using NUnit.Framework;
using Toolbox.Utils.Dispatching;
using Toolbox.Utils.Test.MockObjects.Dispatcher;

namespace Toolbox.Utils.Test.Tests.Dispatching
{
    [TestFixture]
    public class ReflectionDependencyDispatcherTest
    {
        [Test]
        public void ConstructorThrowsIfDependencyIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ReflectionDependencyDispatcher<string>(null);
            });
        }

        [Test]
        public void ConstructorThrowsIfAssemblyIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ReflectionDependencyDispatcher<string>("Foo", null);
            });
        }

        [Test]
        public void DependencyPropertyThrowsOnNullAssignment()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ReflectionDependencyDispatcher<string>("Foo")
                {
                    Dependency = null
                };
            });
        }

        [Test]
        public void DependencyIsInjectedBeforeHandleIsCalled()
        {
            var dispatcher = new ReflectionDependencyDispatcher<string>("Foo", Assembly.GetExecutingAssembly());

            var command = new InjectBeforeHandleCommand();
            
            dispatcher.Dispatch(command);
            
            Assert.IsTrue(command.InjectBeforeHandleCalled);
        }

        [Test]
        public void DependencyValueIsInjected()
        {
            var dispatcher = new ReflectionDependencyDispatcher<string>("Hello", Assembly.GetExecutingAssembly());

            var command = new GetInjectedDependencyCommand();
            
            dispatcher.Dispatch(command);
            
            Assert.AreEqual("Hello", command.Dependency);
        }
    }
}