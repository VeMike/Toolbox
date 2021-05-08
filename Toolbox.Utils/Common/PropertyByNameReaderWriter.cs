// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-08 20:38
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Toolbox.Utils.Common
{
    /// <summary>
    ///     A utility class that gets or sets the values of an
    ///     objects properties using their name.
    ///
    ///     The class only allows to access and/or modify the
    ///     values of properties that have a public modifier
    /// </summary>
    public class PropertyByNameReaderWriter
    {
        #region Attributes

        /// <summary>
        ///     The properties of the object passed as the
        ///     constructor argument
        /// </summary>
        private readonly IDictionary<string, PropertyInfo> properties;

        /// <summary>
        ///     The name of the object passed to the constructor
        /// </summary>
        private readonly string objectName;

        /// <summary>
        ///     The object whose properties can be modified by
        ///     this instance
        /// </summary>
        private readonly object target;
        
        #endregion

        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="target">
        ///    The target object whose properties will be set
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown if the passed argument is null
        /// </exception>
        public PropertyByNameReaderWriter(object target)
        {
            if(target is null)
                throw new ArgumentNullException(nameof(target));

            this.objectName = target.GetType().FullName;
            this.target = target;
            this.properties = GetObjectProperties(target);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sets the property <paramref name="name"/> to a
        ///     value of <paramref name="value"/>
        /// </summary>
        /// <param name="name">
        ///    The name of the property whose value is set
        /// </param>
        /// <param name="value">
        ///    The new value of the property
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown if either of the arguments is 'null'
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///    Throws if a property with <paramref name="name"/> is not found
        /// </exception>
        /// <exception cref="FieldAccessException">
        ///     - The property with <paramref name="name"/> does not have a public 'get' accessor
        /// </exception>
        public void SetProperty(string name, object value)
        {
            if(name is null)
                throw new ArgumentNullException(nameof(name));
            if(value is null)
                throw new ArgumentNullException(nameof(value));

            if (!this.properties.TryGetValue(name, out var property))
            {
                throw new InvalidOperationException($"Property '{this.objectName}.{name}' not found");
            }

            this.SetPropertyValue(property, value);
        }

        /// <summary>
        ///     Gets the value of property <paramref name="name"/>
        /// </summary>
        /// <param name="name">
        ///    The name of the property whose value is read
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///    Thrown if the argument is 'null'
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///    Thrown if:
        ///     - A property with <paramref name="name"/> is not found.
        /// </exception>
        /// <exception cref="FieldAccessException">
        ///     - The property with <paramref name="name"/> does not have a public 'get' accessor
        /// </exception>
        public object GetProperty(string name)
        {
            if(name is null)
                throw new ArgumentNullException(nameof(name));
            
            if (this.properties.TryGetValue(name, out var property))
            {
                return this.GetPropertyValue(property);
            }
            
            throw new InvalidOperationException($"Property '{this.objectName}.{name}' not found");
        }

        /// <summary>
        ///     Sets the value of <paramref name="property"/> to <paramref name="value"/>
        /// </summary>
        /// <param name="property">
        ///    The property whose value is set
        /// </param>
        /// <param name="value">
        ///    The new value of the property
        /// </param>
        /// <exception cref="FieldAccessException">
        ///     Thrown if:
        ///         - The <paramref name="property"/> has no public setter
        ///         - The <paramref name="property"/> has no setter at all
        /// </exception>
        private void SetPropertyValue(PropertyInfo property, object value)
        {
            if (property.SetMethod is null)
            {
                throw new FieldAccessException($"Property '{this.objectName}.{property.Name}' " +
                                               $"has no setter");
            }
            
            if (!property.SetMethod.IsPublic)
            {
                throw new FieldAccessException($"Property '{this.objectName}.{property.Name}' " +
                                               $"has no pubic setter");
            }
            
            property.SetValue(this.target, value);
        }
        
        /// <summary>
        ///     Gets the value of the passed property
        /// </summary>
        /// <param name="property">
        ///    The property whose value shall be read
        /// </param>
        /// <returns>
        ///    The value of the property
        /// </returns>
        /// <exception cref="FieldAccessException">
        ///     Thrown if:
        ///         - The <paramref name="property"/> has no public getter
        ///         - The <paramref name="property"/> has no getter at all
        /// </exception>
        private object GetPropertyValue(PropertyInfo property)
        {
            if (property.GetMethod is null)
            {
                throw new FieldAccessException($"Property '{this.objectName}.{property.Name}' " +
                                               $"has no getter");
            }
            
            if (!property.GetMethod.IsPublic)
            {
                throw new FieldAccessException($"Property '{this.objectName}.{property.Name}' " +
                                               $"has no pubic getter");
            }

            return property.GetValue(this.target);
        }

        /// <summary>
        ///     Gets the properties of the passed object using
        ///     reflection
        /// </summary>
        /// <param name="target">
        ///    The object whose properties shall be reflected
        /// </param>
        /// <returns>
        ///    A collection of the objects properties
        /// </returns>
        private static IDictionary<string, PropertyInfo> GetObjectProperties(object target)
        {
            return target.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public)
                         .ToDictionary(p => p.Name, p => p);
        }

        #endregion
    }
}