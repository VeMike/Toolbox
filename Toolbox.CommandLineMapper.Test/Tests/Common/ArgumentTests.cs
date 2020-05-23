// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-22 16:48
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.CommandLineMapper.Common;

namespace Toolbox.CommandLineMapper.Test.Tests.Common
{
    [TestFixture]
    public class ArgumentTests
    {
        #region Tests

        [TestCase(null, "SomeValue")]
        [TestCase("SomeCommand", null)]
        public void ConstructorThrowsIfArgumentsAreNull(string command, string prefix)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Argument(command, prefix);
            });
        }

        [Test]
        public void CommandWithoutValueHasNoValue()
        {
            var arg = new Argument("-", "bar");
            
            Assert.IsFalse(arg.HasValue);
        }

        [Test]
        public void CommandWithValueHasValue()
        {
            var arg = new Argument("-", "bar", "foo");
            
            Assert.IsTrue(arg.HasValue);
        }

        [Test]
        public void CommandPrefixIsRemoved()
        {
            var arg = new Argument("-", "-bar", "foo");
            
            Assert.AreEqual("bar", arg.CommandWithoutPrefix);
        }

        [Test]
        public void CommandPrefixIsNotRemovedIfNotPresent()
        {
            var arg = new Argument("-", "bar", "foo");
            
            Assert.AreEqual("bar", arg.CommandWithoutPrefix);
        }

        #endregion
    }
}