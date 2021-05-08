// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-08 15:57
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace Toolbox.Utils.Dispatching
{
    public class RegistrationDependencyDispatcher<TDependency> : DispatcherBase, 
                                                                 IDependencyCommandDispatcher<TDependency> where TDependency : class
    {
        #region Attributes

        /// <summary>
        ///     The backing field of <see cref="Dependency"/>
        /// </summary>
        private TDependency dependency;

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
        /// <param name="dependency">
        ///    The dependency injected into the handlers
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown if the passed argument is 'null'
        /// </exception>
        public RegistrationDependencyDispatcher(TDependency dependency)
        {
            this.dependency = dependency;
            this.handlers = new HashSet<object>();
        }

        #endregion
        
        #region IDependencyDispatcher Implementation

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        ///    Thrown if the passed value is 'null'
        /// </exception>
        public TDependency Dependency
        {
            private get { return this.dependency; }
            set
            {
                this.dependency = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        #endregion
        
        #region DispatcherBase Implementation

        /// <inheritdoc />
        protected override Type GetHandlerType(Type commandType)
        {
            return typeof(IDependencyCommandHandler<,>).MakeGenericType(commandType,
                                                                        typeof(TDependency));
        }

        /// <inheritdoc />
        protected override void DispatchToSingleHandler<TCommand>(object handler, TCommand command)
        {
            if (handler is IDependencyCommandHandler<TCommand, TDependency> specificHandler)
            {
                specificHandler.Inject(this.Dependency);
                specificHandler.Handle(command);
            }
        }
        
        /// <inheritdoc />
        protected override IEnumerable<Type> RequestHandlerTypes(object handlerType)
        {
            return this.handlers.Select(handler => handler.GetType());
        }

        #endregion

        #region Implementation Members

        /// <summary>
        ///     Adds a new <see cref="IDependencyCommandHandler{TCommand,TDependency}"/> to the
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
        public void AddHandler<TCommand>(IDependencyCommandHandler<TCommand, TDependency> handler) where TCommand : class
        {
            if(handler is null)
                throw new ArgumentNullException(nameof(handler));
            
            this.handlers.Add(handler);
        }

        #endregion
    }
}