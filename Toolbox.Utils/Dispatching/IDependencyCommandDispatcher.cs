// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 17:20
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Toolbox.Utils.Dispatching
{
    /// <summary>
    ///     The interface for a command dispatcher that
    ///     allows to inject dependencies to a command handler.
    ///
    ///     Issued commands are dispatched to implementations of
    ///     <see cref="IDependencyCommandHandler{TCommand,TDependency}"/>
    /// </summary>
    /// <typeparam name="TDependency">
    ///    The type of command the implementation is able
    ///    to handle. 
    /// </typeparam>
    public interface IDependencyCommandDispatcher<TDependency> : IDispatcher where TDependency : class
    {
        /// <summary>
        ///     Sets the <see cref="TDependency"/> that will be injected
        ///     into any <see cref="IDependencyCommandHandler{TCommand,TDependency}"/> 
        ///     before calling <see cref="IDependencyCommandHandler{TCommand,TDependency}.Handle"/>
        /// </summary>
        TDependency Dependency { set; }
    }
}