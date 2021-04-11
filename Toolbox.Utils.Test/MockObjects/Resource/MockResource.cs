// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-04-03 14:35
// ===================================================================================================
// = Description :
// ===================================================================================================

using Com.Toolbox.Utils.Resource;

namespace Toolbox.Utils.Test.MockObjects.Resource
{
    /// <summary>
    ///     A mock implementation of <see cref="IResource"/>
    /// </summary>
    public class MockResource : IResource
    {
        #region Properties

        /// <summary>
        ///     Counts the calls made to <see cref="Acquire"/>
        /// </summary>
        public int AcquireCalls { get; private set; }

        /// <summary>
        ///     Counts the calls made to <see cref="Release"/>
        /// </summary>
        public int ReleaseCalls { get; private set; }
        
        #endregion
        
        #region IResource Implementation

        /// <inheritdoc />
        public void Acquire()
        {
            this.AcquireCalls++;
        }

        /// <inheritdoc />
        public void Release()
        {
            this.ReleaseCalls++;
        }

        #endregion
    }
}