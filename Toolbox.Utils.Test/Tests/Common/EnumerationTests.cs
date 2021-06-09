// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-23 17:19
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Linq;
using NUnit.Framework;
using Toolbox.Utils.Common;
using Toolbox.Utils.Test.MockObjects.Common;

namespace Toolbox.Utils.Test.Tests.Common
{
    [TestFixture]
    public class EnumerationExtensionTests
    {
        #region Tests

        [TestCase(NumbersWithDescription.ONE, ExpectedResult = "First")]
        [TestCase(NumbersWithDescription.TWO, ExpectedResult = "Second")]
        [TestCase(NumbersWithDescription.THREE, ExpectedResult = "Third")]
        public string DescriptionAttributeIsAccessedFromEnum(NumbersWithDescription number)
        {
            return number.GetDescription();
        }
        
        [TestCase(NumbersWithoutDescription.ONE, ExpectedResult = "ONE")]
        [TestCase(NumbersWithoutDescription.TWO, ExpectedResult = "TWO")]
        [TestCase(NumbersWithoutDescription.THREE, ExpectedResult = "THREE")]
        public string EnumValueNameIsReturnedIfDescriptionNotPresent(NumbersWithoutDescription number)
        {
            return number.GetDescription();
        }

        #endregion
    }

    [TestFixture]
    public class EnumUtilTests
    {
        #region Tests

        [Test]
        public void GetValuesYieldsEmptyEnumerationIfEnumIsEmpty()
        {
            var result = EnumUtil.GetValues<EmptyEnumeration>();
            
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void GetValuesYieldsAllEnumValues()
        {
            var result = EnumUtil.GetValues<NumbersWithoutDescription>();

            var allValuesEnumerated = result.All(e => Enum.IsDefined(typeof(NumbersWithoutDescription), 
                                                                                                      e));
            
            Assert.IsTrue(allValuesEnumerated);
        }

        [TestCase(Foo.ONE, ExpectedResult = Bar.ONE)]
        [TestCase(Foo.TWO, ExpectedResult = Bar.TWO)]
        [TestCase(Foo.THREE, ExpectedResult = Bar.THREE)]
        public Bar EnumsAreConvertedIfTheyHaveTheSameName(Foo source)
        {
            return EnumUtil.Convert<Foo, Bar>(source);
        }
        
        [Test]
        public void EnumsCanNotBeConvertedIfTheNamesDoNotMatch()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _ = EnumUtil.Convert<Foo, Bar>(Foo.NOT_DEFINED_IN_BAR);
            });
        }

        #endregion

        #region Nested

        /// <summary>
        ///     A mock enumeration
        /// </summary>
        public enum Foo
        {
            ONE,
            
            TWO,
            
            THREE,
            
            NOT_DEFINED_IN_BAR
        }

        /// <summary>
        ///     A mock enumeration
        /// </summary>
        public enum Bar
        {
            THREE,
            
            TWO,
            
            ONE
        }

        #endregion
    }
}