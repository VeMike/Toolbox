// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-05-08 14:54
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Linq;
using NUnit.Framework;
using Toolbox.Utils.Common;

namespace Toolbox.Utils.Test.Tests.Common
{
    [TestFixture]
    public class ObjectExtensionTests
    {
        [Test]
        public void SingleObjectIsEnumerated()
        {
            const string STRING_TO_ENUMERATE = "Foo";

            var enumeratedString = STRING_TO_ENUMERATE.ToEnumerable();
            
            CollectionAssert.Contains(enumeratedString, STRING_TO_ENUMERATE);
        }

        [Test]
        public void ObjectIsEnumeratedJustOnce()
        {
            const string STRING_TO_ENUMERATE = "Foo";

            var enumeratedString = STRING_TO_ENUMERATE.ToEnumerable().ToList();
            
            Assert.AreEqual(1, enumeratedString.Count);
        }
    }
}