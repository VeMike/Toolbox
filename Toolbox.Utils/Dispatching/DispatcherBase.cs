// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 17:35
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace Toolbox.Utils.Dispatching
{
    /// <summary>
    ///     A base class for implementations of <see cref="IDispatcher"/>
    /// </summary>
    public abstract class DispatcherBase : IDispatcher
    {
        #region Attributes

        /// <summary>
        ///     Contains instances of handlers for command
        ///     types. This is used to cache already created
        ///     handler instances.
        /// </summary>
        private readonly IDictionary<Type, IList<object>> handlersForType;

        #endregion

        #region Constructor

        /// <summary>
        ///     Default initialization
        /// </summary>
        protected DispatcherBase()
        {
            this.handlersForType = new Dictionary<Type, IList<object>>();
        }

        #endregion

        #region IDispatcher Implementation

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        ///    Thrown if the passed command is null
        /// </exception>
        public void Dispatch<TCommand>(TCommand command) where TCommand : class
        {
            if(command is null)
                throw new ArgumentNullException(nameof(command));

            var handlerType = this.GetHandlerType(command.GetType());

            var handlers = this.FindHandlers(handlerType, command.GetType());

            foreach (var handler in handlers)
            {
                this.DispatchToSingleHandler(handler, command);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the type of the handler that can
        ///     handle the type of command passed to
        ///     <see cref="Dispatch{TCommand}"/>
        /// </summary>
        /// <param name="commandType">
        ///    The type of the command passed to <see cref="Dispatch{TCommand}"/>
        /// </param>
        /// <returns>
        ///    The type of the handler
        /// </returns>
        protected abstract Type GetHandlerType(Type commandType);

        /// <summary>
        ///     Finds implementations of command handlers, that are
        ///     able to handle the passed <paramref name="commandType"/>
        /// </summary>
        /// <param name="handlerType">
        ///    The command type of handler whose implementations
        ///     shall be found.
        /// </param>
        /// <param name="commandType">
        ///    The type of the command for whom a handler should
        ///     be found
        /// </param>
        /// <returns>
        ///    A collection of handler instances that are
        ///     able to handle the passed <paramref name="commandType"/>
        /// </returns>
        private IEnumerable<object> FindHandlers(Type handlerType, Type commandType)
        {
            if (this.handlersForType.TryGetValue(commandType, out var handlers))
                return handlers;

            var requestedHandlers = this.RequestHandlerTypes(handlerType);

            var createdHandlers = CreateHandlerInstances(requestedHandlers).ToList();
            
            this.handlersForType.Add(commandType, createdHandlers);

            return createdHandlers;
        }
        
        /// <summary>
        ///     Dispatches a command to a single handler. The
        ///     <paramref name="handler"/> has the same type as
        ///     the one returned by <see cref="GetHandlerType"/>
        /// </summary>
        /// <param name="handler">
        ///    An instance of a handler that can handle <paramref name="command"/>
        /// </param>
        /// <param name="command">
        ///    A command the <paramref name="handler"/> can handle.
        /// </param>
        /// <typeparam name="TCommand">
        ///    The type of command dispatched to <paramref name="handler"/>
        /// </typeparam>
        protected abstract void DispatchToSingleHandler<TCommand>(object handler, TCommand command) where TCommand : class;

        /// <summary>
        ///     Creates an instance of each passed type. Instances are created by
        ///     calling their default constructor
        /// </summary>
        /// <param name="requestedHandlers">
        ///    The types for whom an instance is created
        /// </param>
        /// <returns>
        ///    An instance for each passed type
        /// </returns>
        private static IEnumerable<object> CreateHandlerInstances(IEnumerable<Type> requestedHandlers)
        {
            return requestedHandlers.Select(Activator.CreateInstance)
                                    .Where(instance => instance is not null);
        }

        /// <summary>
        ///     Requests types of objects that implement <see cref="ICommandHandler{TCommand}"/>
        /// </summary>
        /// <param name="handlerType">
        ///    The type of <see cref="ICommandHandler{TCommand}"/> that handles
        ///    the type of command passed to <see cref="Dispatch{TCommand}"/>
        /// </param>
        /// <returns>
        ///    A collection of types that are able to handle the command
        ///    passed to <see cref="Dispatch{TCommand}"/>
        /// </returns>
        protected abstract IEnumerable<Type> RequestHandlerTypes(object handlerType);

        #endregion
    }
}