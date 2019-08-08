// ===================================================================================================
// = Author      :  Mike
// = Created     :  2019-08-08 20:05
// ===================================================================================================
// = Description : Tests the 'AreWe' class
// ===================================================================================================

using Com.Toolbox.Utils.Probing;
using NUnit.Framework;

namespace Toolbox.Utils.Test.Tests.Probing
{
    [TestFixture]
    public class AreWeTest
    {
        #region Tests

        [Test]
        [Description("Passes a 'null' value as argument to 'AreWe.AbleToWrite'")]
        public void AreWeAbleToWriteNull()
        {
            var expectedResult = false;

            var actualResult = AreWe.AbleToWrite(null, false);

            Assert.AreEqual(expectedResult, actualResult, "Passing 'null' to 'AreWe.AbleToWrite' returned 'true'");
        }

        #endregion
    }
}