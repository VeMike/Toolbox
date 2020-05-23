// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-12 00:11
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Collections.Generic;
using Toolbox.CommandLineMapper.Common;

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
        ///     Gets the result the mapping operation for
        ///     a specific type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of objects whose map result should be accessed.
        /// </typeparam>
        /// <returns>
        ///     The result of the mapping operation
        /// </returns>
        IMapperResult<T> GetMapperResult<T>() where T : class, new();

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
        ///     Handles registrations for objects that can be mapped
        ///     to command line arguments
        /// </summary>
        IRegistrationService RegistrationService { get; }
        
        /// <summary>
        ///     Gets a collection of arguments, that were not
        ///     mapped to any object. This typically happens
        ///     if the command line argument has a value that
        ///     does not match the name (long name, short name or
        ///     actual name) of the property.
        /// </summary>
        IEnumerable<Argument> UnmappedArguments { get; } 

        #endregion
    }
}