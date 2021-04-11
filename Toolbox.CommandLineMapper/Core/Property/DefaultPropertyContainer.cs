// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 20:18
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Com.Toolbox.Utils.Probing;
using Toolbox.CommandLineMapper.Specification;

namespace Toolbox.CommandLineMapper.Core.Property
{
    /// <summary>
    ///     An implementation of <see cref="IPropertyContainer{TAttribute}"/> that just
    ///     holds a collection of <see cref="IAssignableProperty{TAttribute}"/>s
    /// </summary>
    internal class DefaultPropertyContainer<TAttribute> : IPropertyContainer<TAttribute> where TAttribute : AttributeBase
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
        public int Properties => this.properties.Count;

        /// <inheritdoc />
        public IAssignableProperty<TAttribute> GetProperty(string name)
        {
            Guard.AgainstNullArgument(nameof(name), name);

            //Matches 'LongName' or full property name
            if (this.properties.TryGetValue(name.ToLower(), out var value))
            {
                return value;
            }
            
            //Matches 'ShortName' of the attribute
            var property = this.properties.Values.FirstOrDefault(val => val.Attribute.ShortName.Equals(name));

            if (property is null)
            {
                throw new PropertyNotFoundException($"The property '{name}' was not found", name);
            }

            return property;
        }
        
        /// <inheritdoc />
        public IEnumerator<string> GetEnumerator() => this.properties.Keys.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

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