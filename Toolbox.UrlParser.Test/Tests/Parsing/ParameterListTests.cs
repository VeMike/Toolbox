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
    }
}