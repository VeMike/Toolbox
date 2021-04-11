// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-04-11 14:05
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Com.Toolbox.Utils.Collection;
using NUnit.Framework;

namespace Toolbox.Utils.Test.Tests.Collection
{
    [TestFixture]
    public class ExtensionTests
    {
        [Test]
        public void AddRangeThrowsIfArgumentIsNull()
        {
            IList<int> list = new List<int>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                list.AddRange(null);;
            });
        }

        [Test]
        public void AddRangeAddsARangeToAList()
        {
            IList<int> list = new List<int>();
            var elements = new List<int> { 1, 2, 3, 4 };
            
            list.AddRange(elements);
            
            CollectionAssert.AreEqual(elements, list);
        }

        [Test]
        public void AddRangeAddsARangeIsCollectionIsNotAList()
        {
            IList<int> list = new Collection<int>();
            var elements = new List<int> { 1, 2, 3, 4 };
            
            list.AddRange(elements);
            
            CollectionAssert.AreEqual(elements, list);
        }
    }
}