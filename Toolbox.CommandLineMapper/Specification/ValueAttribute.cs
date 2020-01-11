// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 23:33
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Toolbox.CommandLineMapper.Specification
{
    /// <summary>
    ///     Defines a nameless command line option that
    ///     is mapped using it's index position in the
    ///     command line string.
    /// </summary>
    public class ValueAttribute : AttributeBase
    {
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="index">
        ///     The index of the value in the command line
        ///     string.
        /// </param>
        public ValueAttribute(int index)
        {
            this.Index = index;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the index position of the value
        ///     in the command line string
        /// </summary>
        public int Index { get; set; }

        #endregion
    }
}