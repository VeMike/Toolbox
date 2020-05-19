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
using Toolbox.CommandLineMapper.Core.Wrappers;
using Toolbox.CommandLineMapper.Specification;

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
        ///     attributes are reflected and mapped
        /// </summary>
        private readonly IDictionary<Type, MapperData> registeredObjects;

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
            this.registeredObjects = new Dictionary<Type, MapperData>(10);
        }

        #endregion

        #region ICommandLineMapper Implementation

        /// <inheritdoc />
        public void Register<T>() where T : class, new()
        {
            if (this.IsRegistered<T>())
                return;

            var mapperData = new MapperData() {Reflector = new AttributedObjectReflector(() => new T())};
            
            this.registeredObjects.Add(typeof(T), mapperData);
        }

        /// <inheritdoc />
        public void UnRegister<T>() where T : class, new()
        {
            this.registeredObjects.Remove(typeof(T));
        }

        /// <inheritdoc />
        public bool IsRegistered<T>() where T : class, new()
        {
            return this.registeredObjects.ContainsKey(typeof(T));
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        ///    Thrown if an object with the passed type was
        ///     not registered using <see cref="Register{T}"/>
        ///     before
        /// </exception>
        public IMapperResult<T> GetMapperResult<T>() where T : class, new()
        { 
            if(!this.IsRegistered<T>())
                throw new ArgumentException($"The object '{typeof(T).FullName}' is not registered");

            var data = this.registeredObjects[typeof(T)];
            
            return new MapperResult<T>(data.Reflector.Source as T,
                                       data.Errors);
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
            this.ProcessArgumentList(args.Select(a => a.ToLower()).ToList(), options);
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
        ///     The list of arguments passed via command line
        /// </param>
        /// <param name="options">
        ///     The <see cref="MapperOptions"/> passed to on of the mapping
        ///     methods
        /// </param>
        private void ProcessArgumentList(IList<string> argsList, MapperOptions options)
        {
            var optionValuePairs = new Dictionary<string, string>();

            for (int i = 0; i < argsList.Count - 1; i += 2)
            {
                argsList[i] = argsList[i].Replace(options.OptionPrefix, string.Empty);

                if (optionValuePairs.ContainsKey(argsList[i]))
                    continue;

                optionValuePairs.Add(argsList[i], argsList[i + 1]);
            }

            this.MapCommandLineValuesToObjects(optionValuePairs, options);
        }

        /// <summary>
        ///     Maps all passed <paramref name="optionValuePairs"/> to the objects that
        ///     were registered at this mapper class
        /// </summary>
        /// <param name="optionValuePairs">
        ///     The pairs of options and values that are mapped to objects properties.
        /// </param>
        /// <param name="mapperOptions">
        ///    Options for the mapper
        /// </param>
        private void MapCommandLineValuesToObjects(IDictionary<string, string> optionValuePairs,
                                                   MapperOptions mapperOptions)
        {
            foreach (var mapperData in this.registeredObjects.Values)
            {
                foreach (var optionValuePair in optionValuePairs)
                {
                    MapCommandLineValueToSingleObject(optionValuePair, 
                                                      mapperData);

                    if (!mapperOptions.ContinueOnError)
                        break;
                }
            }
        }

        /// <summary>
        ///     Maps a single pair of option and value to all suitable
        ///     properties of the object held by <paramref name="mapperData.Reflector"/>
        /// </summary>
        /// <param name="optionValuePair">
        ///     A single pair of option and value
        /// </param>
        /// <param name="mapperData">
        ///    Contains objects required for mapping and receives the result
        /// </param>
        private static void MapCommandLineValueToSingleObject(KeyValuePair<string, string> optionValuePair,
                                                              MapperData mapperData)
        {
            try
            {
                MapCommandLineValueToOption(optionValuePair, mapperData.Reflector.GetOptions());

                MapCommandLineValueToValue(optionValuePair, mapperData.Reflector.GetValues());
            }
            catch (Exception e)
            {
                mapperData.Errors.Add(new MappingError
                {
                    OptionName = optionValuePair.Key,
                    OptionValue = optionValuePair.Value,
                    Cause = e
                });
            }
        }

        /// <summary>
        ///     Maps a single <paramref name="optionValuePair"/> to the
        ///     first property in the <see cref="IPropertyContainer{TAttribute}"/>
        ///     whose name matches the <see cref="KeyValuePair{TKey,TValue}.Key"/>
        /// </summary>
        /// <param name="optionValuePair">
        ///    A single option-value pair
        /// </param>
        /// <param name="optionProperties">
        ///    A collection of properties that have an <see cref="OptionAttribute"/>
        /// </param>
        /// <exception cref="PropertyNotFoundException">
        ///    Thrown if a property with a name specified as 'Key' of the passed
        ///     <paramref name="optionValuePair"/> is not found.
        /// </exception>
        /// <exception cref="InvalidCastException">
        ///    Thrown if the 'Value' of the passed <paramref name="optionValuePair"/>
        ///     can not be cast/converted to the type of the property.
        /// </exception>
        private static void MapCommandLineValueToOption(KeyValuePair<string, string> optionValuePair, 
                                                        IPropertyContainer<OptionAttribute> optionProperties)
        {
            var option = optionProperties.GetProperty(optionValuePair.Key);
            
            option.Assign(optionValuePair.Value);
        }

        /// <summary>
        ///     Maps a single <paramref name="optionValuePair"/> to the
        ///     first property in the <see cref="IPropertyContainer{TAttribute}"/>
        ///     whose name matches the <see cref="KeyValuePair{TKey,TValue}.Key"/>
        /// </summary>
        /// <param name="optionValuePair">
        ///    A single option-value pair
        /// </param>
        /// <param name="valueProperties">
        ///    A collection of properties that have an <see cref="ValueAttribute"/>
        /// </param>
        /// <exception cref="PropertyNotFoundException">
        ///    Thrown if a property with a name specified as 'Key' of the passed
        ///     <paramref name="optionValuePair"/> is not found.
        /// </exception>
        /// <exception cref="InvalidCastException">
        ///    Thrown if the 'Value' of the passed <paramref name="optionValuePair"/>
        ///     can not be cast/converted to the type of the property.
        /// </exception>
        private static void MapCommandLineValueToValue(KeyValuePair<string, string> optionValuePair, 
                                                       IPropertyContainer<ValueAttribute> valueProperties)
        {
            var value = valueProperties.GetProperty(optionValuePair.Key);
            
            value.Assign(optionValuePair.Value);
        }

        #endregion

        #region Nested Types

        /// <summary>
        ///     Just a single data object that contains objects required
        ///     during the mapping
        /// </summary>
        private class MapperData
        {
            /// <summary>
            ///     Contains a single object with its properties
            /// </summary>
            public AttributedObjectReflector Reflector { get; set; }
            
            /// <summary>
            ///     Contains errors that were caused, when the object held by
            ///     <see cref="Reflector"/> was mapped to command line arguments
            /// </summary>
            public IList<MappingError> Errors { get; } = new List<MappingError>();
        }

        #endregion
    }
}