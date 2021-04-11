// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-04-03 14:03
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Com.Toolbox.Utils.Resource
{
    /// <summary>
    ///     A handle for a shared resource that will be
    ///     shared across multiple consumers but only
    ///     acquired and release once.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of the resource handled by this instance
    /// </typeparam>
    public interface IResourceHandle<TResource> where TResource : IResource
    {
        #region Methods

        /// <summary>
        ///     Creates a new access token for a shared resource.
        /// </summary>
        /// <returns>
        ///     A new access token for a resource
        /// </returns>
        Token<TResource> Access();

        #endregion

        #region Properties

        /// <summary>
        ///     The current amount of tokens in use.
        /// </summary>
        int Tokens { get; }

        #endregion
    }
}