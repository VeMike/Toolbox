// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-01-24 13:51
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.UrlParser.Parsing;

namespace Toolbox.UrlParser.Test.Tests.Parsing
{
    [TestFixture]
    public class UrlParserTests
    {
        [Test]
        public void ConstructorThrowsOnNullArgument()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentNullException>((() =>
                {
                    var _ = new Parser(null);
                }));

                Assert.Throws<ArgumentException>(() =>
                {
                    var _ = new Parser(string.Empty);
                });
            });
        }
        
        [Test]
        public void ParseThrowsOnNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Parser("http://127.0.0.1:8000").Parse(null);
            });
        }

        [Test]
        public void UrlWithSinglePathParameterIsParsed()
        {
            var parser = new Parser("http://127.0.0.1:8000/items/{item_id}");

            var result = parser.Parse("http://127.0.0.1:8000/items/5");

            var parameter = result.PathParameters[0];
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(parameter.Name, "item_id");
                Assert.AreEqual(parameter.Value, "5");
            });
        }
    }
}