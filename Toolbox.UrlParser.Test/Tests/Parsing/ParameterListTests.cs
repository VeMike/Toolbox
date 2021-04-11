// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-01-24 13:29
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.UrlParser.Parsing;

namespace Toolbox.UrlParser.Test.Tests.Parsing
{
    [TestFixture]
    public class ParameterListTests
    {
        [Test]
        public void ConstructorThrowsOnNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ParameterList(null);
            });
        }

        [Test]
        public void ParameterIsFoundByIndex()
        {
            var list = new ParameterList {new Parameter("Foo", "Bar", 5)};

            var result = list.TryGetParameter(5, out var param);
            
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result);
                
                Assert.IsNotNull(param);
            });
        }

        [Test]
        public void ParameterIsNotFoundByIndex()
        {
            var list = new ParameterList {new Parameter("Foo", "Bar", 5)};

            var result = list.TryGetParameter(9, out var param);
            
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result);
                
                Assert.IsNull(param);
            });
        }
    }
}