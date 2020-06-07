// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 17:14
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Com.Toolbox.Utils.Dispatching
{
    /// <summary>
    ///     Handles a single command.
    /// </summary>
    /// <typeparam name="TCommand">
    ///    The type of command the implementation is able
    ///    to handle
    /// </typeparam>
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        /// <summary>
        ///     Handles the passed command
        /// </summary>
        /// <param name="command">
        ///    The command handled by the implementation
        /// </param>
        void Handle(TCommand command);
    }
}