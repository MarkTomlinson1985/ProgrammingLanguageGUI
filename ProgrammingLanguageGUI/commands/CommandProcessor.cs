using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands {
    public class CommandProcessor {
        Drawer drawer;

        public CommandProcessor(Drawer drawer) {
            this.drawer = drawer;
        }

        // write unit tests for this
        public Command ParseCommand(string command) {
            string commandType = command.Split(" ")[0];

            switch (commandType.ToLower()) {
                case "move":
                    return new Move(command, drawer);
                case "drawto":
                    return new DrawTo(command, drawer);
            }
            
            throw new NotImplementedException("Command " + commandType + " not recognised.");
        }
    }
}
