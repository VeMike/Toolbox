using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Collection
{
    class SequentialPartitioner<T> : IPartitioner<T>
    {
        #region Attributes
        private readonly IList<IList<T>> partitions;
        #endregion

        public void Partition(IList<T> sourceCollection, uint partitionSize)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetPartitionAt(uint index)
        {
            throw new NotImplementedException();
        }
    }
}
