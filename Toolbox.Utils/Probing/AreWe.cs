// ===================================================================================================
// = Author      :  Mike
// = Created     :  2019-07-26 23:47
// ===================================================================================================
// = Description :  A simple collections of operations to determine
//                  conditions and/or states
// ===================================================================================================

using System;
using System.IO;

namespace Com.Toolbox.Utils.Probing
{
    /// <summary>
    ///     A simple collections of operations to determine
    ///     conditions and/or states.
    /// </summary>
    public static class AreWe
    {
        #region Attributes

        /// <summary>
        ///     The GUID used as the temporary files name
        /// </summary>
        private const string TEMP_FILE_NAME = "E0F9D06C-DA29-4C64-B434-36776CA4D4E1";

        #endregion

        #region Methods

        /// <summary>
        ///     Tests, if program has write access inside
        ///     the directory of the passed path. It tries
        ///     to determine this by creating a file
        ///     temporarily and deleting it afterwards.
        /// </summary>
        /// <param name="fullPath">
        ///     The full path to the directory tested for
        ///     write access
        /// </param>
        /// <param name="throwOnException"> </param>
        /// <returns>
        ///     'true', if the we can write to the passed
        ///     path, false if not.
        ///     Possible exceptions: <see cref="File.Create(string)"/>
        /// </returns>
        public static bool AbleToWrite(string fullPath, bool throwOnException)
        {
            if (fullPath is null)
                return false;

            var pathToFile = Path.GetDirectoryName(fullPath)
                             + Path.DirectorySeparatorChar
                             + TEMP_FILE_NAME;

            try
            {
                using (File.Create(pathToFile))
                {
                }

                //Delete the file again
                if (File.Exists(pathToFile))
                    File.Delete(pathToFile);
                return true;
            }
            catch(Exception)
            {
                if (throwOnException)
                    throw;
                return false;
            }
        }

        #endregion
    }
}