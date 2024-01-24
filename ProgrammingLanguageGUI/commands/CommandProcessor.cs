using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    public class CommandProcessor {
        private VariableManager variableManager;

        public CommandProcessor(VariableManager variableManager) {
            this.variableManager = variableManager;
        }

        public ProgramResults ParseProgram(string program) {
            string[] textCommands = program.Split("\n");
            Dictionary<Command, int> commands = new Dictionary<Command, int>();
            List<CommandException> exceptions = new List<CommandException>();

            for (int i = 0; i < textCommands.Length; i++) {
                try {
                    commands.Add(ParseCommand(textCommands[i]), i + 1);
                } catch (CommandNotFoundException ex) {
                    exceptions.Add(new CommandNotFoundException($"Line {(i + 1)}: {ex.Message}"));
                }
            }

            return new ProgramResults(commands, exceptions);
        }

        public Command ParseCommand(string command) {
            Command? builtCommand = CommandFactory.BuildCommand(command);

            if (builtCommand != null) {
                return builtCommand;
            }

            string commandType = command.Split(" ")[0];
            throw new CommandNotFoundException("Command " + commandType + " not recognised.");
        }
    }
}
