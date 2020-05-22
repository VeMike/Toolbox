// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-22 22:27
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Core.Property
{
    /// <summary>
    ///     A factory, that creates instances of <see cref="IAssignableProperty{TAttribute}"/>
    /// </summary>
    public interface IAssignablePropertyFactory<TAttribute> where TAttribute : AttributeBase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="IAssignableProperty{TAttribute}"/>
        ///     that allows assignment of objects of type <paramref name="type"/>
        /// </summary>
        /// <param name="type">
        ///    The type of object that can be assigned to the returned property
        /// </param>
        /// <returns>
        ///    A new instance of <see cref="IAssignableProperty{TAttribute}"/>
        /// </returns>
        IAssignableProperty<TAttribute> CreatePropertyForType(Type type);
    }
}