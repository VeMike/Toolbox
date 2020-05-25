// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-23 17:19
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Linq;
using Com.Toolbox.Utils.Common;
using NUnit.Framework;
using Toolbox.Utils.Test.MockObjects;

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

        #endregion
    }
}