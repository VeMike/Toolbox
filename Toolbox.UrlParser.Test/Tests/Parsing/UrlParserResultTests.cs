// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-01-24 13:59
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.UrlParser.Parsing;

namespace Toolbox.UrlParser.Test.Tests.Parsing
{
    [TestFixture]
    public class UrlParserResultTests
    {
        [Test]
        public void ConstructorThrowsOnNullArgument()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new UrlParserResult(null, new ParameterList(), new ParameterList());
                });
                
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new UrlParserResult("Foo", null, new ParameterList());
                });
                
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new UrlParserResult("Foo", new ParameterList(), null);
                });
            });
        }
    }
}