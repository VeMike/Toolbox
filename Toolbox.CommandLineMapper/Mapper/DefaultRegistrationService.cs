// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-05-21 23:54
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Toolbox.CommandLineMapper.Mapper
{
    /// <summary>
    ///     The implementation of <see cref="IRegistrationService"/>
    /// </summary>
    public class DefaultRegistrationService : IRegistrationService
    {
        #region Attributes

        /// <summary>
        ///     A collection of all types (and instances) currently
        ///     registered at this instance.
        /// </summary>
        private readonly IDictionary<Type, object> registrations;

        #endregion

        #region Constructor

        /// <summary>
        ///     Creates a new instance
        /// </summary>
        public DefaultRegistrationService()
        {
            this.registrations = new Dictionary<Type, object>();
        }

        #endregion

        #region IRegistrationService Implementation

        /// <inheritdoc />
        public IEnumerator<Type> GetEnumerator() => this.registrations.Keys.ToList().GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <inheritdoc />
        public void Register<TMapTarget>() where TMapTarget : class, new()
        {
            var type = typeof(TMapTarget);
            
            if (!this.registrations.ContainsKey(type))
            {
                this.registrations.Add(type, new TMapTarget());
            }
        }

        /// <inheritdoc />
        public void UnRegister<TMapTarget>() where TMapTarget : class, new()
        {
            this.registrations.Remove(typeof(TMapTarget));
        }

        /// <inheritdoc />
        public bool IsRegistered<TMapTarget>() where TMapTarget : class, new()
        {
            return this.registrations.ContainsKey(typeof(TMapTarget));
        }

        /// <inheritdoc />
        /// <exception cref="KeyNotFoundException">
        ///    Thrown if an instance of type <paramref name="type"/>
        ///     is not found
        /// </exception>
        public object GetInstanceOf(Type type)
        {
            if (this.registrations.TryGetValue(type, out var instance))
            {
                return instance;
            }
            
            throw new KeyNotFoundException($"An instance of type '{type.FullName}' was not found");
        }

        /// <inheritdoc />
        public int Registrations => this.registrations.Count;

        #endregion
    }
}