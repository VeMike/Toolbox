// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-02-28 19:22
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Toolbox.IniFileParser.Parsing.Files
{
    /// <summary>
    ///     An implementation of <see cref="ITextFile"/>
    ///     that uses a local file for reading and writing
    /// </summary>
    public class LocalTextFile : ITextFile
    {
        #region Attributes

        /// <summary>
        ///     The local file contents are
        ///     read from or written to
        /// </summary>
        private readonly FileInfo file;

        #endregion
        
        #region Construtor

        /// <summary>
        ///     Creates a new instance of the class
        /// </summary>
        /// <param name="file">
        ///     The local file contents are
        ///     read from or written to
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the passed argument is null
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown if the passed file does not
        ///     exist.
        /// </exception>
        public LocalTextFile(FileInfo file)
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file));
            
            if (!file.Exists)
                throw new ArgumentException($"The file '{file}' does not exist");
        }

        #endregion
        
        #region ITextFile Implementation

        /// <inheritdoc />
        public IEnumerable<Line> ReadLines()
        {
            var lineIndex = 0;
            
            foreach (var line in File.ReadLines(this.file.FullName))
            {
                yield return new Line(lineIndex, line);

                lineIndex++;
            }
        }

        /// <inheritdoc />
        public void WriteLines(IEnumerable<Line> lines)
        {
            using var writer = File.AppendText(this.file.FullName);
            foreach (var line in lines.OrderBy(l => l.Number))
            {
                writer.WriteLine(line.Content);
            }
        }

        #endregion
    }
}