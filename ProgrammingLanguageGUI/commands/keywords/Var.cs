using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands
{
    public class Var(params string[] arguments) : ConditionalCommand(arguments) {
        private string variableName;
        private string variableValue;

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
            variableManager.AddVariable(variableName, variableValue);
        }

        public override void ValidateCommand(VariableManager variableManager) {
            if (arguments.Length < 3) {
                throw new CommandArgumentException(
                    "Number of arguments incorrect. Provide at least 3 arguments for variable assignment.");
            }
            
            variableName = arguments[0];

            if (variableName == string.Empty) {
                throw new CommandArgumentException("Variable name invalid.");
            }

            if (!arguments[1].Equals("=")) {
                throw new CommandArgumentException("'=' operator not found. Usage var <variableName> = <value>.");
            }

            try {
                if (arguments.Length == 3) {
                    variableValue = int.Parse(GetVariableOrValue(arguments[2], variableManager)).ToString();
                    return;
                }

                if (arguments.Length % 2 == 0) {
                    throw new CommandArgumentException("Invalid number of arguments for variable assignment.");
                }

                // Limit to 5 arguments for now. a = 1 * 2
                if (arguments.Length > 5) {
                    throw new CommandArgumentException("Invalid number of arguments for variable assignment.");
                }

                int argumentOne = int.Parse(GetVariableOrValue(arguments[2], variableManager));
                int argumentTwo = int.Parse(GetVariableOrValue(arguments[4], variableManager));

                variableValue = arguments[3] switch {
                    "+" => (argumentOne + argumentTwo).ToString(),
                    "-" => (argumentOne - argumentTwo).ToString(),
                    "*" => (argumentOne * argumentTwo).ToString(),
                    "/" => (argumentOne / argumentTwo).ToString(),
                    "%" => (argumentOne % argumentTwo).ToString(),
                    _ => throw new CommandArgumentException("Invalid operator in variable assignment."),
                };
            } catch (FormatException) {
                throw new CommandArgumentException("Provided variable assignment is not a valid number.");
            }
        }

        public override string ToString() {
            return $"VAR {string.Join(" ", arguments)}";
        }
    }
}
