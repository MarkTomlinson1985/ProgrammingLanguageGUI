using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.loop;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    /// <summary>
    /// Abstract command class. For commands that implement conditional logic.
    /// </summary>
    /// <param name="arguments"></param>
    public abstract class ConditionalCommand(params string[] arguments) : Command(arguments), ISelection {
        protected bool condition;

        public override void ValidateCommand(VariableManager variableManager) {
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

                // Evaluates the arguments of the condition.
                condition = conditionOperator switch {
                    "!=" => leftValue != rightValue,
                    "==" => leftValue == rightValue,
                    "<" => leftValue < rightValue,
                    "<=" => leftValue <= rightValue,
                    ">" => leftValue > rightValue,
                    ">=" => leftValue >= rightValue,
                    _ => throw new CommandArgumentException($"Invalid comparison operator: '{conditionOperator}'."),
                };
            } catch (FormatException) {
                throw new CommandArgumentException("Provided value is not a valid number.");
            }
        }

        /// <summary>
        /// Returns the result of the condition evaluation.
        /// </summary>
        /// <returns></returns>
        public bool Evaluate() {
            return condition;
        }

    }
}
