using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Memory.Unmanaged
{
    public sealed class UnmanagedMemoryChunk : IUnmanagedMemory
    {

        #region Attributes
        private int memorySize;
        private bool disposeCalled;
        #endregion

        #region Constructor
        public UnmanagedMemoryChunk()
        {
            //Indicate, that nothing has been allocated.
            this.Memory = IntPtr.Zero;
            //Used for the IDisposable pattern (pattern generated via VS2017)
            this.disposeCalled = false;
            //Nothing has been initializes
            this.memorySize = 0;
        } 
        #endregion

        #region IUnmanagedMemory implementation
        public byte[] Data
        {
            set
            {
                if (value != null && value.Length > 0)
                    this.CopyToUnmanagedMemory(value);
            }
        }

        public int MemorySize => this.memorySize;

        public IntPtr Memory { get; private set; }
        #endregion

        #region IDisposable implementation
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposeCalled)
            {
                if (disposing)
                {
                    //Free the unmanaged memory
                    if(this.Memory != IntPtr.Zero)
                        Marshal.FreeHGlobal(this.Memory);
                    //Set the handle to zero
                    this.Memory = IntPtr.Zero;
                }

                this.disposeCalled = true;
            }
        }
        #endregion

        #region Helpers
        private void CopyToUnmanagedMemory(byte[] data)
        {
            //Free any previously allocated memory
            this.FreeMemory();
            try
            {
                //Allocate memory for the new data
                this.Memory = Marshal.AllocHGlobal(data.Length);
                //Copy the data into unmanaged memory
                Marshal.Copy(data, 0, this.Memory, data.Length);
            }
            catch (OutOfMemoryException)
            {
                //Nothing was allocated, if the we are out of memory
                this.memorySize = 0;
            }
            catch (Exception)
            {
                //This path will be reached, of something went
                //wrong while copying the data. Free any handles,
                //if this occurs.
                this.FreeMemory();
            }
        }

        /// <summary>
        ///     Frees the unmanaged memory and sets the internal
        ///     memory handle to 'IntPtr.Zero' and the memory
        ///     size to 0.
        /// </summary>
        private void FreeMemory()
        {
            //Free the unmanaged Memory, if it is allocated
            if (this.Memory != IntPtr.Zero)
                Marshal.FreeHGlobal(this.Memory);
            //Indicate, that nothing is allocated
            this.Memory = IntPtr.Zero;
            //The size is 0, since it was freed
            this.memorySize = 0;
        } 
        #endregion
    }
}
