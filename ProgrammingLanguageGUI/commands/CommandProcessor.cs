namespace ProgrammingLanguageGUI.commands {
    public class CommandProcessor { 

        public CommandProcessor() {}

        // write unit tests for this
        public Command ParseCommand(string command) {
            string commandType = command.Split(" ")[0];

            switch (commandType.ToLower()) {
                case "move":
                    return new Move(command);
            }
            
            throw new NotImplementedException("Command " + commandType + " not recognised.");
        }
    }
}
