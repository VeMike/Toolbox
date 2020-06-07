// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 19:28
// ===================================================================================================
// = Description :
// ===================================================================================================

using Com.Toolbox.Utils.Dispatching;

namespace Toolbox.Utils.Test.MockObjects.Dispatcher
{
    #region ICommandHandler

    // ReSharper disable once UnusedType.Global
    public class CalledCommandHandler : ICommandHandler<CalledCommand>
    {
        /// <inheritdoc />
        public void Handle(CalledCommand command)
        {
            command.WasCalled = true;
        }
    }

    // ReSharper disable once UnusedType.Global
    public class FirstAllHandlersCalledHandler : ICommandHandler<CallTwoHandlersCommand>
    {
        /// <inheritdoc />
        public void Handle(CallTwoHandlersCommand command)
        {
            command.Calls++;
        }
    }
    
    // ReSharper disable once UnusedType.Global
    public class SecondAllHandlersCalledHandler : ICommandHandler<CallTwoHandlersCommand>
    {
        /// <inheritdoc />
        public void Handle(CallTwoHandlersCommand command)
        {
            command.Calls++;
        }
    }

    #endregion

    #region IDependencyCommandHandler

    // ReSharper disable once UnusedType.Global
    public class CalledDependencyCommandHandler : IDependencyCommandHandler<CalledCommand, string>
    {
        /// <inheritdoc />
        public void Handle(CalledCommand command)
        {
            command.WasCalled = true;
        }

        /// <inheritdoc />
        public void Inject(string dependency)
        {
        }
    }

    // ReSharper disable once UnusedType.Global
    public class FirstAllDependencyHandlersCalledHandler : IDependencyCommandHandler<CallTwoHandlersCommand, string>
    {
        /// <inheritdoc />
        public void Handle(CallTwoHandlersCommand command)
        {
            command.Calls++;
        }

        /// <inheritdoc />
        public void Inject(string dependency)
        {
        }
    }

    // ReSharper disable once UnusedType.Global
    public class SecondAllDependencyHandlersCalledHandler : IDependencyCommandHandler<CallTwoHandlersCommand, string>
    {
        /// <inheritdoc />
        public void Handle(CallTwoHandlersCommand command)
        {
            command.Calls++;
        }

        /// <inheritdoc />
        public void Inject(string dependency)
        {
        }
    }

    public class InjectCalledBeforeHandleHandler : IDependencyCommandHandler<InjectBeforeHandleCommand, string>
    {
        private bool injectWasCalled;
        
        /// <inheritdoc />
        public void Handle(InjectBeforeHandleCommand command)
        {
            command.InjectBeforeHandleCalled = this.injectWasCalled;
        }

        /// <inheritdoc />
        public void Inject(string dependency)
        {
            this.injectWasCalled = true;
        }
    }

    public class GetInjectedDependencyHandler : IDependencyCommandHandler<GetInjectedDependencyCommand, string>
    {
        private string injected;
        
        /// <inheritdoc />
        public void Handle(GetInjectedDependencyCommand command)
        {
            command.Dependency = this.injected;
        }

        /// <inheritdoc />
        public void Inject(string dependency)
        {
            this.injected = dependency;
        }
    }
    
    #endregion
}