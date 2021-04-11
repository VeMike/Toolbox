// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 19:56
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Reflection;

namespace Toolbox.CommandLineMapper.Core.Property
{
    /// <summary>
    ///     Defines a wrapper around <see cref="PropertyInfo"/> that is assignable
    ///     from a string value
    /// </summary>
    public interface IAssignableProperty<TAttribute> where TAttribute : Attribute
    {
        #region Properties

        /// <summary>
        ///     Gets the name of the property this instance
        ///     wraps around.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the object the property belongs to
        /// </summary>
        object Owner { get; set; }

        /// <summary>
        ///     The <see cref="Attribute"/> this <see cref="IAssignableProperty{TAttribute}"/>
        ///     has applied
        /// </summary>
        TAttribute Attribute { get; set; }

        /// <summary>
        ///     The <see cref="Type"/> of object that can be assigned to this
        ///     <see cref="IAssignableProperty{TAttribute}"/>
        /// </summary>
        Type AssignableType { get; }
        
        /// <summary>
        ///     Sets the property whose value is assigned
        ///     when calling <see cref="Assign"/>
        /// </summary>
        PropertyInfo Property { set; }
        #endregion

        #region Methods

        /// <summary>
        ///     Assigns the <paramref name="value"/> to the property
        ///     wrapped by this instance
        /// </summary>
        /// <param name="value">
        ///     The value assigned to the property
        /// </param>
        void Assign(string value);

        #endregion
        
    }
}