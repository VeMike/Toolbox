using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Parser.CommandLine
{
    interface ICommandLineParser
    {
        /// <summary>
        ///     Converts the value of command line argument to the appropriate type
        /// </summary>
        /// <typeparam name="T">
        ///     The type, the commands value should be converted to
        /// </typeparam>
        /// <param name="command">
        ///     The 'command' whose 'value' should be accessed.
        /// </param>
        /// <returns>
        ///     The command line value for the specified command line parameter converted to T. 
        ///     If the command is not found or any exception occurs while trying to convert it
        ///     default(T) is returned.
        /// </returns>
        T Get<T>(string command);

        /// <summary>
        ///     Sets the array of commands aquired from the command line, which will be parsed
        /// </summary>
        string[] Commands { set; }
    }
}
