// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-23 17:39
// ===================================================================================================
// = Description :
// ===================================================================================================

using Com.Toolbox.Utils.Common;

namespace Toolbox.Utils.Test.MockObjects
{
    /// <summary>
    ///     An object, that uses the <see cref="Singleton{T}"/> utility
    /// </summary>
    public class SingletonObject : Singleton<SingletonObject>
    {
        private SingletonObject()
        {
            
        }
    }
}