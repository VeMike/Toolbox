// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-07 19:26
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Toolbox.Utils.Test.MockObjects.Dispatcher
{
    public class EmptyCommand
    {
        
    }
    
    public class CalledCommand
    {
        public bool WasCalled { get; set; } = false;
    }
    
    public class CallTwoHandlersCommand
    {
        public int Calls { get; set; } = 0;
    }
    
    public class InjectBeforeHandleCommand
    {
        public bool InjectBeforeHandleCalled { get; set; } = false;
    }

    public class GetInjectedDependencyCommand
    {
        public string Dependency { get; set; }
    }
}