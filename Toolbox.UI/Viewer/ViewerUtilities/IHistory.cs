using System.IO;

namespace Toolbox.UI.Viewer.ViewerUtilities
{
    /// <summary>
    ///     Defines the history of the PdfViewer. Each file
    ///     added to the viewer will be included in the history.
    ///     
    ///     To uniquely identify a file, the <see cref="IHistory"/>
    ///     uses the <see cref="FileInfo.FullName"/>
    /// </summary>
    public interface IHistory
    {
        #region Methods
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
        bool Exists(FileInfo pdfFile);

        /// <summary>
        ///     Adds a new file to the history
        /// </summary>
        /// <param name="pdfFile">
        ///     The pdf file to add to the history
        /// </param>
        /// <param name="pages">
        ///     The number of pages of the document
        /// </param>
        void Add(FileInfo pdfFile, int pages);

        /// <summary>
        ///     Erases the history of the passed file
        /// </summary>
        /// <param name="pdfFile">
        ///     The file whose history shall be erased
        /// </param>
        void Erase(FileInfo pdfFile);

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
        HistoryEntry Search(FileInfo pdfFile);

        /// <summary>
        ///     Clears the complete history
        /// </summary>
        void Clear();
        #endregion
    }
}
