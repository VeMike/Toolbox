// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 17:12
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.IniFileParser.Parsing.Events;

namespace Toolbox.IniFileParser.Test.Tests.Parsing.Events
{
    [TestFixture]
    public class ContentEventArgsTests
    {
        [TestCase(null, "Foo")]
        [TestCase("Foo", null)]
        public void ConstructorThrowsOnNullArguments(string line, string content)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ContentEventArgs(0, line, content);
            });
        }
    }
}