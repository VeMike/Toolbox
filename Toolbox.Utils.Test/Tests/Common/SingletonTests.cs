// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-23 17:39
// ===================================================================================================
// = Description :
// ===================================================================================================

using NUnit.Framework;
using Toolbox.Utils.Test.MockObjects.Common;

namespace Toolbox.Utils.Test.Tests.Common
{
    [TestFixture]
    public class SingletonTests
    {
        #region Tests

        [Test]
        public void SingletonInstanceIsCreated()
        {
            var instance = SingletonObject.Instance;
            
            Assert.IsNotNull(instance);
        }

        [Test]
        public void SameInstanceIsReturnedOfSingletonInstance()
        {
            var firstInstance = SingletonObject.Instance;

            var secondInstance = SingletonObject.Instance;
            
            Assert.AreSame(firstInstance, secondInstance);
        }

        #endregion
    }
}