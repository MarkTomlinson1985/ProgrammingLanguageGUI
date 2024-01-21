using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands {
    public class CommandProcessor {
        Drawer drawer;

        public CommandProcessor(Drawer drawer) {
            this.drawer = drawer;
        }

        public List<Command> ParseProgram(string program) {
            string[] textCommands = program.Split("\n");
            List<Command> commands = new List<Command>();

            foreach (string command in textCommands) {
                commands.Add(ParseCommand(command));
            }

            return commands;
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
            
            throw new NotImplementedException("Command " + commandType + " not recognised.");
        }
    }
}
