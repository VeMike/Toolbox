// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-03-13 16:59
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Toolbox.IniFileParser.Parsing.Files;
using Toolbox.IniFileParser.Test.Mock;

namespace Toolbox.IniFileParser.Test.Tests.Parsing.Files
{
    [TestFixture]
    public class IniFileTests
    {
        [Test]
        public void DefaultConstructorCreatesNewParserInstance()
        {
            var ini = new IniFile();
            
            Assert.IsNotNull(ini.Parser);
        }

        [Test]
        public void TextFileConstructorThrowsIfArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new IniFile(null);
            });
        }

        [Test]
        public void ParserAndFileConstructorThrowsIfArgumentIsNull()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new IniFile(null, new MockReadableTextFile(new List<string>()));
                });

                Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new IniFile(new Parser(), null);
                });
            });
        }
    }
}