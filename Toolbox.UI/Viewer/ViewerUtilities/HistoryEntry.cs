using System;
using System.IO;

namespace Toolbox.UI.Viewer.ViewerUtilities
{
    /// <summary>
    ///     Encapsulates information about a file beeing
    ///     displayed in the viewer.
    /// </summary>
    public class HistoryEntry
    {
        #region Constructor
        /// <summary>
        ///     Creates a new instance
        /// </summary>
        /// <param name="pdfFile">
        ///     The pdf file this <see cref="HistoryEntry"/>
        ///     describes.
        /// </param>
        /// <param name="pages">
        ///     The number of pages the document has
        /// </param>
        public HistoryEntry(FileInfo pdfFile, int pages)
        {
            this.File = pdfFile;
            this.Pages = pages;
            this.Timestamp = DateTime.Now;
        } 
        #endregion

        #region Properties
        /// <summary>
        ///     The pdf file this <see cref="HistoryEntry"/>
        ///     describes
        /// </summary>
        public FileInfo File { get; }

        /// <summary>
        ///     The number of pages the file has
        /// </summary>
        public int Pages { get; }

        /// <summary>
        ///     The last page that was active in the
        ///     viewer before the file was removed from
        ///     the viewer.
        /// </summary>
        public int LastPage { get; set; }

        /// <summary>
        ///     The maximum page that was displayed in the 
        ///     viewer.
        /// </summary>
        public int MaximumPage { get; set; }

        /// <summary>
        ///     The date and time of this entries creation
        /// </summary>
        public DateTime Timestamp { get; } 
        #endregion
    }
}
