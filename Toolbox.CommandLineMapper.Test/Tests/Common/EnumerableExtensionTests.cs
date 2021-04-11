// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-22 18:04
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Toolbox.CommandLineMapper.Common;

namespace Toolbox.CommandLineMapper.Test.Tests.Common
{
    [TestFixture]
    public class EnumerableExtensionTests
    {
        #region Tests
        
        [TestCase(null, "-")]
        [TestCase("", "-")]
        [TestCase(" ", null)]
        [TestCase(" ", "")]
        public void SplitArgumentsThrowsIfArgumentsAreInvalid(string splitChar, string prefix)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var simpleSplit = "-String To -Split".SimpleSplitArguments(splitChar);

                var _ = simpleSplit.ToArgument(prefix).ToList();
            });
        }

        [TestCaseSource(typeof(EnumerableExtensionTests), nameof(SplitArgumentTestCases))]
        public List<Argument> SplitArgumentsSplitsString(string argumentString, string prefix)
        {
            var list = argumentString.SimpleSplitArguments().ToArgument(prefix).ToList();

            return list;
        }

        #endregion

        #region Test Data

        /// <summary>
        ///     Test case data for <see cref="SplitArgumentsSplitsString"/>
        /// </summary>
        public static IEnumerable SplitArgumentTestCases
        {
            get
            {
                yield return new TestCaseData("-a value1 -b value2 -c", "-").Returns(new List<Argument>
                {
                    new Argument("-", "-a", "value1"),
                    new Argument("-", "-b", "value2"),
                    new Argument("-", "-c")
                });
                
                yield return new TestCaseData("-a value1 -b value2", "-").Returns(new List<Argument>
                {
                    new Argument("-", "-a", "value1"),
                    new Argument("-", "-b", "value2")
                });
                
                yield return new TestCaseData("-c -a value1 -b value2", "-").Returns(new List<Argument>
                {
                    new Argument("-", "-c"),
                    new Argument("-", "-a", "value1"),
                    new Argument("-", "-b", "value2")
                });
                
                yield return new TestCaseData("-c -a value1 -b value2 -d", "-").Returns(new List<Argument>
                {
                    new Argument("-", "-c"),
                    new Argument("-", "-a", "value1"),
                    new Argument("-", "-b", "value2"),
                    new Argument("-", "-d")
                });
                
                yield return new TestCaseData("-a value1 -c -b value2 -d value3", "-").Returns(new List<Argument>
                {
                    new Argument("-", "-a", "value1"),
                    new Argument("-", "-c"),
                    new Argument("-", "-b", "value2"),
                    new Argument("-", "-d", "value3")
                });
                
                yield return new TestCaseData("-a -b -c -d value1", "-").Returns(new List<Argument>
                {
                    new Argument("-", "-a"),
                    new Argument("-", "-b"),
                    new Argument("-", "-c"),
                    new Argument("-", "-d", "value1")
                });
                
                yield return new TestCaseData("-d value1 -a -b -c -d", "-").Returns(new List<Argument>
                {
                    new Argument("-", "-d", "value1"),
                    new Argument("-", "-a"),
                    new Argument("-", "-b"),
                    new Argument("-", "-c"),
                    new Argument("-", "-d")
                });
            }
        }

        #endregion
    }
}