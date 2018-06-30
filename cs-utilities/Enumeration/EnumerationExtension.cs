using System;
using System.ComponentModel;
using System.Reflection;

namespace Utilities.Enumeration
{
    /// <summary>
    ///     This class contains extension methods for enums
    /// </summary>
    public static class EnumerationExtension
    {
        #region Extension Methods
        /// <summary>
        ///     An extension method, that returns the value of a <see cref="DescriptionAttribute"/>,
        ///     that was attached to the given enum.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="Enum"/> whose Description will be extracted
        /// </param>
        /// <returns>
        ///     The value of the <see cref="DescriptionAttribute"/> or 'enum.ToString()' 
        ///     if there is no description.
        /// </returns>
        public static string GetDescription(this Enum value)
        {
            try
            {
                Type type = value.GetType();
                string name = Enum.GetName(type, value);
                if (name != null)
                {
                    FieldInfo field = type.GetField(name);
                    if (field != null)
                    {
                        DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                        if (attribute != null)
                        {
                            //The value of the description attribute
                            return attribute.Description;
                        }
                    }
                }
                //No DescriptionAttribute has been found. Just return ToString as default value.
                return value.ToString();
            }
            catch (Exception)
            {
                //Something went wrong. Just return enum.ToString()
                return value.ToString();
            }
        }
        #endregion
    }
}
