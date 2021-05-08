// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 17:01
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Toolbox.Utils.Common;

namespace Toolbox.Utils.Test.Tests.Common
{
    [TestFixture]
    public class ExtensionTests
    {
        #region Tests

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

        [Test]
        public void ContentOfStreamIsCopiedToArray()
        {
            var data = new byte[]
            {
                1, 2, 3, 4
            };
            
            using (Stream s = new MemoryStream())
            {
                s.Write(data, 0, data.Length);

                var target = s.ToArray(0);
                
                CollectionAssert.AreEqual(data, target);
            }
        }

        [Test]
        public void ContentOfStreamIsCopiedFromCurrentPosition()
        {
            var data = new byte[]
            {
                1, 2, 3, 4
            };

            using (Stream s = new MemoryStream())
            {
                s.Write(data, 0, data.Length);

                s.Position = 2;
                
                var target = s.ToArray();
                
                CollectionAssert.AreEqual(new byte[]{3,4}, target);
            }
        }

        #endregion
    }
}