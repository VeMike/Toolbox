using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Collection
{
    class SequentialPartitioner<T> : IPartitioner<T>
    {
        #region Attributes
        private readonly IList<IList<T>> partitions;
        #endregion

        #region Constructor
        public SequentialPartitioner()
        {
            //Initialize the outer List
            this.partitions = new List<IList<T>>();
        }
        #endregion

        #region IPartitioner<T> implementation
        public int Partitions { get; private set; }

        public void Partition(IList<T> sourceCollection, int partitionSize)
        {
            if(sourceCollection != null &&
               partitionSize > 0)
            {
                this.PartitionList(sourceCollection, partitionSize);
            }
        }

        public IList<T> GetPartitionAt(int index)
        {
            if (index >= 0 &&
                index < this.partitions.Count)
            {
                return this.partitions[index];
            }

            return null;
        }
        #endregion

        #region Helpers
        /// <summary>
        ///     Does the actual partitioning of the List
        /// </summary>
        /// <param name="sourceCollection">
        ///     The collection, that will be partitioned
        /// </param>
        /// <param name="partitionSize">
        ///     The prefered size of one partition
        /// </param>
        private void PartitionList(IList<T> sourceCollection, int partitionSize)
        {
            //The number of partitions, that will be created
            this.Partitions = (int)Math.Ceiling(sourceCollection.Count / (double)partitionSize);
            //Create the partitions
            for (int i = 0; i < this.Partitions; i++)
                this.partitions.Add(new List<T>(sourceCollection.Skip(partitionSize * i).Take(partitionSize)));
        }
        #endregion
    }
}
