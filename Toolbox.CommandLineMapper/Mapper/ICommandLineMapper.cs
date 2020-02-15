// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-12 00:11
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Collections.Generic;

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
        ///     Registers a new object type at the mapper. Each type
        ///     can only be registered once. Multiple calls to
        ///     <see cref="Register{T}"/> using the same type will
        ///     be ignored.
        /// </summary>
        /// <typeparam name="T">
        ///     The object to register. This is the object to
        ///     whom command line arguments are mapped.
        /// </typeparam>
        void Register<T>() where T : new();

        /// <summary>
        ///     The inverse method of <see cref="Register{T}"/>.
        ///     Unregisteres a type previously added.
        /// </summary>
        /// <typeparam name="T">
        ///     The object to unregister. This object will no longer
        ///     be used for mapping of command line arguments
        /// </typeparam>
        void UnRegister<T>() where T : new();

        /// <summary>
        ///     Checks, if a type is registered at this instance
        /// </summary>
        /// <typeparam name="T">
        ///     The type whose registration shall be checked
        /// </typeparam>
        /// <returns>
        ///     'true' if the type is registered, 'false' otherwise
        /// </returns>
        bool IsRegistered<T>() where T : new();

        /// <summary>
        ///     Gets the result the mapping operation for
        ///     a specific type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of objects whose map result should be accessed.
        /// </typeparam>
        /// <returns>
        ///     The result of the mapping operation
        /// </returns>
        IMapperResult<T> GetMapperResult<T>() where T : new();

        /// <summary>
        ///     Maps the passed <paramref name="args"/> to the objects
        ///     that were registered at this instance.
        ///     The mapping is made with the default <see cref="MapperOptions"/>
        /// </summary>
        /// <param name="args">
        ///     The arguments passed to the applications 'Main'-method
        ///     when started.
        /// </param>
        void Map(IEnumerable<string> args);

        /// <summary>
        ///     Maps the passed <paramref name="args"/> to the objects
        ///     that were registered at this instance.
        ///     The mapping is made with a custom set of <see cref="MapperOptions"/>
        /// </summary>
        /// <param name="args">
        ///     The arguments passed to the applications 'Main'-method
        ///     when started.
        /// </param>
        /// <param name="options">
        ///     Options for the command line mapping
        /// </param>
        void Map(IEnumerable<string> args, MapperOptions options);

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the current number of registrations. Those are
        ///     all calls to <see cref="Register{T}"/> with unique
        ///     objects types (same type registrations are ignored).
        /// </summary>
        int Registrations { get; }

        #endregion

    }
}