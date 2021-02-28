// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 19:09
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.IniFileParser.Parsing.Files;

namespace Toolbox.IniFileParser.Test.Tests.Parsing.Files
{
    [TestFixture]
    public class LineTests
    {
        [Test]
        public void ConstructorThrowsOnNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Line(0, null);
            });
        }
    }
}