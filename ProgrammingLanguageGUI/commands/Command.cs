using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    public abstract class Command : ICommand {
        protected Drawer drawer;
        protected string command;
        protected int numberOfArguments;

        public Command(string command, Drawer drawer) {
            this.command = command;
            this.drawer = drawer;
            numberOfArguments = 1;
        }

        public abstract void Execute();
        
        public virtual void ValidateCommand() {
            string[] argumentArray = new string[command.Split(" ").Length];

            if (argumentArray.Length != numberOfArguments) {
                throw new CommandArgumentException(
                    "Number of arguments incorrect. Expected: " + numberOfArguments + " - Actual: " + argumentArray.Length);
            }
        }
    }
}
