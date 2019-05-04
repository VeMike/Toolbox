using PdfiumViewer;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Toolbox.UI.Viewer.ViewerUtilities;

namespace Toolbox.UI.Viewer
{
    /// <summary>
    /// Interaktionslogik für PdfSpy.xaml
    /// </summary>
    public partial class PdfViewer : System.Windows.Controls.UserControl
    {
        //TODO: Implement event handlers for scroll and mousewheel

        #region Attributes
        /// <summary>
        ///     The dependency property definition for the <see cref="PdfFile"/>
        /// </summary>
        public static readonly DependencyProperty PdfFileProperty = DependencyProperty.Register(nameof(PdfFile), typeof(FileInfo), typeof(PdfViewer));
        
        /// <summary>
        ///     The dependency property definition for the initial page beeing displayed    
        /// </summary>
        public static readonly DependencyProperty InitialPageProperty = DependencyProperty.Register(nameof(InitialPage), typeof(int), typeof(PdfViewer));

        /// <summary>
        ///     Renders a Pdf file
        /// </summary>
        private readonly PdfRenderer renderer;

        /// <summary>
        ///     The history of files, that were viewed in this control
        /// </summary>
        private readonly IHistory history;
        #endregion

        #region Constructor
        /// <summary>
        ///     Initializes the control
        /// </summary>
        public PdfViewer()
        {
            this.InitializeComponent();
            this.history = new History();
            this.renderer = new PdfRenderer { Dock = DockStyle.Fill };
            this.windowsFormsHost.Child = this.renderer;
        }
        #endregion

        #region Properties
        /// <summary>
        ///     Gets or sets the Pdf file beeing displayed in
        ///     the control
        /// </summary>
        public FileInfo PdfFile
        {
            get => this.GetValue(PdfFileProperty) as FileInfo;
            set
            {
                /*
                 * Update the history of the old file (the one currently in display)
                 * brefore changing to a new one.
                 */
                this.UpdateHistory(this.PdfFile);
                //Put the new document into the view
                this.LoadNewDocument(value);
                this.SetValue(PdfFileProperty, value);
            }
        }

        /// <summary>
        ///     The initial page the renderer shall display
        /// </summary>
        public int InitialPage
        {
            get => (int) this.GetValue(InitialPageProperty);
            set
            {
                this.SetValue(InitialPageProperty, value);
                this.renderer.Page = value;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        ///     Loads a new pdf document into the view
        /// </summary>
        /// <param name="pdfFile">
        ///     The pdf document to load into the view
        /// </param>
        private void LoadNewDocument(FileInfo pdfFile)
        {
            if (pdfFile != null && pdfFile.Exists)
            {
                //The old, currently displayed document must be removed
                this.renderer.Document?.Dispose();
                //Load the new document
                var pdfDocument = PdfDocument.Load(pdfFile.FullName);
                //Add the passed file to the history
                this.history.Add(pdfFile, pdfDocument.PageCount);
                //Load the actual document into the view
                this.renderer.Load(pdfDocument);
                //Make the renderer visible in case an empty string was provided before
                this.Visibility = Visibility.Visible;
            }
            else
            {
                //Dispose the previously viewed PDF-file
                this.renderer.Document?.Dispose();
                //Set the controls visibility to hidden. The viewer displays a big red X if
                //no document is displayed. We do not want the user to see this.
                this.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        ///     Updates the internal history of this control
        /// </summary>
        /// <param name="pdfFile">
        ///     The file whose history shall be updated
        /// </param>
        private void UpdateHistory(FileInfo pdfFile)
        {
            if (pdfFile == null)
                return;
            //Get the history entry of the file
            var historyEntry = this.history.Search(pdfFile);
            if (historyEntry is null)
                return;
            //Access the page infos from the renderer
            historyEntry.LastPage = this.renderer.Page;
            //Update the maximum page the user scrolled to
            if (historyEntry.MaximumPage < this.renderer.Page)
                historyEntry.MaximumPage = this.renderer.Page;
        }
        #endregion

    }
}
