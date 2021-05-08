// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 17:16
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Toolbox.IniFileParser.Parsing.Events;

namespace Toolbox.IniFileParser.Test.Tests.Parsing.Events
{
    [TestFixture]
    public class PropertyEventArgsTests
    {
        [TestCase(null)]
        public void ConstructorThrowsOnNullArguments(string line)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new PropertyEventArgs(0, 
                                              line,
                                              new KeyValuePair<string, string>());
            });
        }
    }
}