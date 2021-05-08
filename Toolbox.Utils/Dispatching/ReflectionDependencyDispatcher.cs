// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 19:49
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;

namespace Toolbox.Utils.Dispatching
{
    /// <summary>
    ///     An implementation of <see cref="IDependencyCommandDispatcher{TDependency}"/> that searches an
    ///     <see cref="Assembly"/> using reflection for implementations
    ///     of <see cref="IDependencyCommandHandler{TCommand,TDependency}"/> 
    /// </summary>
    /// <typeparam name="TDependency">
    ///    The typeof dependency injected into the command handler
    /// </typeparam>
    public sealed class ReflectionDependencyDispatcher<TDependency> : ReflectionDispatcher, 
                                                                      IDependencyCommandDispatcher<TDependency> where TDependency : class
    {
        #region Attributes

        /// <summary>
        ///     The backing field of <see cref="Dependency"/>
        /// </summary>
        private TDependency dependency;

        #endregion

        #region Constructor

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="dependency">
        ///    The dependency injected into the handlers
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown if the passed instance is null
        /// </exception>
        public ReflectionDependencyDispatcher(TDependency dependency) : this(dependency,
                                                                             Assembly.GetExecutingAssembly())
        {

        }

        /// <summary>
        ///     Creates a new instance
        /// </summary>
        /// <param name="dependency">
        ///    The dependency injected into the handlers
        /// </param>
        /// <param name="assembly">
        ///    The assembly that contains implementations of
        ///     <see cref="IDependencyCommandHandler{TCommand,TDependency}"/>
        /// </param>
        public ReflectionDependencyDispatcher(TDependency dependency, Assembly assembly) : base(assembly)
        {
            this.dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
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
            if (handler is not IDependencyCommandHandler<TCommand, TDependency> specificHandler)
            {
                return;
            }

            specificHandler.Inject(this.Dependency);
            specificHandler.Handle(command);
        }

        #endregion
    }
}