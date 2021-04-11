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
        /// <param name="throwOnException">
        ///     If write access fails due to an exception,
        ///     it will be re-thrown if this is set to true
        /// </param>
        /// <returns>
        ///     'true', if the we can write to the passed
        ///     path, false if not.
        ///     Possible exceptions: <see cref="File.Create(string)"/>
        /// </returns>
        public static bool AbleToWrite(string fullPath, bool throwOnException)
        {
            if (string.IsNullOrEmpty(fullPath))
                return false;

            try
            {
                return CreateAndDeleteFileInDir(fullPath);
            }
            catch(Exception)
            {
                if (throwOnException)
                    throw;
                return false;
            }
        }

        #endregion

        #region Helpers
        /// <summary>
        ///    Creates a temporary file in <paramref name="directoryPath"/>
        ///     and deletes it again, if it was created.
        /// </summary>
        /// <param name="directoryPath">
        ///     The target directory for the temporary file
        /// </param>
        /// <returns>
        ///     'true', if the temporary file was successfully
        ///     created and deleted, 'false' otherwise.
        /// </returns>
        private static bool CreateAndDeleteFileInDir(string directoryPath)
        {
            var file = CreateFileInDir(directoryPath);

            DeleteFile(file);

            return true;
        }

        /// <summary>
        ///     Creates a single temporary file in <paramref name="directoryPath"/>
        /// </summary>
        /// <param name="directoryPath">
        ///     The target directory for the temporary file
        /// </param>
        /// <returns>
        ///     The created temporary file
        /// </returns>
        private static FileInfo CreateFileInDir(string directoryPath)
        {
            var file = new FileInfo(Path.Combine(directoryPath, TEMP_FILE_NAME));

            using (file.Create())
            {
            }

            return file;
        }

        /// <summary>
        ///     Deletes the passed temporary file from
        ///     the directory
        /// </summary>
        /// <param name="file">
        ///     The file, that will be deleted
        /// </param>
        private static void DeleteFile(FileSystemInfo file)
        {
            if (file.Exists)
                file.Delete();
        }

        #endregion
    }
}