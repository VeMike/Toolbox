// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-21 17:21
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using NUnit.Framework;
using Toolbox.CommandLineMapper.Common;

namespace Toolbox.CommandLineMapper.Test.Tests.Common
{
    [TestFixture]
    public class StringExtensionTests
    {
        #region Tests

        [TestCase(null)]
        [TestCase("")]
        public void SimpleSplitArgumentsThrowsIfSplitcharIsInvalid(string param)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                "String To Split".SimpleSplitArguments(param);
            });
        }

        [Test]
        public void SimpleSplitArgumentsSkipsValues()
        {
            var args = "-p FirstArgument -a SecondArgument -b ThirdArgument";

            var split = args.SimpleSplitArguments(skip:1);
            
            CollectionAssert.DoesNotContain(split, "-p");
        }

        [Test]
        public void SimpleSplitArgumentsReturnsEmptyCollectionIfAllAreSkipped()
        {
            var args = "-p FirstArgument -a SecondArgument -b ThirdArgument";

            var split = args.SimpleSplitArguments(skip:10);
            
            CollectionAssert.IsEmpty(split);
        }
        
        #endregion
    }
}