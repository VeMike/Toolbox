// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 17:43
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Com.Toolbox.Utils.Dispatching
{
    /// <summary>
    ///     An implementation of <see cref="IDispatcher"/> that searches an
    ///     <see cref="Assembly"/> using reflection for implementations
    ///     of <see cref="ICommandHandler{TCommand}"/> 
    /// </summary>
    public class ReflectionDispatcher : CommandHandlerDispatcherBase
    {
        #region Attributes

        /// <summary>
        ///     The backing field of <see cref="Assembly"/>
        /// </summary>
        private Assembly assembly;

        #endregion

        #region Constructor

        /// <summary>
        ///     Default constructor
        /// </summary>
        public ReflectionDispatcher()
        {
            this.assembly = Assembly.GetEntryAssembly();
        }

        /// <summary>
        ///     Creates a new instance
        /// </summary>
        /// <param name="assembly">
        ///    The assembly that contains implementations of
        ///     <see cref="ICommandHandler{TCommand}"/>
        /// </param>
        public ReflectionDispatcher(Assembly assembly)
        {
            this.assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the <see cref="Assembly"/> that contains
        ///     implementations of <see cref="ICommandHandler{TCommand}"/>.
        ///
        ///     Defaults to the entry assembly 'Assembly.GetEntryAssembly()'
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///    Thrown is the assigned value is 'null'
        /// </exception>
        public Assembly Assembly
        {
            private get { return this.assembly; }
            set
            {
                this.assembly = value ?? throw new ArgumentNullException(nameof(this.Assembly));
            }
        }

        #endregion

        #region DispatcherBase Implementation

        /// <inheritdoc />
        protected override IEnumerable<Type> RequestHandlerTypes(object handlerType)
        {
            return this.assembly.GetTypes().Where(t => t.IsClass &&
                                                       t.GetInterfaces().Contains(handlerType));
        }

        #endregion
    }
}