// ===================================================================================================
// = Author      :  Mike
// = Created     :  2021-03-06 16:33
// ===================================================================================================
// = Description :
// ===================================================================================================

using System.Collections.Generic;
using Toolbox.IniFileParser.Parsing.Files;

namespace Toolbox.IniFileParser.Test.Mock
{
    /// <summary>
    ///     A mock implementation of <see cref="ITextFile"/>
    ///     that only allows reading
    /// </summary>
    internal class MockReadableTextFile : ITextFile
    {
        /// <summary>
        ///     The lines of the file
        /// </summary>
        private IEnumerable<string> lines;
        
        /// <summary>
        ///     Creates a new instance
        /// </summary>
        /// <param name="lines">
        ///     The lines of the files
        /// </param>
        public MockReadableTextFile(IEnumerable<string> lines)
        {
            this.lines = lines;
        }
        
        /// <inheritdoc />
        public IEnumerable<Line> ReadLines()
        {
            var index = 0;

            foreach (var line in this.lines)
            {
                yield return new Line(index++, line);
            }
        }

        /// <inheritdoc />
        public void WriteLines(IEnumerable<Line> lines)
        {
        }
    }
}