// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-14 19:21
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Toolbox.CommandLineMapper.Core.Property;
using Toolbox.CommandLineMapper.Specification;
using Toolbox.Utils.Probing;

namespace Toolbox.CommandLineMapper.Core
{
    /// <summary>
    ///     A utility class that uses reflection to create
    ///     <see cref="IPropertyContainer{TAttribute}"/> for
    ///     objects whose properties are mapped to command line
    ///     arguments
    /// </summary>
    /// <typeparam name="TAttribute">
    ///    The type of attribute applied to the properties
    /// </typeparam>
    internal sealed class AttributedObjectReflector<TAttribute> where TAttribute : AttributeBase
    {
        #region Attributes

        /// <summary>
        ///     The object whose properties have an applied <see cref="OptionAttribute"/>
        /// </summary>
        private readonly Lazy<object> source;

        /// <summary>
        ///     Contains all properties of an object that have
        ///     an <see cref="OptionAttribute"/>
        /// </summary>
        private readonly Lazy<IPropertyContainer<TAttribute>> propertyContainer;

        /// <summary>
        ///     A factory that creates instances of <see cref="IAssignableProperty{TAttribute}"/>
        /// </summary>
        private readonly IAssignablePropertyFactory<TAttribute> assignablePropertyFactory;
        
        #endregion

        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="source">
        ///    The object that has properties with an applied
        ///     attribute
        /// </param>
        /// <param name="assignablePropertyFactory">
        ///     A factory that creates instances of <see cref="IAssignableProperty{TAttribute}"/>
        /// </param>
        public AttributedObjectReflector(object source, 
                                         IAssignablePropertyFactory<TAttribute> assignablePropertyFactory) : this(() => source,
                                                                                                                  assignablePropertyFactory)
        {

        }

        /// <summary>
        ///     Creates a new instance of the class. Using this
        ///     constructor allows to lazily create the
        ///     <see cref="object"/>.
        ///     The creation of the object happens when
        ///     the properties of the object are reflected
        ///     during the mapping
        /// </summary>
        /// <param name="objectFactory">
        ///     A factory that creates a new instance of the
        ///     <see cref="object"/>.
        ///
        ///     The factory must not return 'null'
        /// </param>
        /// <param name="assignablePropertyFactory">
        ///     A factory that creates instances of <see cref="IAssignableProperty{TAttribute}"/>
        /// </param>
        public AttributedObjectReflector(Func<object> objectFactory,
                                         IAssignablePropertyFactory<TAttribute> assignablePropertyFactory)
        {
            Guard.AgainstNullArgument(nameof(objectFactory), objectFactory);
            Guard.AgainstNullArgument(nameof(assignablePropertyFactory), assignablePropertyFactory);

            this.source = new Lazy<object>(objectFactory);
            this.propertyContainer = new Lazy<IPropertyContainer<TAttribute>>(this.CreateContainerForAttribute);
            this.assignablePropertyFactory = assignablePropertyFactory;
        }

        #endregion

        /// <summary>
        ///     The object whose properties have an applied <see cref="OptionAttribute"/>
        /// </summary>
        public object Source => this.source.Value;

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
        public IPropertyContainer<TAttribute> GetOptions()
        {
            return this.propertyContainer.Value;
        }

        #endregion

        #region Helper

        /// <summary>
        ///     Creates a new <see cref="IPropertyContainer{TAttribute}"/> from the object
        ///     passed as the constructor argument to this class.
        /// </summary>
        /// <typeparam name="TAttribute">
        ///     The type of attribute the property must have to be in the container
        /// </typeparam>
        /// <returns>
        ///     A new <see cref="IPropertyContainer{TAttribute}"/> for the object passed
        ///     as the constructor argument
        /// </returns>
        private IPropertyContainer<TAttribute> CreateContainerForAttribute()
        {
            return new DefaultPropertyContainer<TAttribute>(this.source.Value, this.GetRequiredAssignableProperties());
        }

        /// <summary>
        ///     Reflects upon the properties of the object passed as the
        ///     argument to the constructor and determines all
        ///     <see cref="IAssignableProperty{TAttribute}"/>s that are
        ///     required by the instance
        /// </summary>
        /// <typeparam name="TAttribute">
        ///     The type of the attribute the property must have to be considered
        ///     a <see cref="IAssignableProperty{TAttribute}"/>
        /// </typeparam>
        /// <returns>
        ///     A collection of <see cref="IAssignableProperty{TAttribute}"/>s of
        ///     the object passed as the constructor argument. Each returned
        ///     <see cref="IAssignableProperty{TAttribute}"/> represents a wrapper
        ///     around an actual property of the object.
        /// </returns>
        private IEnumerable<IAssignableProperty<TAttribute>> GetRequiredAssignableProperties()
        {
            return this.source.Value.GetType().GetProperties()
                       .TakeWhile(property => Attribute.IsDefined(property, typeof(TAttribute)))
                       .Select(this.GetSingleRequiredAssignableProperty);
        }

        /// <summary>
        ///     Gets a single instance of <see cref="IAssignableProperty{TAttribute}"/> that
        ///     is essentially a wrapper around <paramref name="property"/>
        /// </summary>
        /// <typeparam name="TAttribute">
        ///     The type of attribute this property has
        /// </typeparam>
        /// <param name="property">
        ///     The property from whom the <see cref="IAssignableProperty{TAttribute}"/> is a wrapper
        /// </param>
        /// <returns>
        ///     An instance of <see cref="IAssignableProperty{TAttribute}"/> that matches
        ///     the type of the <paramref name="property"/>
        /// </returns>
        private IAssignableProperty<TAttribute> GetSingleRequiredAssignableProperty(PropertyInfo property)
        {
            //Multiple attributes of the same type are not supported. Accessing [0] is fine
            var attribute = property.GetCustomAttributes(typeof(TAttribute), true)[0] as TAttribute;

            var assignableProperty = this.assignablePropertyFactory.CreatePropertyForType(property.PropertyType);
            
            assignableProperty.Property = property;
            assignableProperty.Attribute = attribute;
            assignableProperty.Owner = this.Source;

            if(attribute is not null)
                assignableProperty.Assign(attribute.Default);
            
            return assignableProperty;
        }

        #endregion


    }
}