// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-01-11 20:18
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Collections.Generic;
using Com.Toolbox.Utils.Probing;

namespace Toolbox.CommandLineMapper.Core.Wrappers
{
    /// <summary>
    ///     An implementation of <see cref="IPropertyContainer"/> that just
    ///     holds a collection of <see cref="IAssignableProperty"/>s
    /// </summary>
    internal class PropertyContainer : IPropertyContainer
    {
        #region Attributes

        /// <summary>
        ///     The property container
        /// </summary>
        private Dictionary<string, IAssignableProperty> properties;

        #endregion

        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="container">
        ///     The containing type of the collection
        ///     of <paramref name="properties"/>
        /// </param>
        /// <param name="properties">
        ///     The collection of propertyCollection contained
        ///     in <paramref name="container"/>
        /// </param>
        public PropertyContainer(object container, IEnumerable<IAssignableProperty> properties)
        {
            Guard.AgainstNullArgument(nameof(container), container);

            this.Source = container;
            this.properties = new Dictionary<string, IAssignableProperty>();

            this.PutPropertiesInContainer(properties);
        } 

        #endregion


        #region IPropertyContainer Implementation

        /// <inheritdoc />
        public object Source { get; }

        /// <inheritdoc />
        public IAssignableProperty GetProperty(string name)
        {
            Guard.AgainstNullArgument(nameof(name), name);

            if (this.properties.TryGetValue(name, out var value))
            {
                return value;
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
        ///     The collection of <see cref="IAssignableProperty"/> to place in
        ///     the container
        /// </param>
        private void PutPropertiesInContainer(IEnumerable<IAssignableProperty> propertyCollection)
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