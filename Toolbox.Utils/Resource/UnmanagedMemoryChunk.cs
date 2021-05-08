using System;
using System.Runtime.InteropServices;

namespace Toolbox.Utils.Resource
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
            this.Memory = IntPtr.Zero;
            this.Size = 0;
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
        public int Size { get; private set; }
        
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
                    if(this.Memory != IntPtr.Zero)
                        Marshal.FreeHGlobal(this.Memory);
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
            this.FreeMemory();
            try
            {
                this.Memory = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, this.Memory, data.Length);
                this.Size = data.Length;
            }
            catch (OutOfMemoryException)
            {
                this.Size = 0;
            }
            catch (Exception)
            {
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
            if (this.Memory != IntPtr.Zero)
                Marshal.FreeHGlobal(this.Memory);
            this.Memory = IntPtr.Zero;
            this.Size = 0;
        } 
        
        #endregion
    }
}
