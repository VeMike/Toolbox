// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-14 19:21
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using Com.Toolbox.Utils.Probing;
using Toolbox.CommandLineMapper.Core.Wrappers;
using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Core
{
    /// <summary>
    ///     A utility class that uses reflection to create
    ///     <see cref="IPropertyContainer{TAttribute}"/> for
    ///     objects whose properties are mapped to command line
    ///     arguments
    /// </summary>
    internal sealed class AttributedObjectReflector
    {
        #region Attributes

        /// <summary>
        ///     applied <see cref="OptionAttribute"/> or
        ///     <see cref="ValueAttribute"/> should be reflected  
        /// </summary>
        private readonly object source;

        #endregion

        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="source">
        ///     The <see cref="object"/> whose properties with
        ///     applied <see cref="OptionAttribute"/> or
        ///     <see cref="ValueAttribute"/> should be reflected
        /// </param>
        public AttributedObjectReflector(object source)
        {
            Guard.AgainstNullArgument(nameof(source), source);

            this.source = source;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     A wrapper around the <see cref="object"/> passed as
        ///     constructor argument, that contains all properties
        ///     of this object who have an applied <see cref="OptionAttribute"/>
        /// </summary>
        /// <returns>
        ///     The <see cref="IPropertyContainer{TAttribute}"/> that represents
        ///     a wrapper around the constructor argument.
        /// </returns>
        public IPropertyContainer<OptionAttribute> GetOptions()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     A wrapper around the <see cref="object"/> passed as
        ///     constructor argument, that contains all properties
        ///     of this object who have an applied <see cref="OptionAttribute"/>
        /// </summary>
        /// <returns>
        ///     The <see cref="IPropertyContainer{TAttribute}"/> that represents
        ///     a wrapper around the constructor argument.
        /// </returns>
        public IPropertyContainer<ValueAttribute> GetValues()
        {
            throw new NotImplementedException();
        }

        #endregion

        
    }
}