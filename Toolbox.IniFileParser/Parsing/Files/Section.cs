// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-03-06 19:56
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     An implementation of <see cref="ISection"/>
    /// </summary>
    public class Section : ISection
    {
        #region Attributes

        /// <summary>
        ///     A collection of properties of this section.
        ///     The 'key' is the name of the property.
        /// </summary>
        private readonly Dictionary<string, IProperty> properties;

        #endregion
        
        #region Constructor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="name">
        ///     The name of the section
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is null
        /// </exception>
        public Section(string name)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));

            this.properties = new Dictionary<string, IProperty>();
        }

        #endregion

        #region ISection Implementation

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        /// <exception cref="KeyNotFoundException">
        ///     Thrown if the property with the passed
        ///     name is not found
        /// </exception>
        public IProperty this[string name] => this.properties[name];

        /// <inheritdoc />
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if there is no property at the
        ///     passed index
        /// </exception>
        public IProperty this[int index] => this.properties.ElementAt(index).Value;
        
        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed property is null
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown if the passed property already exists
        ///     in this section.
        /// </exception>
        public void Add(IProperty property)
        {
            if (property is null)
                throw new ArgumentNullException(nameof(property));
            
            this.properties.Add(property.Name, property);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if there is no property at the
        ///     passed index
        /// </exception>
        public void Remove(int index)
        {
            var remove = this.properties.ElementAt(index);

            this.properties.Remove(remove.Key);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is null
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        ///     Thrown if the passed property is not
        ///     found
        /// </exception>
        public void Remove(IProperty property)
        {
            if (property is null)
                throw new ArgumentNullException(nameof(property));
            
            this.properties.Remove(property.Name);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is null
        /// </exception>
        public bool Contains(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return this.properties.ContainsKey(name);
        }

        /// <inheritdoc />
        public int Properties => this.properties.Count;

        #endregion

        #region Equality

        protected bool Equals(Section other)
        {
            return this.Name == other.Name;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Section) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (this.Name != null ? this.Name.GetHashCode() : 0);
        }

        public static bool operator ==(Section left, Section right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Section left, Section right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region ToString

        /// <inheritdoc />
        public override string ToString()
        {
            return this.ToString("[", "]", "=");
        }

        public string ToString(string sectionStart, 
                               string sectionEnd,
                               string propertySeparator)
        {
            var builder = new StringBuilder();
            builder.Append(sectionStart);
            builder.Append(this.Name);
            builder.Append(sectionEnd);
            builder.AppendLine();

            foreach (var property in this.properties)
            {
                builder.Append(property.Value.ToString(propertySeparator));
                builder.AppendLine();
            }
            
            //We add two blank lines between sections
            builder.AppendLine();

            return builder.ToString();
        }

        #endregion
    }
}