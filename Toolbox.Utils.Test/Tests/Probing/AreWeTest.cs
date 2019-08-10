// ===================================================================================================
// = Author      :  Mike
// = Created     :  2019-08-08 20:05
// ===================================================================================================
// = Description : Tests the 'AreWe' class
// ===================================================================================================

using System.IO;
using Com.Toolbox.Utils.Probing;
using NUnit.Framework;

namespace Toolbox.Utils.Test.Tests.Probing
{
    [TestFixture]
    public class AreWeTest
    {
        [Test]
        [Description("Passes an empty string as argument to 'AreWe.AbleToWrite'")]
        public void AreWeAbleToWriteEmptyString()
        {
            var actualResult = AreWe.AbleToWrite(string.Empty, false);

            Assert.IsFalse(actualResult, "Passing an empty string to 'AreWe.AbleToWrite' returned 'true'");
        }

        [Test]
        [Description("Passes a non-existing path to 'AreWe.AbleToWrite' without throwing")]
        public void AreWeAbleToWriteNonExistingDir()
        {
            var actualResult = AreWe.AbleToWrite("C:\\this\\is\\a\\path\\that\\does\\not\\exist",
                                                 false);

            Assert.IsFalse(actualResult, "Passing a non-existing dir to 'AreWe.AbleToWrite' returned 'true'");
        }

        [Test]
        [Description("Passes a non-existing path to 'AreWe.AbleToWrite' with throwing")]
        public void AreWeAbleToWriteNonExistingDirThrow()
        {
            void ThrowingDelegate()
            {
                AreWe.AbleToWrite("C:\\this\\is\\a\\path\\that\\does\\not\\exist", true);
            }

            Assert.Throws<DirectoryNotFoundException>(ThrowingDelegate);
        }

        [Test]
        [Description("Passes a 'null' value as argument to 'AreWe.AbleToWrite'")]
        public void AreWeAbleToWriteNull()
        {
            var actualResult = AreWe.AbleToWrite(null, false);

            Assert.IsFalse(actualResult, "Passing 'null' to 'AreWe.AbleToWrite' returned 'true'");
        }
    }
}