// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-01-24 13:07
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.UrlParser.Parsing;

namespace Toolbox.UrlParser.Test.Tests.Parsing
{
    [TestFixture]
    public class ParameterTests
    {
        [Test]
        public void ConstructorThrowsOnNullArgument()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new Parameter(null, "Foo", 0);
                });

                Assert.Throws<ArgumentNullException>(() =>
                {
                    var _ = new Parameter("Foo", null, 0);
                });
            });
        }

        [Test]
        public void ConstructorThrowsOnEmptyArgument()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    var _ = new Parameter("", "Foo", 0);
                });
            });
        }

        [Test]
        public void ParameterValueCanBeLeftEmpty()
        {
            Assert.DoesNotThrow(() =>
            {
                var _ = new Parameter("Foo", "", 0);
            });
        }
    }
}