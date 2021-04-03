// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-08 19:13
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Com.Toolbox.Utils.Resource;
using NUnit.Framework;

namespace Toolbox.Utils.Test.Tests.Memory
{
    [TestFixture]
    public class FileZipperTests
    {
        #region Attributes

        private TempFileCollection tempTestFiles;

        private FileInfo singleZipFile;

        private const int NUMBER_OF_TEMPFILES = 10;

        private const string FILE_TEXT = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam";

        #endregion

        #region Setup/Teardown

        [OneTimeSetUp]
        public void InitializeFixture()
        {
            this.tempTestFiles = new TempFileCollection();

            foreach (var _ in Enumerable.Range(0, NUMBER_OF_TEMPFILES))
            {
                var path = Path.GetTempFileName();
                
                this.tempTestFiles.AddFile(path, false);
                
                File.WriteAllLines(path, Enumerable.Repeat(FILE_TEXT, 100));
            }
        }

        [SetUp]
        public void SetupTest()
        {
            this.singleZipFile = new FileInfo(Path.GetTempFileName());
        }

        [TearDown]
        public void FinalizeTest()
        {
            if(this.singleZipFile.Exists)
                this.singleZipFile.Delete();
        }
        
        [OneTimeTearDown]
        public void FinalizeFixture()
        {
            this.tempTestFiles.Delete();
        }

        #endregion

        #region Tests

        [Test]
        public void ZipFilesThrowsIfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = FileZipper.ZipFiles(null);
            });
        }

        [Test]
        public void ZipFilesBoolThrowsIfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = FileZipper.ZipFiles(null, true);
            });
        }

        [TestCaseSource(typeof(FileZipperTests), nameof(GetInvalidCollecton))]
        public void ZipFilesThrowsIfCollectionIsEmpty(IEnumerable<FileInfo> files)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var _ = FileZipper.ZipFiles(files);
            });
        }
        
        [TestCaseSource(typeof(FileZipperTests), nameof(GetInvalidCollecton))]
        public void ZipFilesBoolThrowsIfCollectionIsEmpty(IEnumerable<FileInfo> files)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var _ = FileZipper.ZipFiles(files);
            });
        }

        [Test]
        public void ZipArchiveIsCreatedFromValidFiles()
        {
            using (var stream = FileZipper.ZipFiles(this.EnumerateFileOfTempCollection()))
            {
                File.WriteAllBytes(this.singleZipFile.FullName, stream.ToArray());
                
                Assert.IsTrue(this.singleZipFile.Exists);
            }
        }

        [Test]
        public void UnzipFilesThrowsOnNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = FileZipper.UnzipFiles(null).ToList();
            });
        }

        [Test]
        public void UnizipFilesThrowsIfStreamIsEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                using (var stream = new MemoryStream())
                {
                    var _ = FileZipper.UnzipFiles(stream).ToList();
                }
            });
        }

        [Test]
        public void FilesOfArchiveAreWrittenIntoMemory()
        {
            var unzippedFiles = new List<FileInfo>();
            
            using (var archive = this.CreateArchiveFromTemporaryFiles())
            {
                foreach (var singleFileStream in FileZipper.UnzipFiles(archive))
                {
                    using (singleFileStream)
                    {
                        var path = Path.GetTempFileName();
                        this.tempTestFiles.AddFile(path, false);
                        File.WriteAllBytes(path, singleFileStream.ToArray());
                        unzippedFiles.Add(new FileInfo(path));
                    }
                }
            }
            
            Assert.IsTrue(unzippedFiles.All(f => f.Exists));
        }

        #endregion

        /// <summary>
        ///     Gets collections of files that are not valid and
        ///     should throw exceptions when passed to the
        ///     <see cref="FileZipper"/>
        /// </summary>
        private static IEnumerable<IEnumerable<FileInfo>> GetInvalidCollecton
        {
            get
            {
                yield return Enumerable.Empty<FileInfo>();
                
                yield return new List<FileInfo>{new FileInfo("DoesNotExistInThisDirectory.txt")};
            }
        }

        /// <summary>
        ///     Enumerates all files currently held by the <see cref="TempFileCollection"/>
        ///     of this instance
        /// </summary>
        /// <returns>
        ///    ALl files inside this instances <see cref="TempFileCollection"/>
        /// </returns>
        private IEnumerable<FileInfo> EnumerateFileOfTempCollection()
        {
            foreach (var file in this.tempTestFiles.Cast<string>())
            {
                yield return new FileInfo(file);
            }
        }

        /// <summary>
        ///     Creates a new zip archive in memory from the temporary
        ///     files held by this instance 
        /// </summary>
        /// <returns>
        ///    A <see cref="MemoryStream"/> containing the created zip archive
        /// </returns>
        private MemoryStream CreateArchiveFromTemporaryFiles()
        {
            return FileZipper.ZipFiles(this.EnumerateFileOfTempCollection());
        }
    }
}