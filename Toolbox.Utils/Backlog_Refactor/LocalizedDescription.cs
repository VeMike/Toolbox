using System;
using System.ComponentModel;
using System.Resources;

namespace Com.Toolbox.Utils.Backlog_Refactor
{
    /// <summary>
    ///     Allows to use localized description attributes.
    /// </summary>
    public class LocalizedDescription : DescriptionAttribute
    {
        #region Attributes
        /// <summary>
        ///     The key of the resource, that will be accessed
        /// </summary>
        private readonly string resourceKey;
        /// <summary>
        ///     The actual resource, that will be accessed
        /// </summary>
        private readonly ResourceManager resource;
        #endregion

        #region Constructor
        /// <summary>
        ///     The constructor
        /// </summary>
        /// <param name="resourceKey">
        ///     The key (name) of the resource, that will be accessed
        /// </param>
        /// <param name="resourceType">
        ///     The type of resource, that will be accessed
        /// </param>
        public LocalizedDescription(string resourceKey, Type resourceType)
        {
            this.resource = new ResourceManager(resourceType);
            this.resourceKey = resourceKey;
        }
        #endregion

        #region Overriding Properties
        /// <summary>
        ///     Accesses the value of the description attribute. In this
        ///     case, the value is the key of a resource inside the
        ///     resx-file, that corresponds to the current culture.
        /// </summary>
        public override string Description
        {
            get
            {
                try
                {
                    //Read the string from the resources
                    var displayName = this.resource.GetString(this.resourceKey);
                    //Check, if there is a resource with this name
                    return string.IsNullOrEmpty(displayName) ? this.resourceKey : displayName;
                }
                catch (Exception)
                {
                    return this.resourceKey;
                }
            }
        }
        #endregion
    }
}
