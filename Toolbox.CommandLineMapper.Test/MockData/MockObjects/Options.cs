// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-14 20:00
// ===================================================================================================
// = Description :
// ===================================================================================================

using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Test.MockData.MockObjects
{
    /// <summary>
    ///     A mock object used for some of the implemented
    ///     unit tests
    /// </summary>
    public class Options
    {
        [Option("p", "path")]
        public string Path { get; set; }
        
        [Option("s", "size")]
        public int Size { get; set; }
    }
    
    /// <summary>
    ///     A mock object used for some of the implemented
    ///     unit tests
    /// </summary>
    public class OtherOptions
    {

    }
}