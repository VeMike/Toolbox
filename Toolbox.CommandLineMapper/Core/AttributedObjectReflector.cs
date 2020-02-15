// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-14 19:21
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Com.Toolbox.Utils.Probing;
using Toolbox.CommandLineMapper.Core.Wrappers;
using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Core
{
    /// <summary>
    ///     A utility class that uses reflection to create
    ///     <see cref="IPropertyContainer{TAttribute}"/> for
    ///     objects whose properties are mapped to command line
    ///     arguments
    /// </summary>
    internal sealed class AttributedObjectReflector
    {
        #region Attributes

        /// <summary>
        ///     applied <see cref="OptionAttribute"/> or
        ///     <see cref="ValueAttribute"/> should be reflected  
        /// </summary>
        private readonly Lazy<object> source;

        #endregion

        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="source">
        ///     The <see cref="object"/> whose properties with
        ///     applied <see cref="OptionAttribute"/> or
        ///     <see cref="ValueAttribute"/> should be reflected
        /// </param>
        public AttributedObjectReflector(object source) : this(() => source)
        {

        }

        /// <summary>
        ///     Creates a new instance of the class. Using this
        ///     constructor allows to lazily create the
        ///     <see cref="object"/>.
        ///     The creation of the object happens when
        ///     <see cref="GetOptions"/> or <see cref="GetValues"/>
        ///     of this class are called
        /// </summary>
        /// <param name="objectFactory">
        ///     A factory that creates a new instance of the
        ///     <see cref="object"/>.
        ///
        ///     The factory must not return 'null'
        /// </param>
        public AttributedObjectReflector(Func<object> objectFactory)
        {
            Guard.AgainstNullArgument(nameof(objectFactory), objectFactory);

            this.source = new Lazy<object>(objectFactory);
        }

        #endregion

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
        public IPropertyContainer<OptionAttribute> GetOptions()
        {
            return this.CreateContainerForAttribute<OptionAttribute>();
        }

        /// <summary>
        ///     A wrapper around the <see cref="object"/> passed as
        ///     constructor argument, that contains all properties
        ///     of this object who have an applied <see cref="OptionAttribute"/>
        /// </summary>
        /// <returns>
        ///     The <see cref="IPropertyContainer{TAttribute}"/> that represents
        ///     a wrapper around the constructor argument.
        /// </returns>
        public IPropertyContainer<ValueAttribute> GetValues()
        {
            return this.CreateContainerForAttribute<ValueAttribute>();
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
        private IPropertyContainer<TAttribute> CreateContainerForAttribute<TAttribute>() where TAttribute : Attribute
        {
            return new DefaultPropertyContainer<TAttribute>(this.source.Value, this.CreateAssignableProperties<TAttribute>());
        }

        /// <summary>
        ///     Reflects upon the properties of the object passed as the
        ///     argument to the constructor and creates a new
        ///     <see cref="IAssignableProperty{TAttribute}"/> for each of
        ///     the objects properties, that has the specified type of attribute
        /// </summary>
        /// <typeparam name="TAttribute">
        ///     The type of the attribute the property must have to be considered
        ///     a <see cref="IAssignableProperty{TAttribute}"/>
        /// </typeparam>
        /// <returns>
        ///     A collection of <see cref="IAssignableProperty{TAttribute}"/>s of
        ///     the object passed as the constructor argument
        /// </returns>
        private IEnumerable<IAssignableProperty<TAttribute>> CreateAssignableProperties<TAttribute>() where TAttribute : Attribute
        {
            foreach (var property in this.source.Value.GetType().GetProperties())
            {
                yield return this.CreateTypedAssignableProperty<TAttribute>(property);
            }
        }

        /// <summary>
        ///     Creates an instance of <see cref="IAssignableProperty{TAttribute}"/> from
        ///     the passed <paramref name="property"/>. The actual implementation of
        ///     <see cref="IAssignableProperty{TAttribute}"/> returned here matches the
        ///     type of the <paramref name="property"/>
        /// </summary>
        /// <typeparam name="TAttribute">
        ///     The type of attribute this property has
        /// </typeparam>
        /// <param name="property">
        ///     The property from whom the <see cref="IAssignableProperty{TAttribute}"/> is created
        /// </param>
        /// <returns>
        ///     A new instance of <see cref="IAssignableProperty{TAttribute}"/> that matches
        ///     the type of the <paramref name="property"/>
        /// </returns>
        private IAssignableProperty<TAttribute> CreateTypedAssignableProperty<TAttribute>(PropertyInfo property) where TAttribute : Attribute
        {
            var propertyType = property.GetType();
            //Multiple attributes of the same type are not supported. Accessing [0] is fine
            var attribute = property.GetCustomAttributes(typeof(TAttribute), true)[0] as TAttribute;

            if (propertyType == typeof(string))
            {
                return new StringAssignableProperty<TAttribute>(this.source.Value, property, attribute);
            }
            if (propertyType == typeof(int))
            {
                return new IntegerAssignableProperty<TAttribute>(this.source.Value, property, attribute);
            }
            if (propertyType == typeof(bool))
            {
                return new BooleanAssignableProperty<TAttribute>(this.source.Value, property, attribute);
            }
            if (propertyType == typeof(char))
            {
                return new CharAssignableProperty<TAttribute>(this.source.Value, property, attribute);
            }
            if (propertyType == typeof(byte))
            {
                return new ByteAssignableProperty<TAttribute>(this.source.Value, property, attribute);
            }
            if (propertyType == typeof(short))
            {
                return new BooleanAssignableProperty<TAttribute>(this.source.Value, property, attribute);
            }
            if (propertyType == typeof(long))
            {
                return new LongAssignableProperty<TAttribute>(this.source.Value, property, attribute);
            }
            if (propertyType == typeof(float))
            {
                return new FloatAssignableProperty<TAttribute>(this.source.Value, property, attribute);
            }
            if (propertyType == typeof(double))
            {
                return new DoubleAssignableProperty<TAttribute>(this.source.Value, property, attribute);
            }

            throw new NotSupportedException($"Mapping to properties of type '{propertyType.Name}' is currently not supported");
        }

        #endregion


    }
}