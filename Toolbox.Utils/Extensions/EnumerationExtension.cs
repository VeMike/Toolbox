using System;
using System.ComponentModel;

namespace Com.Toolbox.Utils.Enumeration
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
                //Get the enums type
                var type = value.GetType();
                //The name of the enum
                var name = Enum.GetName(type, value);
                //Could the name be retreived?
                if (name != null)
                {
                    //A single enum value
                    var field = type.GetField(name);
                    //Did we get a field?
                    if (field != null)
                    {
                        //Get the attribute
                        //Did the cast succeed?
                        if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                            return attribute.Description;
                    }
                }
                //Return the default value
                return value.ToString();
            }
            catch (Exception)
            {
                //Return the default value
                return value.ToString();
            }
        }
        #endregion
    }
}
