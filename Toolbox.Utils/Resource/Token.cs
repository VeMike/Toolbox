// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-04-03 14:08
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;

namespace Toolbox.Utils.Resource
{
    /// <summary>
    ///     A token that allows to access a shared resource.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of the resource this token allows access to
    /// </typeparam>
    public sealed class Token<TResource> : IDisposable where TResource : IResource
    {
        #region Attributes

        /// <summary>
        ///     The action called when this instance
        ///     is disposed.
        /// </summary>
        private readonly Action<Token<TResource>> disposeAction;

        /// <summary>
        ///     The backing field of <see cref="Resource"/>
        /// </summary>
        private readonly TResource resource;

        /// <summary>
        ///     Indicates if this instance was disposed
        /// </summary>
        private bool disposed;

        #endregion
        
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="resource">
        ///     The resource this token will handle
        /// </param>
        /// <param name="disposeAction">
        ///     The action that will be called
        ///     when this instance is disposed.
        /// </param>
        internal Token(TResource resource,
                       Action<Token<TResource>> disposeAction)
        {
            this.resource = resource;
            this.disposeAction = disposeAction;
            
            this.Value = Guid.NewGuid();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The resource held by this token
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     Thrown if the resource is accessed after
        ///     the token was disposed
        /// </exception>
        public TResource Resource
        {
            get
            {
                if (this.disposed)
                    throw new ObjectDisposedException(nameof(Token<TResource>));
                return this.resource;
            }
        }
        
        /// <summary>
        ///     The value of the token
        /// </summary>
        internal Guid Value { get; }

        #endregion
        
        #region IDisposable Implementation

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.disposeAction(this);

            this.disposed = true;
        }

        #endregion

        #region Equality Members

        private bool Equals(Token<TResource> other)
        {
            return this.Value.Equals(other.Value);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Token<TResource> other && this.Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static bool operator ==(Token<TResource> left, Token<TResource> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Token<TResource> left, Token<TResource> right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}