using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.runner {
    public class Runner {
        private CommandProcessor processor;
        private Drawer drawer;

        public Runner(CommandProcessor processor, Drawer drawer) {
            this.processor = processor;
            this.drawer = drawer;
        }

        public string RunCommand(string input) {
            try {
                Command command = processor.ParseCommand(input);
                command.ValidateCommand();
                command.Execute(drawer);
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
                    command.ValidateCommand();
                    command.Execute(drawer);
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
