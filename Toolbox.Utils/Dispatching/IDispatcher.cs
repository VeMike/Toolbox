// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-06 20:17
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Toolbox.Utils.Dispatching
{
    /// <summary>
    ///     The interface for a command dispatcher. Issued commands
    ///     are dispatched to implementations of <see cref="ICommandHandler{TCommand}"/>
    /// </summary>
    public interface IDispatcher
    {
        /// <summary>
        ///     Dispatches the passed command to all handlers this
        ///     dispatcher can find.
        /// </summary>
        /// <param name="command">
        ///    An instance of the command that will be dispatched
        /// </param>
        /// <typeparam name="TCommand">
        ///    The type of the command that will be dispatched
        /// </typeparam>
        void Dispatch<TCommand>(TCommand command) where TCommand : class;
    }
}