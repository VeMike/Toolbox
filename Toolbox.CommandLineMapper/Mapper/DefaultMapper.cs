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
        ///     Contains helpers required during object mapping
        /// </summary>
        private readonly IList<MapperData> mapperData;

        #endregion

        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        public DefaultMapper() : this(new DefaultRegistrationService())
        {

        }

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="registrationService">
        ///    Handles types and instances, that can be registered
        ///     for command line mapping
        /// </param>
        public DefaultMapper(IRegistrationService registrationService)
        {
            this.mapperData = new List<MapperData>();

            this.RegistrationService = registrationService;
        }

        #endregion

        #region ICommandLineMapper Implementation

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        ///    Thrown if an object with the passed type was
        ///     not registered using <see cref="Register{T}"/>
        ///     before
        /// </exception>
        public IMapperResult<TMapTarget> GetMapperResult<TMapTarget>() where TMapTarget : class, new()
        { 
            if(!this.RegistrationService.IsRegistered<TMapTarget>())
                throw new ArgumentException($"The object '{typeof(TMapTarget).FullName}' is not registered");

            var data = this.registeredObjects[typeof(TMapTarget)];
            
            return new MapperResult<TMapTarget>(data.Reflector.Source as TMapTarget,
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

            this.CreateMapperDataObjects();
            
            // ReSharper disable once PossibleMultipleEnumeration
            this.ProcessArgumentList(args.ToList(), options);
        }

        /// <inheritdoc />
        public IRegistrationService RegistrationService { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Creates instances of <see cref="MapperData"/> for
        ///     each object currently registered at the
        ///     <see cref="RegistrationService"/>
        /// </summary>
        private void CreateMapperDataObjects()
        {
            throw new NotImplementedException();
        }
        
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

                optionValuePairs.Add(argsList[i].ToLower(), argsList[i + 1]);
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

                    if (mapperData.Errors.Any() && !mapperOptions.ContinueOnError)
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
            if (optionProperties.Properties == 0)
                return;
            
            var option = optionProperties.GetProperty(optionValuePair.Key);
            
            option.Assign(optionValuePair.Value);
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