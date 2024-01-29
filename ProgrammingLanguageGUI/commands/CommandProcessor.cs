using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {

    /// <summary>
    /// Class for parsing programs and individual commands.
    /// </summary>
    public class CommandProcessor {
        /// <summary>
        /// Parses a program. Returns parsed commands and exceptions.
        /// </summary>
        /// <param name="program">New-line separated program string.</param>
        /// <returns></returns>
        public ProgramResults ParseProgram(string program) {
            string[] textCommands = program.Split("\n");
            Dictionary<Command, int> commands = new Dictionary<Command, int>();
            List<CommandException> exceptions = new List<CommandException>();

            for (int i = 0; i < textCommands.Length; i++) {
                try {
                    Command command = ParseCommand(textCommands[i]);
                    if (command is Empty) { continue; }
                    commands.Add(ParseCommand(textCommands[i]), i + 1);
                } catch (CommandNotFoundException ex) {
                    exceptions.Add(new CommandNotFoundException($"Line {(i + 1)}: {ex.Message}"));
                }
            }

            return new ProgramResults(commands, exceptions);
        }

        /// <summary>
        /// Parses an individual string-based command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>The corresponding Command object.</returns>
        /// <exception cref="CommandNotFoundException"></exception>
        public Command ParseCommand(string command) {
            string parsedCommand = command.Split("//")[0].Replace("\t", "").Trim();

            if (parsedCommand.Equals(string.Empty)) {  return Command.Empty; }

            Command? builtCommand = CommandFactory.BuildCommand(command.Split("//")[0].Replace("\t", "").Trim());

            if (builtCommand != null) {
                return builtCommand;
            }

            string commandType = command.Split(" ")[0];
            throw new CommandNotFoundException("Command " + commandType + " not recognised.");
        }
    }
}
