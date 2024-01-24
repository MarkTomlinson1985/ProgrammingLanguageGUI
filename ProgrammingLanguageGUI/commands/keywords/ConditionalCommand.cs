using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.loop;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    public abstract class ConditionalCommand(params string[] arguments) : Command(arguments), ISelection {
        protected bool condition;

        protected override void ValidateCommand(VariableManager variableManager) {
            if (arguments.Length < 3) {
                throw new CommandArgumentException(
                    "Number of arguments incorrect. Provide at least 3 arguments for comparison.");
            }

            if (arguments[0] == string.Empty || arguments[2] == string.Empty) {
                throw new CommandArgumentException("Invalid comparator.");
            }

            try {
                int leftValue = int.Parse(GetVariableOrValue(arguments[0], variableManager));
                string conditionOperator = arguments[1];
                int rightValue = int.Parse(GetVariableOrValue(arguments[2], variableManager));

                condition = conditionOperator switch {
                    "!=" => leftValue != rightValue,
                    "==" => leftValue == rightValue,
                    "<" => leftValue < rightValue,
                    "<=" => leftValue <= rightValue,
                    ">" => leftValue > rightValue,
                    ">=" => leftValue >= rightValue,
                    _ => throw new CommandArgumentException("Invalid comparison operator in loop condition."),
                };
            } catch (FormatException) {
                throw new CommandArgumentException("Provided value is not a valid number.");
            }
        }

        public bool Evaluate() {
            return condition;
        }

    }
}
