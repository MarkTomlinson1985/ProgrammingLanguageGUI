using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    public abstract class Command : ICommand {
        protected string[] arguments;
        protected int numberOfArguments;

        public Command(params string[] arguments) {
            this.arguments = arguments;
            numberOfArguments = 0;
        }

        public abstract void Execute(Drawer drawer);

        public virtual void ValidateCommand() {
            if (arguments.Length != numberOfArguments) {
                throw new CommandArgumentException(
                    "Number of arguments incorrect. Expected: " + numberOfArguments + " - Actual: " + arguments.Length);
            }
        }
    }
}
