// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 17:23
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Toolbox.Utils.Dispatching
{
    /// <summary>
    ///     Handles a single command with a dependency
    /// </summary>
    /// <typeparam name="TCommand">
    ///    The type of command handled
    /// </typeparam>
    /// <typeparam name="TDependency">
    ///    The type of dependency required by the handler
    /// </typeparam>
    public interface IDependencyCommandHandler<TCommand, TDependency> where TCommand : class 
                                                                      where TDependency : class
    {
        /// <summary>
        ///     Handles the passed command
        /// </summary>
        /// <param name="command">
        ///    The command handled by the implementation
        /// </param>
        void Handle(TCommand command);
        
        /// <summary>
        ///     Injects the dependency into the command handler.
        ///     This is called before <see cref="ICommandHandler{TCommand}.Handle"/>
        /// </summary>
        /// <param name="dependency">
        ///    The injected dependency
        /// </param>
        void Inject(TDependency dependency);
    }
}