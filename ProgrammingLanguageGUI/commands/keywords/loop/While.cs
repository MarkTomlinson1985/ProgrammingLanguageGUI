using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands.keywords.loop {
    public class While : FunctionCommand, ILoop {
        private bool loopCondition;

        public While(params string[] arguments) : base(arguments) {
            numberOfArguments = 3;
        }

        public override void Execute(VariableManager variableManager) {
            ValidateCommand(variableManager);
        }

        protected override void ValidateCommand(VariableManager variableManager) {
            base.ValidateCommand(variableManager);
            try {
                int leftValue = int.Parse(GetVariableOrValue(arguments[0], variableManager));
                string conditionOperator = arguments[1];
                int rightValue = int.Parse(GetVariableOrValue(arguments[2], variableManager));

                loopCondition = conditionOperator switch {
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
            return loopCondition;
        }
    }
}
