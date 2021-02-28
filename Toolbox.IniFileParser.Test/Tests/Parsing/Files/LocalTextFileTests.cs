// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 19:28
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Toolbox.IniFileParser.Parsing.Files;

namespace Toolbox.IniFileParser.Test.Tests.Parsing.Files
{
    [TestFixture]
    public class LocalTextFileTests
    {
        #region Attributes

        /// <summary>
        ///     A local text file whose contents
        ///     are read by tests
        /// </summary>
        private FileInfo readTextFile;

        /// <summary>
        ///     A local text file whose contents
        ///     will overridden by tests
        /// </summary>
        private FileInfo writeTextFile;

        /// <summary>
        ///     The contents of <see cref="readTextFile"/>
        /// </summary>
        private IList<string> contentsOfReadFile;
        
        #endregion
        
        #region Setup/Teardown

        [OneTimeSetUp]
        public void InitTest()
        {
            this.readTextFile = new FileInfo("Mock\\Files\\MockIniFile.txt");

            this.contentsOfReadFile = File.ReadAllLines(this.readTextFile.FullName);
        }

        #endregion
        
        #region Tests

        [Test]
        public void ConstructorThrowsOnNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new LocalTextFile(null);
            });
        }

        [Test]
        public void ConstructorThrowsIfFileDoesNotExist()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var _ = new LocalTextFile(new FileInfo("FooBarTextFile.txt"));
            });
        }

        [Test]
        public void ConstructorDosNotThrowIfFileExists()
        {
            Assert.DoesNotThrow(() =>
            {
                var _ = new LocalTextFile(this.readTextFile);
            });
        }

        [Test]
        public void ContentsOfLocalFileAreRead()
        {
            var file = new LocalTextFile(this.readTextFile);

            var content = file.ReadLines().Select(l => l.Content);
            
            CollectionAssert.AreEqual(this.contentsOfReadFile, content);
        }

        #endregion
    }
}