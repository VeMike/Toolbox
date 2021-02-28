// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 17:03
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.IniFileParser.Parsing;
using Toolbox.IniFileParser.Parsing.Events;

namespace Toolbox.IniFileParser.Test.Tests.Parsing.Events
{
    [TestFixture]
    public class ParserEventArgsTests
    {
        [Test]
        public void ConstructorThrowsOnNullArguments()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ParserEventArgs(0, null);
            });
        }
    }
}