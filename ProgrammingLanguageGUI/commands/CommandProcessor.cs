using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    public class CommandProcessor {
        Drawer drawer;

        public CommandProcessor(Drawer drawer) {
            this.drawer = drawer;
        }

        public ProgramResults ParseProgram(string program) {
            string[] textCommands = program.Split("\n");
            Dictionary<Command, int> commands = new Dictionary<Command, int>();
            Dictionary<CommandException, int> exceptions = new Dictionary<CommandException, int>();

            for (int i = 0; i < textCommands.Length; i++) {
                try {
                    commands.Add(ParseCommand(textCommands[i]), i + 1);
                } catch (CommandNotFoundException ex) {
                    exceptions.Add(ex, i + 1);
                }
            }

            return new ProgramResults(commands, exceptions);
        }

        public Command ParseCommand(string command) {
            string commandType = command.Split(" ")[0];

            switch (commandType.ToLower()) {
                case "move":
                    return new Move(command, drawer);
                case "drawto":
                    return new DrawTo(command, drawer);
                case "circle":
                    return new Circle(command, drawer);
                case "clear":
                    return new Clear(command, drawer);
            }
            
            throw new CommandNotFoundException("Command " + commandType + " not recognised.");
        }
    }
}
