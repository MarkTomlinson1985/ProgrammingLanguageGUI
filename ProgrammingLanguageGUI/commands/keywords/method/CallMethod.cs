using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands.keywords.method {
    public class CallMethod(params string[] arguments) : Command(arguments) {
        private Drawer drawer;
        private Method method;
        private string methodName;
        public string MethodName { get { return methodName; } }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            this.drawer = drawer;
            ValidateCommand(variableManager);
        }

        public override void ValidateCommand(VariableManager variableManager) {
            methodName = arguments[0];

            if (!variableManager.HasMethod(methodName)) {
                throw new CommandNotFoundException($"Method not declared: '{methodName}'.");
            }

            method = variableManager.GetMethod(methodName);

            if (method.Parameters.Length != arguments.Length - 1) {
                throw new CommandArgumentException($"Invalid method parameters provided for method {methodName}; expected: {method.Parameters.Length} - provided {arguments.Length - 1}.");
            }

            for (int i = 0; i < method.Parameters.Length; i++) {
                string variableAssignment = $"VAR {method.Parameters[i]} = {arguments[i + 1]}";
                Command variable = CommandFactory.BuildCommand(variableAssignment);

                if (variable == null || !(variable is Var)) {
                    throw new CommandArgumentException("Unable to create variable: " + variableAssignment);
                }

                variable.Execute(drawer, variableManager);
            }
        }

        public void UnassignVariables(VariableManager variableManager) {
            foreach (string variable in method.Parameters) {
                variableManager.RemoveVariable(variable);
            }
        }

        public Command[] GetMethodCommands() {
            return method.Commands;
        }

        public override string ToString() {
            return $"{arguments[0]}({string.Join(",", arguments.Skip(1).ToArray())})";
        }
    }
}
