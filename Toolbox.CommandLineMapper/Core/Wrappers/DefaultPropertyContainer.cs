// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 20:18
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Com.Toolbox.Utils.Probing;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IPropertyContainer{TAttribute}"/> that just
    ///     holds a collection of <see cref="IAssignableProperty{TAttribute}"/>s
    /// </summary>
    internal class DefaultPropertyContainer<TAttribute> : IPropertyContainer<TAttribute> where TAttribute : Attribute
    {
        #region Attributes

        /// <summary>
        ///     The property container
        /// </summary>
        private Dictionary<string, IAssignableProperty<TAttribute>> properties;

        #endregion

        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="source">
        ///     The containing type of the collection
        ///     of <paramref name="properties"/>
        /// </param>
        /// <param name="properties">
        ///     The collection of propertyCollection contained
        ///     in <paramref name="source"/>
        /// </param>
        public DefaultPropertyContainer(object source, IEnumerable<IAssignableProperty<TAttribute>> properties)
        {
            Guard.AgainstNullArgument(nameof(source), source);

            this.Source = source;
            this.properties = new Dictionary<string, IAssignableProperty<TAttribute>>();

            this.PutPropertiesInContainer(properties);
        } 

        #endregion

        #region IPropertyContainer Implementation

        /// <inheritdoc />
        public object Source { get; }

        /// <inheritdoc />
        public IAssignableProperty<TAttribute> GetProperty(string name)
        {
            Guard.AgainstNullArgument(nameof(name), name);

            if (this.properties.TryGetValue(name, out var value))
            {
                return value;
            }

            foreach (var pair in this.properties.Where(pair => pair.Key.StartsWith(name)))
            {
                return pair.Value;
            }

            throw new PropertyNotFoundException($"The property '{name}' was not found", name);
        }

        #endregion

        #region Helpers

        /// <summary>
        ///     Puts all passed <paramref name="propertyCollection"/> into the internal
        ///     data structure
        /// </summary>
        /// <param name="propertyCollection">
        ///     The collection of <see cref="IAssignableProperty{TAttribute}"/> to place in
        ///     the container
        /// </param>
        private void PutPropertiesInContainer(IEnumerable<IAssignableProperty<TAttribute>> propertyCollection)
        {
            foreach (var property in propertyCollection)
            {
                if (!this.properties.ContainsKey(property.Name))
                {
                    this.properties.Add(property.Name, property);
                }
            }
        }

        #endregion
    }
}