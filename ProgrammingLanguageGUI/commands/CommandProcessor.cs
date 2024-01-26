using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    public class CommandProcessor {

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
