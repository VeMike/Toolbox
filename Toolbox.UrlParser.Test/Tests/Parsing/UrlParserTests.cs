// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-01-24 13:51
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
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
        public void ConstructorThrowsIfHostIsSpecified()
        {
            Assert.Throws<UriFormatException>(() =>
            {
                var _ = new Parser("http://localhost/items/{item_id}");
            });
        }

        [Test]
        public void ParseThrowsIfHostIsSpecified()
        {
            Assert.Throws<UriFormatException>(() =>
            {
                new Parser("/items/{item_id}").Parse("http://localhost/items/5");
            });
        }
        
        [Test]
        public void ParseThrowsOnNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Parser("/items/{item_id}").Parse(null);
            });
        }

        [Test]
        public void ParseThrowsIfUrlDoesNotMatchTheAmountOfSegmentsInThePattern()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Parser("/items/{item_id}").Parse("/items/{item_id}/product");
            });
        }

        [Test]
        public void ParseThrowsIfUrlDoesNotMatchTheSegmentValuesInThePattern()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Parser("/items/{item_id}").Parse("/orders/{item_id}");
            });
        }

        [Test]
        public void NoPathParametersAreParsedIfUrlDoesNotDefinePatters()
        {
            var result = new Parser("/items/number").Parse("/orders/{item_id}");
            
            CollectionAssert.IsEmpty(result.PathParameters);
        }

        [Test]
        public void UrlWithSinglePathParameterIsParsed()
        {
            var parser = new Parser("/items/{item_id}");
            var result = parser.Parse("/items/5");

            var parameter = result.PathParameters[0];
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("item_id", parameter.Name);
                Assert.AreEqual("5", parameter.Value);
            });
        }

        [Test]
        public void UrlWithMultiplePathParametersIsParsed()
        {
            var parser = new Parser("/users/{user_id}/roles/{role_id}");
            var result = parser.Parse("/users/10/roles/42");

            var expectedParameters = new List<Parameter>
            {
                new Parameter("user_id", "10", 2),
                new Parameter("role_id", "42", 4),
            };
            
            CollectionAssert.AreEqual(expectedParameters, result.PathParameters);
        }

        [Test]
        public void UrlWithSingleQueryParametersIsParsed()
        {
            var parser = new Parser("/items/item");
            var result = parser.Parse("/items/5?skip=0");
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("skip", result.QueryParameters[0].Name);
                Assert.AreEqual("0", result.QueryParameters[0].Value);
            });
        }

        [Test]
        public void UrlWithMultipleQueryParametersIsParsed()
        {
            var parser = new Parser("/items/item");
            var result = parser.Parse("/items/5?skip=0&count=23");
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual("skip", result.QueryParameters[0].Name);
                Assert.AreEqual("0", result.QueryParameters[0].Value);
                
                Assert.AreEqual("count", result.QueryParameters[1].Name);
                Assert.AreEqual("23", result.QueryParameters[1].Value);
            });
        }

        [Test]
        public void UrlWithNoQueryParametersYieldsEmptyList()
        {
            var parser = new Parser("/items/item");
            var result = parser.Parse("/items/5");
            
            CollectionAssert.IsEmpty(result.QueryParameters);
        }
    }
}