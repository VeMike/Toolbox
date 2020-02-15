// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-14 19:15
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Com.Toolbox.Utils.Probing;
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
        public void Map(IEnumerable<string> args) => this.Map(args, new MapperOptions());

        /// <inheritdoc />
        public void Map(IEnumerable<string> args, MapperOptions options)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            Guard.AgainstNullArgument(nameof(args), args);
            Guard.AgainstNullArgument(nameof(options), options);

            // ReSharper disable once PossibleMultipleEnumeration
            this.ProcessArgumentList(args.ToList(), options);
        }

        /// <inheritdoc />
        public int Registrations => this.registeredObjects.Count;

        #endregion

        #region Methods

        /// <summary>
        ///     Processes the passed <paramref name="argsList"/> by applying
        ///     the mapping to all registered objects
        /// </summary>
        /// <param name="argsList">
        ///     A l
        /// </param>
        /// <param name="options">
        ///     The <see cref="MapperOptions"/> passed to on of the mapping
        ///     methods
        /// </param>
        private void ProcessArgumentList(IList<string> argsList, MapperOptions options)
        {
            var optionValuePairs = new Dictionary<string, string>();

            for (int i = 0; i < argsList.Count - 1; i++)
            {
                /*
                 * We only need every 2nd iteration. Every first iteration
                 * is the option, the second the value
                 */
                if (i % 2 == 0)
                    continue;

                argsList[i] = argsList[i].Replace(options.OptionPrefix, string.Empty);

                if (optionValuePairs.ContainsKey(argsList[i]))
                    continue;

                optionValuePairs.Add(argsList[i], argsList[i + 1]);
            }

            this.MapCommandLineValuesToObjects(optionValuePairs);

            //TODO: Implement this

            //1. Create pairs of option <-> value

            //2. Search the registered types for all objects having a property with option-name

            //3. Assign the value to the correct property of all found objects

            //4. Create IMappingResult
            //4.1 What information should be in 'IMapperResult'
        }

        private void MapCommandLineValuesToObjects(IDictionary<string, string> optionValuePairs)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}