// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-21 17:21
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Linq;
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
        public void SplitArgumentsThrowsIfSplitcharIsInvalid(string param)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                "String To Split".SplitArguments(param);
            });
        }

        [Test]
        public void SplitArgumentsSkipsValues()
        {
            var args = "-p FirstArgument -a SecondArgument -b ThirdArgument";

            var split = args.SplitArguments(skip:1);
            
            CollectionAssert.DoesNotContain(split, "-p");
        }

        [Test]
        public void SplitArgumentsReturnsEmptyCollectionIfAllAreSkipped()
        {
            var args = "-p FirstArgument -a SecondArgument -b ThirdArgument";

            var split = args.SplitArguments(skip:10);
            
            CollectionAssert.IsEmpty(split);
        }

        #endregion
    }
}