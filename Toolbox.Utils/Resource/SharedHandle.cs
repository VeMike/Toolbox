// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-04-03 14:21
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Toolbox.Utils.Resource
{
    /// <summary>
    ///     An implementation of <see cref="IResourceHandle{TResource}"/> that
    ///     handles access to a shared resource.
    ///
    ///     The first token created by this instance calls <see cref="IResource.Acquire"/>
    ///     to access the resource. The last token that is released will call
    ///     <see cref="IResource.Release"/> to release the shared resource.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The resource handled by this instance
    /// </typeparam>
    public class SharedHandle<TResource> : IResourceHandle<TResource> where TResource : IResource
    {
        #region Attributes

        /// <summary>
        ///     Locks access to this instance.
        /// </summary>
        private readonly object lockInstance = new();
        
        /// <summary>
        ///     Creates the resource to which the access
        ///     is shared
        /// </summary>
        private readonly Lazy<TResource> resourceFactory;

        /// <summary>
        ///     The tokens currently held by this instance.
        /// </summary>
        private readonly ConcurrentDictionary<Guid, Token<TResource>> tokens;

        #endregion
        
        #region Constructor
        
        /// <summary>
        ///     Creates a new instance of the class.
        /// </summary>
        /// <param name="resource">
        ///     The resource handled by this instance.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed instance is null
        /// </exception>
        public SharedHandle(TResource resource) : this(() => resource)
        {
            if (EqualityComparer<TResource>.Default.Equals(resource, default))
                throw new ArgumentNullException(nameof(resource));
        }

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="factory">
        ///     A factory that creates a new instance
        ///     of the resource.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed instance is null
        /// </exception>
        public SharedHandle(Func<TResource> factory)
        {
            if (factory is null)
                throw new ArgumentNullException(nameof(factory));
            
            this.resourceFactory = new Lazy<TResource>(factory, true);
            this.tokens = new ConcurrentDictionary<Guid, Token<TResource>>();
        }

        #endregion
        
        #region IResourceHandle Implementation

        /// <inheritdoc />
        public Token<TResource> Access()
        {
            this.AcquireResource();

            return this.CreateToken();
        }

        /// <inheritdoc />
        public int Tokens => this.tokens.Count;

        #endregion

        #region Methods

        /// <summary>
        ///     Acquires the resource if there are
        ///     currently no tokens held by the instance
        /// </summary>
        private void AcquireResource()
        {
            lock (this.lockInstance)
            {
                if (this.Tokens == 0)
                {
                    this.resourceFactory.Value.Acquire();
                }
            }
        }

        /// <summary>
        ///     Creates a new token to access a shared
        ///     resource.
        /// </summary>
        /// <returns>
        ///     The created token.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if the token can not be created.
        /// </exception>
        private Token<TResource> CreateToken()
        {
            // ReSharper disable once InconsistentlySynchronizedField
            // The factory itself is already synchronized.
            var token = new Token<TResource>(this.resourceFactory.Value,
                                             this.DisposeTokenAction);

            if (!this.tokens.TryAdd(token.Value, token))
            {
                throw new InvalidOperationException("Failed to add a new token to the internal dictionary");
            }

            return token;
        }

        /// <summary>
        ///     This action will be called once a token
        ///     disposes.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if the token can not be removed.
        /// </exception>
        private void DisposeTokenAction(Token<TResource> resource)
        {
            lock (this.lockInstance)
            {
                if (!this.tokens.TryRemove(resource.Value, out _))
                {
                    throw new InvalidOperationException("Failed to remove a token from internal dictionary");
                }
            
                if (this.Tokens == 0)
                {
                    this.resourceFactory.Value.Release();
                }
            }
        }

        #endregion
    }
}