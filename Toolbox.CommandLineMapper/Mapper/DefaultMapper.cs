// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-14 19:15
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Com.Toolbox.Utils.Probing;
using Toolbox.CommandLineMapper.Common;
using Toolbox.CommandLineMapper.Core;
using Toolbox.CommandLineMapper.Core.Property;
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
        private readonly List<MapperData> mapperDatas;

        /// <summary>
        ///     An instance of <see cref="IAssignablePropertyFactory{TAttribute}"/>
        /// </summary>
        private readonly IAssignablePropertyFactory<OptionAttribute> assignablePropertyFactory;

        #endregion

        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        public DefaultMapper() : this(new DefaultRegistrationService(),
                                      new DefaultAssignablePropertyFactory<OptionAttribute>())
        {
            
        }

        /// <summary>
        ///     Creates a new instance of the class. This constructor
        ///     allows to provide a custom <see cref="IRegistrationService"/>
        /// </summary>
        /// <param name="registrationService">
        ///    Handles types and instances, that can be registered
        ///     for command line mapping
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown if the passed argument is null
        /// </exception>
        public DefaultMapper(IRegistrationService registrationService) 
            : this(registrationService, new DefaultAssignablePropertyFactory<OptionAttribute>())
        {

        }

        /// <summary>
        ///     Creates a new instance of the class. This constructor allows
        ///     to provide a custom <see cref="IAssignablePropertyFactory{TAttribute}"/>
        /// </summary>
        /// <param name="assignablePropertyFactory">
        ///    The factory that provides instances of <see cref="IAssignableProperty{TAttribute}"/>
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown if the passed argument is null
        /// </exception>
        public DefaultMapper(IAssignablePropertyFactory<OptionAttribute> assignablePropertyFactory) 
            : this(new DefaultRegistrationService(), assignablePropertyFactory)
            
        {
            
        }

        /// <summary>
        ///     Creates a new instance of the class. This constructor allows to
        ///     specify a custom <see cref="IRegistrationService"/> and
        ///     <see cref="IAssignablePropertyFactory{TAttribute}"/>
        /// </summary>
        /// <param name="registrationService">
        ///    An instance of <see cref="IRegistrationService"/>
        /// </param>
        /// <param name="assignablePropertyFactory">
        ///    An instance of <see cref="IAssignablePropertyFactory{TAttribute}"/>
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown if either of the arguments is null
        /// </exception>
        public DefaultMapper(IRegistrationService registrationService,
                             IAssignablePropertyFactory<OptionAttribute> assignablePropertyFactory)
        {
            Guard.AgainstNullArgument(nameof(registrationService), registrationService);
            Guard.AgainstNullArgument(nameof(assignablePropertyFactory), assignablePropertyFactory);
            
            this.mapperDatas = new List<MapperData>();
            this.UnmappedArguments = Enumerable.Empty<Argument>();

            this.RegistrationService = registrationService;
            this.assignablePropertyFactory = assignablePropertyFactory;
        }

        #endregion

        #region ICommandLineMapper Implementation

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        ///    Thrown if an object with the passed type was
        ///     not registered using <see cref="RegistrationService"/>
        ///     before
        /// </exception>
        public IMapperResult<TMapTarget> GetMapperResult<TMapTarget>() where TMapTarget : class, new()
        { 
            if(!this.RegistrationService.IsRegistered<TMapTarget>())
                throw new ArgumentException($"The object '{typeof(TMapTarget).FullName}' is not registered");

            var data = this.mapperDatas.Find(m => m.Reflector.Source.GetType() == typeof(TMapTarget));
            
            return new MapperResult<TMapTarget>(data.Reflector.Source as TMapTarget,
                                                data.MappedArguments,
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
            this.ProcessArgumentList(args.ToArgument(options.OptionPrefix), options);
        }

        /// <inheritdoc />
        public IRegistrationService RegistrationService { get; }

        /// <inheritdoc />
        public IEnumerable<Argument> UnmappedArguments { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Creates instances of <see cref="MapperData"/> for
        ///     each object currently registered at the
        ///     <see cref="RegistrationService"/>
        /// </summary>
        private void CreateMapperDataObjects()
        {
            foreach (var type in this.RegistrationService)
            {
                this.mapperDatas.Add(new MapperData
                {
                    Reflector = new AttributedObjectReflector<OptionAttribute>(this.RegistrationService.GetInstanceOf(type),
                                                                               this.assignablePropertyFactory)
                });
            }
        }
        
        /// <summary>
        ///     Processes the passed <paramref name="arguments"/> by applying
        ///     the mapping to all registered objects
        /// </summary>
        /// <param name="arguments">
        ///     The list of arguments passed via command line
        /// </param>
        /// <param name="options">
        ///     The <see cref="MapperOptions"/> passed to on of the mapping
        ///     methods
        /// </param>
        private void ProcessArgumentList(IEnumerable<Argument> arguments, MapperOptions options)
        {
            var argsList = arguments.ToList();
            
            foreach (var mapperData in this.mapperDatas)
            {
                foreach (var argument in argsList)
                {
                    //Argument is already mapped to some object. We do not map the same arg twice
                    if(argument.IsMapped)
                        continue;
                    
                    MapCommandLineValueToSingleObject(argument, 
                                                      mapperData);

                    if (mapperData.Errors.Any() && !options.ContinueOnError)
                        break;
                }
            }

            this.UnmappedArguments = argsList.Where(a => !a.IsMapped);
        }

        /// <summary>
        ///     Maps a single pair of option and value to all suitable
        ///     properties of the object held by <paramref name="mapperData.Reflector"/>
        /// </summary>
        /// <param name="argument">
        ///     A single <see cref="Argument"/> passed via command line
        /// </param>
        /// <param name="mapperData">
        ///    Contains objects required for mapping and receives the result
        /// </param>
        private static void MapCommandLineValueToSingleObject(Argument argument,
                                                              MapperData mapperData)
        {
            try
            {
                MapCommandLineValueToOption(argument, mapperData.Reflector.GetOptions());
                
                if(argument.IsMapped)
                    mapperData.MappedArguments.Add(argument);
            }
            catch (Exception e)
            {
                mapperData.Errors.Add(new MappingError
                {
                    Argument = argument,
                    Cause = e
                });
            }
        }

        /// <summary>
        ///     Maps a single <paramref name="argument"/> to the
        ///     first property in the <see cref="IPropertyContainer{TAttribute}"/>
        ///     whose name matches the <see cref="KeyValuePair{TKey,TValue}.Key"/>
        /// </summary>
        /// <param name="argument">
        ///    A single <see cref="Argument"/>
        /// </param>
        /// <param name="optionProperties">
        ///    A collection of properties that have an <see cref="OptionAttribute"/>
        /// </param>
        /// <exception cref="InvalidCastException">
        ///    Thrown if the 'Value' of the passed <paramref name="argument"/>
        ///     can not be cast/converted to the type of the property.
        /// </exception>
        private static void MapCommandLineValueToOption(Argument argument, 
                                                        IPropertyContainer<OptionAttribute> optionProperties)
        {
            if (optionProperties.Properties == 0)
                return;

            try
            {
                var option = optionProperties.GetProperty(argument.CommandWithoutPrefix);

                option.Assign(argument.HasValue ? argument.Value : string.Empty);

                argument.IsMapped = true;
            }
            catch (PropertyNotFoundException)
            {
                argument.IsMapped = false;
            }
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
            public AttributedObjectReflector<OptionAttribute> Reflector { get; set; }
            
            /// <summary>
            ///     Contains all arguments, that were mapped to the object
            ///     held by <see cref="Reflector"/>
            /// </summary>
            public IList<Argument> MappedArguments { get; } = new List<Argument>();
            
            /// <summary>
            ///     Contains errors that were caused, when the object held by
            ///     <see cref="Reflector"/> was mapped to command line arguments
            /// </summary>
            public IList<MappingError> Errors { get; } = new List<MappingError>();
        }

        #endregion
    }
}