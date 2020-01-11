// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-12 00:11
// ===================================================================================================
// = Description :
// ===================================================================================================

namespace Toolbox.CommandLineMapper.Mapper
{
    /// <summary>
    ///     The main operations for mapping command line arguments
    ///     to the properties of an object.
    /// </summary>
    public interface ICommandLineMapper
    {
        #region Methods

        /// <summary>
        ///     Registers a new object type at the mapper. 
        /// </summary>
        /// <typeparam name="T">
        ///     The object to register. This is the object to
        ///     whom command line arguments are mapped.
        /// </typeparam>
        void Register<T>() where T : new();

        /// <summary>
        ///     Gets the result the <see cref="Map"/> operation for
        ///     a specific type
        /// </summary>
        /// <typeparam name="T">
        ///     The type of objects whose map result should be accessed.
        /// </typeparam>
        /// <returns>
        ///     The result of the <see cref="Map"/> operation
        /// </returns>
        IMapperResult<T> GetMapperResult<T>() where T : new();

        /// <summary>
        ///     Maps the passed <paramref name="args"/> to the objects
        ///     that were registered at this instance
        /// </summary>
        /// <param name="args">
        ///     An argument string passed to the application when
        ///     launched.
        /// </param>
        void Map(string args);

        #endregion

    }
}