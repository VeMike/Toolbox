using System;
using System.Runtime.InteropServices;

namespace Com.Toolbox.Utils.Common
{
    /// <summary>
    ///     An implementation of <see cref="IUnmanagedMemory"/>
    /// </summary>
    public class UnmanagedMemoryChunk : IUnmanagedMemory
    {
        #region Attributes
        /// <summary>
        ///     Indicates, if dispose was called
        /// </summary>
        private bool disposeCalled;
        #endregion

        #region Constructor
        /// <summary>
        ///     Creates a new instance
        /// </summary>
        public UnmanagedMemoryChunk()
        {
            //Indicate, that nothing has been allocated.
            this.Memory = IntPtr.Zero;
            //Nothing has been initializes
            this.MemorySize = 0;
        }
        #endregion

        #region IUnmanagedMemory implementation
        /// <summary>
        ///     Sets the data, which will be copied into unmanaged
        ///     memory
        /// </summary>
        public byte[] Data
        {
            set
            {
                if (value != null && value.Length > 0)
                    this.CopyToUnmanagedMemory(value);
            }
        }

        /// <summary>
        ///     A pointer/handle representing the unmanaged memory,
        ///     that stores the bytes passed to <see cref="Data"/>
        /// </summary>
        public IntPtr Memory { get; private set; }

        /// <summary>
        ///     The number of bytes, that are currently stored in unmanaged
        ///     memory.
        /// </summary>
        public int MemorySize { get; private set; }
        #endregion

        #region IDisposable implementation
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
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
        /// <summary>
        ///     Copies the passed bytes into unmanaged memory
        /// </summary>
        /// <param name="data">
        ///     The bytes copied to unmanaged memory
        /// </param>
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
                //Nothing was allocated, if we are out of memory
                this.MemorySize = 0;
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
            this.MemorySize = 0;
        } 
        #endregion
    }
}
