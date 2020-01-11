// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-12 00:29
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Toolbox.CommandLineMapper.Mapper
{
    /// <summary>
    ///     The result object of <see cref="ICommandLineMapper.GetMapperResult{TMapTarget}"/>
    /// </summary>
    /// <typeparam name="T">
    ///     The type of object the command line arguments were mapped to
    /// </typeparam>
    public interface IMapperResult<out T>
    {
        #region Properties

        /// <summary>
        ///     The object the command line arguments were mapped to
        /// </summary>
        T Value { get; }

        #endregion

        #region Methods



        #endregion

    }
}