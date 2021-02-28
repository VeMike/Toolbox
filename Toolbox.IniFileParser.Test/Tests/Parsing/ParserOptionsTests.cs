// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 18:01
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Collections.Generic;
using NUnit.Framework;
using Toolbox.IniFileParser.Parsing;
using Toolbox.IniFileParser.Parsing.Files;

namespace Toolbox.IniFileParser.Test.Tests.Parsing
{
    [TestFixture]
    public class ParserOptionsTests
    {
        [Test]
        public void DefaultValuesOfParserOptionsAreCorrectlyReturned()
        {
            var expected = new List<string>
            {
                ";",
                "#",
                "//"
            };
            
            var options = new ParserOptions();
            
            CollectionAssert.AreEqual(expected, options.CommentChars);
        }

        [Test]
        public void SectionStartValueIsCorrectlyReturned()
        {
            var options = new ParserOptions();
            
            Assert.AreEqual("[", options.SectionStart);
        }
        
        [Test]
        public void SectionEndValueIsCorrectlyReturned()
        {
            var options = new ParserOptions();
            
            Assert.AreEqual("]", options.SectionEnd);
        }
        
        [Test]
        public void PropertySeparatorValueIsCorrectlyReturned()
        {
            var options = new ParserOptions();
            
            Assert.AreEqual("=", options.PropertySeparator);
        }
    }
}