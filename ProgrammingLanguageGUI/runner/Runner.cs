using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.runner {
    public class Runner {
        private CommandProcessor processor;
        private Drawer drawer;
        private VariableManager variableManager;

        public Runner(CommandProcessor processor, Drawer drawer, VariableManager variableManager) {
            this.processor = processor;
            this.drawer = drawer;
            this.variableManager = variableManager;
        }

        public string RunCommand(string input) {
            try {
                Command command = processor.ParseCommand(input);
                processor.AssignVariables(command);

                command.ValidateCommand();
                if (command is DrawCommand drawCommand) {
                    drawCommand.Execute(drawer);
                } else if (command is FunctionCommand functionCommand) {
                    functionCommand.Execute(variableManager);
                }

                return "Command run successfully";
            } catch (Exception ex) {
                return ex.Message;
            }
        }

        public string RunProgram(string input) {
            drawer.Clear();
            ProgramResults results = processor.ParseProgram(input);
            List<CommandException> exceptions = results.GetExceptions();

            foreach (Command command in results.GetCommands().Keys) {
                try {
                    processor.AssignVariables(command);
                    command.ValidateCommand();

                    if (command is DrawCommand drawCommand) {
                        drawCommand.Execute(drawer);
                    } else if (command is FunctionCommand functionCommand) {
                        functionCommand.Execute(variableManager);
                    }

                } catch (CommandException ex) {
                    exceptions.Add(new CommandException($"Line {results.GetCommands()[command]}: {ex.Message}"));
                }
            }

            if (exceptions.Count > 0) {
                string exceptionOutput = string.Empty;
                foreach (CommandException ex in exceptions) {
                    exceptionOutput = exceptionOutput + ex.Message + "\n";
                }
                return exceptionOutput;
            }

            return "Program executed successfully.";
        }
    }
}
