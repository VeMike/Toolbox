// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-23 17:16
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.ComponentModel;

namespace Toolbox.Utils.Test.MockObjects
{
    /// <summary>
    ///     An enumeration of numbers with <see cref="DescriptionAttribute"/>
    /// </summary>
    public enum NumbersWithDescription
    {
        [Description("First")]
        ONE, 
        
        [Description("Second")]
        TWO,
        
        [Description("Third")]
        THREE
    }
    
    /// <summary>
    ///     An enumeration of numbers without <see cref="DescriptionAttribute"/>
    /// </summary>
    public enum NumbersWithoutDescription
    {
        ONE, 
        
        TWO,
        
        THREE
    }

    /// <summary>
    ///     An enumeration that does not have any members
    /// </summary>
    public enum EmptyEnumeration
    {
        
    }
}