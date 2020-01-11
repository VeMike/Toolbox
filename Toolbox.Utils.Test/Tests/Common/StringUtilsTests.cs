// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-10 20:46
// ===================================================================================================
// = Description :
// ===================================================================================================

using Com.Toolbox.Utils.Common;
using NUnit.Framework;

namespace Toolbox.Utils.Test.Tests.Common
{
    [TestFixture]
    public class StringUtilsTests
    {
        #region Tests

        [Test]
        public void SingleStringIsCombinedToUrl()
        {
            const string expected = "/SomeString";

            var actual = StringUtils.UrlCombine("SomeString");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MultipleStringsAreCombinedToUrl()
        {
            const string expected = "/Some/More/Strings/To/Combine";

            var actual = StringUtils.UrlCombine("Some", "More", "Strings", "To", "Combine");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NullsAndEmptyStringsAreIgnored()
        {
            const string expected = "/Hello/World";

            var actual = StringUtils.UrlCombine("", "Hello", null, "World", null, "");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LeadingAndTrailingSlashesAreStripped()
        {
            const string expected = "/The/Path/To/Some/Resource";

            var actual = StringUtils.UrlCombine("/The", "//Path", "To", "Some/", "/Resource//");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ReturnsEmptyStringsIfAllParametersAreInvalid()
        {
            var actual = StringUtils.UrlCombine(null, "");

            Assert.AreEqual(string.Empty, actual);
        }

        #endregion
    }
}