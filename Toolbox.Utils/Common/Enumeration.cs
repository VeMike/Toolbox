using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Com.Toolbox.Utils.Common
{
    /// <summary>
    ///     This class contains extension methods for enums
    /// </summary>
    public static class EnumerationExtension
    {
        #region Extension Methods
        
        /// <summary>
        ///     An extension method, that returns the value of a DescriptionAttribute,
        ///     that was attached to the given enum.
        /// </summary>
        /// <param name="value">
        ///     The enum whose Description will be extracted
        /// </param>
        /// <returns>
        ///     The value of the description attribute or 'enum.ToString()' if there is no
        ///     description.
        /// </returns>
        public static string GetDescription(this Enum value)
        {
            try
            {
                var attribute = GetAttribute<DescriptionAttribute>(value);

                return attribute is null ? value.ToString() : attribute.Description;
            }
            catch (Exception)
            {
                return value.ToString();
            }
        }

        /// <summary>
        ///     Gets the <see cref="Attribute"/> that is applied to an
        ///     enum field
        /// </summary>
        /// <param name="value">
        ///    The enum value that has an attribute
        /// </param>
        /// <typeparam name="TAttribute">
        ///    The type of attribute that should be accessed
        /// </typeparam>
        /// <returns>
        ///    The attribute of the enum field or 'null'
        ///     if the enum does not have an attribute
        /// </returns>
        private static TAttribute GetAttribute<TAttribute>(Enum value) where TAttribute : System.Attribute
        {
            var field = GetField(value);

            if (field is null)
                return default;

            if (System.Attribute.GetCustomAttribute(field, typeof(TAttribute)) is TAttribute attribute)
            {
                return attribute;
            }

            return null;
        }

        /// <summary>
        ///     Gets the <see cref="FieldInfo"/> object that
        ///     represents the enum passed as argument
        /// </summary>
        /// <param name="value">
        ///    The <see cref="Enum"/> value whose field
        ///     is accessed
        /// </param>
        /// <returns>
        ///    The <see cref="FieldInfo"/> of <paramref name="value"/>
        ///     or 'null' if <see cref="FieldInfo"/> can not be accessed
        /// </returns>
        private static FieldInfo GetField(Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            return name is null ? null : type.GetField(name);
        }
        
        #endregion
    }
    
    /// <summary>
    ///     A collection of utility methods for <see cref="Enum"/>
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        ///     Iterates over all the defined values
        ///     of an enum.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the enum
        /// </typeparam>
        /// <returns>
        ///     A collection of the enums values
        /// </returns>
        public static IEnumerable<T> GetValues<T>()
        {
            //This is the case, if 'GetValues' returns null
            if (!(Enum.GetValues(typeof(T)) is T[] values))
            {
                yield break;
            }

            foreach (var value in values)
                yield return value;
        }
    }
}
