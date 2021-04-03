// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-04-03 14:03
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Com.Toolbox.Utils.Resource
{
    /// <summary>
    ///     A resource that can be acquired and
    ///     released.
    /// </summary>
    public interface IResource
    {
        /// <summary>
        ///     Acquires the resource
        /// </summary>
        void Acquire();

        /// <summary>
        ///     Releases the resource
        /// </summary>
        void Release();
    }
}