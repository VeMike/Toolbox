// ===================================================================================================
// = Author      :  Mike
// = Created     :  2019-08-08 20:05
// ===================================================================================================
// = Description : Tests the 'AreWe' class
// ===================================================================================================

using System;
using System.IO;
using NUnit.Framework;
using Toolbox.Utils.Probing;

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
            Assert.Throws<DirectoryNotFoundException>(() =>
            {
                AreWe.AbleToWrite("C:\\this\\is\\a\\path\\that\\does\\not\\exist", 
                                  true);
            });
        }

        [Test]
        [Description("Passes a 'null' value as argument to 'AreWe.AbleToWrite'")]
        public void AreWeAbleToWriteNull()
        {
            var actualResult = AreWe.AbleToWrite(null, false);

            Assert.IsFalse(actualResult, "Passing 'null' to 'AreWe.AbleToWrite' returned 'true'");
        }

        [Test]
        [Description("Passes a path we can write to to 'AreWe.AbleToWrite'")]
        public void AreWeAbleToWriteWritablePath()
        {
            //The 'Documents' folder of the current user
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var ableToWrite = AreWe.AbleToWrite(path, false);

            Assert.IsTrue(ableToWrite, "'AreWe.AbleToWrite' is 'false' even though we are able to write to the path");
        }
    }
}