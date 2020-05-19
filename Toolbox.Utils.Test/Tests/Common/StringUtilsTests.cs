// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-10 20:46
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Com.Toolbox.Utils.Common;
using NUnit.Framework;

namespace Toolbox.Utils.Test.Tests.Common
{
    [TestFixture]
    public class StringUtilsTests
    {
        #region Tests

        #region UrlCombine

        [Test]
        public void SingleStringIsCombinedToUrl()
        {
            const string EXPECTED = "/SomeString";

            var actual = StringUtils.UrlCombine("SomeString");

            Assert.AreEqual(EXPECTED, actual);
        }

        [Test]
        public void MultipleStringsAreCombinedToUrl()
        {
            const string EXPECTED = "/Some/More/Strings/To/Combine";

            var actual = StringUtils.UrlCombine("Some", "More", "Strings", "To", "Combine");

            Assert.AreEqual(EXPECTED, actual);
        }

        [Test]
        public void NullsAndEmptyStringsAreIgnored()
        {
            const string EXPECTED = "/Hello/World";

            var actual = StringUtils.UrlCombine("", "Hello", null, "World", null, "");

            Assert.AreEqual(EXPECTED, actual);
        }

        [Test]
        public void LeadingAndTrailingSlashesAreStripped()
        {
            const string EXPECTED = "/The/Path/To/Some/Resource";

            var actual = StringUtils.UrlCombine("/The", "//Path", "To", "Some/", "/Resource//");

            Assert.AreEqual(EXPECTED, actual);
        }

        [Test]
        public void ReturnsEmptyStringsIfAllParametersAreInvalid()
        {
            var actual = StringUtils.UrlCombine(null, "");

            Assert.AreEqual(string.Empty, actual);
        }

        #endregion

        #region TrimStart, TrimEnd

        [Test]
        public void TrimStartThrowsOnNullOrEmptySourceString()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    StringUtils.TrimStart(null, "SomeString");
                });

                Assert.Throws<ArgumentException>(() =>
                {
                    StringUtils.TrimStart(string.Empty, "SomeString");
                });

            });
        }

        [Test]
        public void TrimStartReturnsUnchangedStringIfRemoveStringIsNullOrEmpty()
        {
            const string EXPECTED_STRING = "TheStringValue";

            var actualForNull = StringUtils.TrimStart(EXPECTED_STRING, null);
            var actualForEmpty = StringUtils.TrimStart(EXPECTED_STRING, string.Empty);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(EXPECTED_STRING, actualForNull);
                Assert.AreEqual(EXPECTED_STRING, actualForEmpty);
            });
        }

        [Test]
        public void TrimStartTrimsSpecifiedValue()
        {
            const string EXPECTED_STRING = "Value";

            var actualString = StringUtils.TrimStart("TheStringValue", "TheString");

            Assert.AreEqual(EXPECTED_STRING, actualString);
        }

        [Test]
        public void TrimStartDoesNotChangeStringIfRemoveStringNotFound()
        {
            const string EXPECTED_STRING = "TheStringValue";

            var actualString = StringUtils.TrimStart(EXPECTED_STRING, "NotInSourceString");

            Assert.AreEqual(EXPECTED_STRING, actualString);
        }
        
        [Test]
        public void TrimEndThrowsOnNullOrEmptySourceString()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    StringUtils.TrimEnd(null, "SomeString");
                });

                Assert.Throws<ArgumentException>(() =>
                {
                    StringUtils.TrimEnd(string.Empty, "SomeString");
                });

            });
        }

        [Test]
        public void TrimEndReturnsUnchangedStringIfRemoveStringIsNullOrEmpty()
        {
            const string EXPECTED_STRING = "TheStringValue";

            var actualForNull = StringUtils.TrimEnd(EXPECTED_STRING, null);
            var actualForEmpty = StringUtils.TrimEnd(EXPECTED_STRING, string.Empty);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(EXPECTED_STRING, actualForNull);
                Assert.AreEqual(EXPECTED_STRING, actualForEmpty);
            });
        }

        [Test]
        public void TrimEndTrimsSpecifiedValue()
        {
            const string EXPECTED_STRING = "The";

            var actualString = StringUtils.TrimEnd("TheStringValue", "StringValue");

            Assert.AreEqual(EXPECTED_STRING, actualString);
        } 

        [Test]
        public void TrimEndDoesNotChangeStringIfRemoveStringNotFound()
        {
            const string EXPECTED_STRING = "TheStringValue";

            var actualString = StringUtils.TrimEnd(EXPECTED_STRING, "NotInSourceString");

            Assert.AreEqual(EXPECTED_STRING, actualString);
        }

        #endregion

        #endregion
    }
}