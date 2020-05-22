// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-21 23:32
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;

namespace Toolbox.CommandLineMapper.Mapper
{
    /// <summary>
    ///     Handles registrations for types to whose instances
    ///     command line arguments can be mapped.
    ///
    ///     When enumerated, yields all <see cref="Type"/>s, that
    ///     are currently registered at this class.
    /// </summary>
    public interface IRegistrationService : IEnumerable<Type>
    {
        #region Methods

        /// <summary>
        ///     Registers a new object type at the mapper. Each type
        ///     can only be registered once. Multiple calls to
        ///     <see cref="Register{T}"/> using the same type will
        ///     be ignored.
        /// </summary>
        /// <typeparam name="TMapTarget">
        ///     The object to register. This is the object to
        ///     whom command line arguments are mapped.
        /// </typeparam>
        void Register<TMapTarget>() where TMapTarget : class, new();

        /// <summary>
        ///     The inverse method of <see cref="Register{T}"/>.
        ///     Unregisters a type previously added.
        /// </summary>
        /// <typeparam name="TMapTarget">
        ///     The object to unregister. This object will no longer
        ///     be used for mapping of command line arguments
        /// </typeparam>
        void UnRegister<TMapTarget>() where TMapTarget : class, new();

        /// <summary>
        ///     Checks, if a type is registered at this instance
        /// </summary>
        /// <typeparam name="TMapTarget">
        ///     The type whose registration shall be checked
        /// </typeparam>
        /// <returns>
        ///     'true' if the type is registered, 'false' otherwise
        /// </returns>
        bool IsRegistered<TMapTarget>() where TMapTarget : class, new();

        /// <summary>
        ///     Gets an instance of the passed <see cref="type"/>
        /// </summary>
        /// <returns>
        ///    An instance of <paramref name="type"/>. Each
        ///     call returns the same instance. 
        /// </returns>
        object GetInstanceOf(Type type);

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