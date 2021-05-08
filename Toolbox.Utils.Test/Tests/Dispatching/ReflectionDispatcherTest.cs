// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 17:43
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.Utils.Dispatching;

namespace Toolbox.Utils.Test.Tests.Dispatching
{
    [TestFixture]
    public class ReflectionDispatcherTest
    {
        [Test]
        public void ConstructorThrowsIfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ReflectionDispatcher(null);
            });
        }

        [Test]
        public void AssemblyPropertyThrowsOnNullAssignment()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ReflectionDispatcher {Assembly = null};
            });
        }
    }
}