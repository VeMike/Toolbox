// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-10 20:46
// ===================================================================================================
// = Description :
// ===================================================================================================

using NUnit.Framework;
using Toolbox.Utils.Common;

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

        #endregion
    }
}