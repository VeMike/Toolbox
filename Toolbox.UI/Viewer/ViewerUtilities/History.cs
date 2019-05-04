using System.IO;
using System.Collections.Generic;

namespace Toolbox.UI.Viewer.ViewerUtilities
{
    /// <summary>
    ///     An implementation of <see cref="IHistory"/>
    /// </summary>
    internal class History : IHistory
    {
        #region Attributes
        /// <summary>
        ///     Contains the history of all displayed files.
        ///     One entry for each displayed file will exist.
        /// </summary>
        private readonly IDictionary<string, HistoryEntry> history;
        #endregion

        #region Constructor
        /// <summary>
        ///     Creates a new instance
        /// </summary>
        internal History()
        {
            this.history = new Dictionary<string, HistoryEntry>();
        }
        #endregion

        #region IHistory implementation
        /// <summary>
        ///     Searches the history for the passed file.
        /// </summary>
        /// <param name="pdfFile">
        ///     The file that should be searched
        /// </param>
        /// <returns>
        ///     True, if the file is in the history, 
        ///     false if not.
        /// </returns>
        public bool Exists(FileInfo pdfFile) => this.history.ContainsKey(pdfFile.FullName);

        /// <summary>
        ///     Adds a new file to the history
        /// </summary>
        /// <param name="pdfFile">
        ///     The pdf file to add to the history
        /// </param>
        /// <param name="pages">
        ///     The number of pages of the document
        /// </param>
        public void Add(FileInfo pdfFile, int pages)
        {
            if (!this.history.ContainsKey(pdfFile.FullName))
            {
                this.history.Add(pdfFile.FullName, new HistoryEntry(pdfFile, pages));
            }
        }

        /// <summary>
        ///     Erases the history of the passed file
        /// </summary>
        /// <param name="pdfFile">
        ///     The file whose history shall be erased
        /// </param>
        public void Erase(FileInfo pdfFile) => this.history.Remove(pdfFile.FullName);

        /// <summary>
        ///     Searches the <see cref="HistoryEntry"/> for the 
        ///     passed file.
        /// </summary>
        /// <param name="pdfFile">
        ///     The file whose histroy shall be searched
        /// </param>
        /// <returns>
        ///     The history entry of the passed file or
        ///     'null' if the file has no history.
        /// </returns>
        public HistoryEntry Search(FileInfo pdfFile) => this.history.TryGetValue(pdfFile.FullName, out var entry) ? entry : null;

        /// <summary>
        ///     Clears the complete history
        /// </summary>
        public void Clear() => this.history.Clear();
        #endregion
    }
}
