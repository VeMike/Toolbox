// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-05-08 14:55
// ===================================================================================================
// = Description :
// ===================================================================================================

using NUnit.Framework;
using Toolbox.Utils.Common;

namespace Toolbox.Utils.Test.Tests.Common
{
    [TestFixture]
    public class StringExtensionTests
    {
        #region Tests

        [TestCase("0", false)]
        [TestCase("1", true)]
        [TestCase("Foo", 123)]
        public void TryParseReturnsFalseIfStringCanNotBeParsedToType<T>(string value, T target)
        {
            var result = value.TryParse<T>(out var parsed);
            
            Assert.IsFalse(result);
        }

        [TestCase("1.234", 1.234d)]
        [TestCase("9.8765", 9.8765f)]
        [TestCase("true", true)]
        [TestCase("false", false)]
        [TestCase("42", 42)]
        public void TryParseReturnsTrueAndValidResultIfStringCanBeParsed<T>(string value, T target)
        {
            var result = value.TryParse<T>(out var parsed);
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result);
                
                Assert.AreEqual(target, parsed);
            });
        }

        #endregion
        
    }
}