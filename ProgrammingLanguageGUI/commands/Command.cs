using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    /// <summary>
    /// Abstract base command class from which all other individual commands are derived.
    /// </summary>
    public abstract class Command {
        protected string[] arguments;
        public string[] Arguments { get { return arguments; } set { arguments = value; } }
        protected int numberOfArguments;

        /// <summary>
        /// Executes by validating the command then sending it to the drawer to render on screen.
        /// </summary>
        /// <param name="drawer"></param>
        /// <param name="variableManager"></param>
        public abstract void Execute(Drawer drawer, VariableManager variableManager);

        public Command(params string[] arguments) {
            this.arguments = arguments;
            numberOfArguments = 0;
        }

        /// <summary>
        /// Validates the contraints of the command class.
        /// </summary>
        /// <param name="variableManager"></param>
        /// <exception cref="CommandArgumentException"></exception>
        public virtual void ValidateCommand(VariableManager variableManager) {
            if (arguments.Length != numberOfArguments) {
                throw new CommandArgumentException(
                    "Number of arguments incorrect. Expected: " + numberOfArguments + " - Actual: " + arguments.Length);
            }
        }

        /// <summary>
        /// Base method that checks if a provided argument is a stored variable name. Returns the value
        /// of the variable if the variable exists, or the provided value if not.
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="variableManager"></param>
        /// <returns></returns>
        protected string GetVariableOrValue(string argument, VariableManager variableManager) {
            return variableManager.HasVariable(argument)
                ? variableManager.GetVariable(argument)
                : argument;
        }

        /// <summary>
        /// Static method to return an 'empty' command.
        /// </summary>
        public static Empty Empty { get { return new Empty(); } }
    }
}
