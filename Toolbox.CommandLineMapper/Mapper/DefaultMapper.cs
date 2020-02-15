// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-14 19:15
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using Toolbox.CommandLineMapper.Core;

namespace Toolbox.CommandLineMapper.Mapper
{
    /// <summary>
    ///     The default implementation of <see cref="ICommandLineMapper"/>
    /// </summary>
    public sealed class DefaultMapper : ICommandLineMapper
    {
        #region Attributes

        /// <summary>
        ///     Contains the collection of <see cref="object"/> whose
        ///     attributes are reflected.
        /// </summary>
        private readonly IDictionary<Type, AttributedObjectReflector> registeredObjects;

        #endregion

        #region Constructor
        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        public DefaultMapper()
        {
            /*
             * We set the default size to 10, since it is not considered typical to
             * map many objects to command line parameters. The cost of resizing the
             * collection is not that high if more than 10 objects are added to the
             * mapper (such a mapping typically happens just once when an application
             * is started).
             */
            this.registeredObjects = new Dictionary<Type, AttributedObjectReflector>(10);
        }

        #endregion

        #region ICommandLineMapper Implementation

        /// <inheritdoc />
        public void Register<T>() where T : new()
        {
            if (this.IsRegistered<T>())
                return;
            
            this.registeredObjects.Add(typeof(T), new AttributedObjectReflector(() => new T()));
        }

        /// <inheritdoc />
        public void UnRegister<T>() where T : new()
        {
            this.registeredObjects.Remove(typeof(T));
        }

        /// <inheritdoc />
        public bool IsRegistered<T>() where T : new()
        {
            return this.registeredObjects.ContainsKey(typeof(T));
        }

        /// <inheritdoc />
        public IMapperResult<T> GetMapperResult<T>() where T : new()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void Map(IEnumerable<string> args)
        {
            this.Map(args, new MapperOptions());
        }

        /// <inheritdoc />
        public void Map(IEnumerable<string> args, MapperOptions options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int Registrations => this.registeredObjects.Count;

        #endregion
    }
}