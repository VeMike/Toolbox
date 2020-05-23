// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-23 17:19
// ===================================================================================================
// = Description :
// ===================================================================================================

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
}