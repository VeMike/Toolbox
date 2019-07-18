using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Com.Toolbox.Utils.Common
{
    /// <summary>
    ///     An implementation of <see cref="ICommandLineParser"/>
    /// </summary>
    public class SimpleCommandLineParser : ICommandLineParser
    {
        #region Attributes
        /// <summary>
        ///     The prefix of the commands
        /// </summary>
        private const string COMMAND_PREFIX = "-";
        /// <summary>
        ///     An array of the available commands
        /// </summary>
        private static readonly string[] AVAILABLE_COMMANDS = {
                                                                "command1",
                                                                "command2",
                                                                "command3"
                                                                };
        /// <summary>
        ///     The storage for the parsed commands; Key: argument name; Value: argument value
        /// </summary>
        private Dictionary<string, string> commands;
        #endregion

        #region Constructor
        /// <summary>
        ///     Initializes the instance
        /// </summary>
        public SimpleCommandLineParser()
        {
            //Initialize the dictionary with a default size
            this.commands = new Dictionary<string, string>();
        } 
        #endregion

        #region ICommandLineParser implementation
        /// <summary>
        ///     Sets the commands array accessed from command line
        /// </summary>
        public string[] Commands
        {
            set
            {
                //Clear any previous commands
                this.commands.Clear();
                //Parse the commands
                if (value != null && value.Length % 2 == 0)
                    this.ParseCommandlineArguments(value);
            }
        }

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
        public T Get<T>(string command)
        {
            try
            {
                //Make the key lower case and trim any whitespaces
                command = command.ToLower().Trim();
                //The default value of the type T, if the command is not present
                if (!this.commands.ContainsKey(command))
                    return default(T);
                //The default value of the type T, if no command value was specified
                if (string.IsNullOrWhiteSpace(this.commands[command]) || this.commands[command] == string.Empty)
                    return default(T);
                //Convert the string the the specified type
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(this.commands[command]);
            }
            catch (Exception)
            {
                //If any exception occurs we just return the default value of T
                return default(T);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        ///     Parses the command line arguments
        /// </summary>
        /// <param name="arguments">
        ///     An array of command line arguments
        /// </param>
        private void ParseCommandlineArguments(string[] arguments)
        {
            //Create Command-Value-Pairs, filter out any disallowed command and put them in the dictionary
            this.commands = arguments.Where((arg, i) => i < arguments.Length - 1)
                         //Make some command <> command value pairs
                         .Select((arg, i) => new { Command = arg.Replace(COMMAND_PREFIX, string.Empty).ToLower(), Value = arguments[i + 1].ToLower() })
                         //Filter out any disallowed commands
                         .Where(pair => AVAILABLE_COMMANDS.Contains(pair.Command))
                         //Create a dictionary with the now filtered command/value pairs.
                         .ToDictionary(pair => pair.Command, pair => pair.Value);
        }
        #endregion
    }
}
