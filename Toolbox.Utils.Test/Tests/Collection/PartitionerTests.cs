// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-23 17:28
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using Com.Toolbox.Utils.Collection;
using NUnit.Framework;

namespace Toolbox.Utils.Test.Tests.Collection
{
    [TestFixture]
    public class PartitionerTests
    {
        #region Tests

        [Test]
        public void SequentialPartitionThrowsIfPartitionArgumentIsNull()
        {
            var partition = new SequentialPartition<int>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                partition.Partition(null, 1);
            });
        }

        [Test]
        public void GetThrowsIfIndexIsOutOfRange()
        {
            var partition = new SequentialPartition<int>();

            partition.Partition(new List<int>{1, 2, 3, 4, 5}, 2);

            Assert.Throws<IndexOutOfRangeException>(() => partition.Get(200));
        }

        [Test]
        public void GetReturnsPartitionAtIndex()
        {
            var expectedFirstPartition = new List<int>{1,2,3};
            var expectedSecondPartition = new List<int>{4, 5};
            
            var partition = new SequentialPartition<int>();

            partition.Partition(new List<int>{1, 2, 3, 4, 5}, 3);
            
            Assert.Multiple(() =>
            {
                CollectionAssert.AreEqual(expectedFirstPartition, partition.Get(0));
                CollectionAssert.AreEqual(expectedSecondPartition, partition.Get(1));
            });
        }
        
        [TestCaseSource(typeof(PartitionerTests), nameof(PartitionCreatedTestCases))]
        public int PartitionsAreCreatedFromList(IList<int> input, int partitionSize)
        {
            var partition = new SequentialPartition<int>();
            
            partition.Partition(input, partitionSize);

            return partition.Partitions;
        }

        #endregion

        #region Test Data

        /// <summary>
        ///     Test case data for <see cref="PartitionsAreCreatedFromList"/>
        /// </summary>
        private static IEnumerable PartitionCreatedTestCases
        {
            get
            {
                yield return new TestCaseData(new List<int>{1,2,3,4,5,6,7,8}, 2).Returns(4);
                
                yield return new TestCaseData(new List<int>{1,2,3,4}, 4).Returns(1);
                
                yield return new TestCaseData(new List<int>{1,2,3}, 10).Returns(1);
                
                yield return new TestCaseData(new List<int>{1,2,3,4,5,6,7,8}, 1).Returns(8);
            }
        }

        #endregion
    }
}