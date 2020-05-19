// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-19 20:15
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Collections.Generic;
using System.Linq;

namespace Toolbox.CommandLineMapper.Mapper
{
    ///<inheritdoc />
    /// <summary>
    ///     The implementation of <see cref="IMapperResult{T}"/>
    /// </summary>
    /// <typeparam name="TMappedObject">
    ///     The type of object, that was mapped
    /// </typeparam>
    internal class MapperResult<TMappedObject> : IMapperResult<TMappedObject>
    {
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="value">
        ///    The object whose mapping results are accessed
        /// </param>
        public MapperResult(TMappedObject value) : this(value,
                                                        Enumerable.Empty<MappingError>())
        {
            
        }
        
        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="value">
        ///    The object whose mapping results are accessed
        /// </param>
        /// <param name="errors">
        ///    Errors that were caused while mapping
        ///     the <paramref name="value"/>
        /// </param>
        public MapperResult(TMappedObject value,
                            IEnumerable<MappingError> errors)
        {
            this.Value = value;
            this.Errors = new List<MappingError>(errors);
        }

        #endregion

        #region IMapperResult Implementation

        /// <inheritdoc />
        public TMappedObject Value { get; }

        /// <inheritdoc />
        public IList<MappingError> Errors { get; }

        #endregion
    }
}