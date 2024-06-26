﻿using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands.keywords.method {
    public class Method(params string[] arguments) : Command(arguments) {
        private string methodName;
        public string MethodName { get { return methodName; } }
        private string[] parameterNames;
        public string[] Parameters { get { return parameterNames; } }
        public Command[] Commands { get; set; }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
            variableManager.AddMethod(this);
        }

        public override void ValidateCommand(VariableManager variableManager) {
            if (arguments.Length < 1) {
                throw new CommandArgumentException("Number of arguments incorrect. Provide at least 1 arguments for method declaration.");
            }

            if (arguments[0] == string.Empty) {
                throw new CommandArgumentException("Invalid method name.");
            }

            methodName = arguments[0].Split('(')[0];

            int i = 0;
            if (int.TryParse(methodName, out i)) { 
                throw new CommandArgumentException($"Invalid method name '{methodName}': numeric value.");
            }

            // Method declaration with parameters
            if (arguments[0].Contains('(') && arguments[0].Contains(')')) {
                string parameters = arguments[0].Split('(')[1].Replace(")", "");
                parameterNames = parameters.Split(",");

                if (parameterNames.Any(parameter => int.TryParse(parameter, out i))) {
                    throw new CommandArgumentException("Invalid parameter identifier: numeric value.");
                }
                return;
            }

            parameterNames = [];
        }

        public override string ToString() {
            return $"METHOD {arguments[0]}";
        }
    }
}
