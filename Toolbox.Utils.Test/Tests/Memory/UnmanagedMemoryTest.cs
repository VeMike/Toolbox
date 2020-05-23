// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-23 17:54
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Com.Toolbox.Utils.Memory;
using NUnit.Framework;

namespace Toolbox.Utils.Test.Tests.Memory
{
    [TestFixture]
    public class MemoryTests
    {
        #region Tests

        [Test]
        public void NoMemoryIsAllocatedByDefault()
        {
            var memory = new UnmanagedMemoryChunk();
            
            Assert.AreEqual(IntPtr.Zero, memory.Memory);
        }

        [Test]
        public void NothingIsAllocatedIfDataIsNull()
        {
            var memory = new UnmanagedMemoryChunk {Data = null};
            
            Assert.AreEqual(IntPtr.Zero, memory.Memory);
        }

        [Test]
        public void NothingIsAllocatedIfDataIsEmpty()
        {
            var memory = new UnmanagedMemoryChunk {Data = new byte[0]};
            
            Assert.AreEqual(IntPtr.Zero, memory.Memory);
        }

        [Test]
        public void DataIsAllocatedInUnmanagedMemory()
        {
            var data = new byte[100];
            new Random().NextBytes(data);

            using (var memory = new UnmanagedMemoryChunk {Data = data})
            {
                Assert.AreEqual(100, memory.MemorySize);
            }
        }

        [Test]
        public void DataIsDeallocatedAfterObjectIsDisposed()
        {
            var data = new byte[100];
            new Random().NextBytes(data);

            UnmanagedMemoryChunk memory;
            
            using (memory = new UnmanagedMemoryChunk {Data = data})
            {
            }
            
            Assert.AreEqual(IntPtr.Zero, memory.Memory);
        }

        #endregion
    }
}