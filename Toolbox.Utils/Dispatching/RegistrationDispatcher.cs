// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-08 14:54
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace Toolbox.Utils.Dispatching
{
    /// <summary>
    ///     An implementation of <see cref="IDispatcher"/> that only
    ///     uses <see cref="ICommandHandler{TCommand}"/> that were
    ///     explicitly registered at the instance
    /// </summary>
    public class RegistrationDispatcher : CommandHandlerDispatcherBase
    {
        #region Attributes

        /// <summary>
        ///     A collection of all handlers that were added
        ///     to the instance.
        /// </summary>
        private readonly ISet<object> handlers;

        #endregion
        
        #region Constructor
        
        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        public RegistrationDispatcher()
        {
            this.handlers = new HashSet<object>();
        }

        #endregion
        
        #region DispatcherBase Implementation

        /// <inheritdoc />
        protected override IEnumerable<Type> RequestHandlerTypes(object handlerType)
        {
            return this.handlers.Select(handler => handler.GetType());
        }

        #endregion

        #region Implementation Members

        /// <summary>
        ///     Adds a new <see cref="ICommandHandler{TCommand}"/> to the
        ///     instance. The same handler instance can only be added one.
        ///     Sameness is determined by the implementation of a handlers
        ///     'Equals' method.
        /// </summary>
        /// <param name="handler">
        ///    A handler that shall be added to the collection
        ///    of handlers
        /// </param>
        /// <typeparam name="TCommand">
        ///    The type of command the handler is able to handle
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        ///    Thrown if the passed handler is 'null'
        /// </exception>
        public void AddHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : class
        {
            if(handler is null)
                throw new ArgumentNullException(nameof(handler));
            
            this.handlers.Add(handler);
        }

        #endregion
    }
}