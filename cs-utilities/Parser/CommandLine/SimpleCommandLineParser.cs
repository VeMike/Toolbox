using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Utilities.Parser.CommandLine
{
    public class SimpleCommandLineParser : ICommandLineParser
    {
        //The prefix of the commands
        private const string COMMAND_PREFIX = "-";
        //An array of the available commands
        private static readonly string[] AVAILABLE_COMMANDS = {
                                                                "command1",
                                                                "command2",
                                                                "command3"
                                                            };
        //The storage for the parsed commands; Key: argument name; Value: argument value
        private readonly Dictionary<string, string> commands;

        public SimpleCommandLineParser()
        {
            //Initialize the dictionary with a default size
            this.commands = new Dictionary<string, string>();
        }

        public string[] Commands
        {
            set
            {
                //Clear any previous commands
                this.commands.Clear();
                //Parse the commands
                if (value != null)
                    this.ParseCommands(value);
            }
        }

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

        /// <summary>
        ///     Does the actual parsing of the parameters
        /// </summary>
        /// <param name="arguments">
        ///     The command line parameters. Expected format: -command1 value -command2 value
        /// </param>
        private void ParseCommands(string[] arguments)
        {
            //Create Command-Value-Pairs, filter out any disallowed command, put the commands in the dictionary
            var argPairs = arguments.Where((arg, i) => i < arguments.Length - 1)
                                    .Select((arg, i) => new { Command = arg.Replace(COMMAND_PREFIX, string.Empty).ToLower(), Value = arguments[i + 1].ToLower() })
                                    .Where(pair => AVAILABLE_COMMANDS.Contains(pair.Command));
            //Put the pairs in the dictionary
            foreach (var pair in argPairs)
                this.commands[pair.Command.Trim()] = pair.Value.Trim();
        }
    }
}
