using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    public abstract class Command {
        protected string[] arguments;
        public string[] Arguments { get { return arguments; } set { arguments = value; } }
        protected int numberOfArguments;

        public Command(params string[] arguments) {
            this.arguments = arguments;
            numberOfArguments = 0;
        }

        protected virtual void ValidateCommand(VariableManager variableManager) {
            if (arguments.Length != numberOfArguments) {
                throw new CommandArgumentException(
                    "Number of arguments incorrect. Expected: " + numberOfArguments + " - Actual: " + arguments.Length);
            }
        }

        protected string GetVariableOrValue(string argument, VariableManager variableManager) {
            return variableManager.HasVariable(argument)
                ? variableManager.GetVariable(argument)
                : argument;
        }
    }
}
