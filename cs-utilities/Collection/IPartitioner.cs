using System.Collections.Generic;

namespace Utilities.Collection
{
    interface IPartitioner<T>
    {
        /// <summary>
        ///     Creates partitions from a passed <see cref="IList{T}"/>
        /// </summary>
        /// <param name="sourceCollection">
        ///     The <see cref="IList{T}"/>, that should be partitioned
        /// </param>
        /// <param name="partitionSize">
        ///     The number of elements from the <paramref name="sourceCollection"/>, 
        ///     that should be contained in one partition.
        /// </param>
        void Partition(IList<T> sourceCollection, int partitionSize);

        /// <summary>
        ///     Returns the partition at <paramref name="index"/>, that was
        ///     created by <see cref="Partition(IList{T}, int)"/>
        /// </summary>
        /// <param name="index">
        ///     The <paramref name="index"/> of a partition
        /// </param>
        /// <returns>
        ///     The partition at the specified <paramref name="index"/>
        /// </returns>
        IList<T> GetPartitionAt(int index);

        int Partitions { get; }
    }
}
