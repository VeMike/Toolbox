// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-12 00:29
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Collections.Generic;
using Toolbox.CommandLineMapper.Common;

namespace Toolbox.CommandLineMapper.Mapper
{
    /// <summary>
    ///     The result object of <see cref="ICommandLineMapper.GetMapperResult{TMapTarget}"/>
    /// </summary>
    /// <typeparam name="TMappedObject">
    ///     The type of object the command line arguments were mapped to
    /// </typeparam>
    public interface IMapperResult<out TMappedObject>
    {
        #region Properties

        /// <summary>
        ///     The object the command line arguments were mapped to
        /// </summary>
        TMappedObject Value { get; }

        /// <summary>
        ///     Gets all the arguments, that were mapped to
        ///     the object held by <see cref="Value"/>
        /// </summary>
        IList<Argument> MappedArguments { get; }
        
        /// <summary>
        ///     Contains any errors that were caused
        ///     while trying to map command line
        ///     arguments to the <see cref="Value"/>.
        ///
        ///     If <see cref="MapperOptions.ContinueOnError"/> is
        ///     set to 'false', this always just contains a single
        ///     element. If 'true' all errors caused during the mapping
        ///     are included.
        /// </summary>
        IList<MappingError> Errors { get; }

        #endregion


    }
}