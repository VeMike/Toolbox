using System;

namespace Toolbox.Utils.Common
{
    /// <summary>
    ///     An abstract singleton class
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the class that should be a singleton.
    ///     Note that this approach does not force the class
    ///     to be a singleton. It merely adds a singleton
    ///     capability. The class T can still have public 
    ///     constructors. Its the subclasses responsibility
    ///     to only provide a private parameterless constructor.
    /// </typeparam>
    public abstract class Singleton<T> where T : class
    {
        /// <summary>
        ///     The singleton instance
        /// </summary>
        private static readonly Lazy<T> instance = new Lazy<T>(CreateInstance, true);

        /// <summary>
        ///     Provides access to the singleton instance
        /// </summary>
        public static T Instance => instance.Value;

        /// <summary>
        ///     The factory that creates the singleton instance
        /// </summary>
        /// <returns>
        ///     The singleton instance of T
        /// </returns>
        private static T CreateInstance()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }
    }
}
