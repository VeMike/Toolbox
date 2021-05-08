// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-08 15:31
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;

namespace Toolbox.Utils.Dispatching
{
    /// <summary>
    ///     Provides default implementation for <see cref="IDispatcher"/>
    ///     that dispatch to <see cref="ICommandHandler{TCommand}"/>
    /// </summary>
    public abstract class CommandHandlerDispatcherBase : DispatcherBase
    {
        /// <inheritdoc />
        protected override Type GetHandlerType(Type commandType)
        {
            return typeof(ICommandHandler<>).MakeGenericType(commandType);
        }

        /// <inheritdoc />
        protected override void DispatchToSingleHandler<TCommand>(object handler, TCommand command)
        {
            if (handler is ICommandHandler<TCommand> specificHandler)
            {
                specificHandler.Handle(command);
            }
        }
    }
}