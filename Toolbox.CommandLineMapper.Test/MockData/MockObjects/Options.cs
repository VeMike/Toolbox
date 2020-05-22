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
    ///     Used for testing if the specified names are
    ///     found when mapped
    /// </summary>
    public class NamedOptions
    {
        [Option("numberLongName")]
        public int NumberLongName { get; set; }
        
        [Option('t')]
        public string TextShortName { get; set; }
    }

    /// <summary>
    ///     Defines two properties with conflicting
    ///     short names
    /// </summary>
    public class ConflictingShortNames
    {
        [Option('a')]
        public string FirstProperty { get; set; }
        
        [Option('a')]
        public string SecondProperty { get; set; }
    }
    
    /// <summary>
    ///     Defines two properties with conflicting
    ///     long names
    /// </summary>
    public class ConflictingLongNames
    {
        [Option("FirstProperty")]
        public string FirstProperty { get; set; }
        
        [Option("FirstProperty")]
        public string SecondProperty { get; set; }
    }
    
    /// <summary>
    ///     A mock object used for some of the implemented
    ///     unit tests
    /// </summary>
    public class OtherOptions
    {
        
    }

    /// <summary>
    ///     A mock object that contains one property
    ///     for each integrated type that supports mapping
    /// </summary>
    public class IntegratedTypesOptions
    {
        [Option("booleanProperty")]
        public bool BooleanProperty { get; set; }
        
        [Option("byteProperty")]
        public byte ByteProperty { get; set; }
        
        [Option("charProperty")]
        public char CharProperty { get; set; }
        
        [Option("doubleProperty")]
        public double DoubleProperty { get; set; }
        
        [Option("floatProperty")]
        public float FloatProperty { get; set; }
        
        [Option("integerProperty")]
        public int IntegerProperty { get; set; }
        
        [Option("longProperty")]
        public long LongProperty { get; set; }
        
        [Option("shortProperty")]
        public short ShortProperty { get; set; }
        
        [Option("stringProperty")]
        public string StringProperty { get; set; }
    }
}