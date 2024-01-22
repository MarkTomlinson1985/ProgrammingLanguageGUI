using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.runner {
    public class Runner {
        private CommandProcessor processor;
        public Runner(CommandProcessor processor) {
            this.processor = processor;
        }

        public string RunCommand(string input) {
            try {
                Command command = processor.ParseCommand(input);
                command.ValidateCommand();
                command.Execute();
                return "Command run successfully";
            } catch (Exception ex) {
                return ex.Message;
            }
        }

        public string RunProgram(string input) {
            ProgramResults results = processor.ParseProgram(input);
            List<CommandException> exceptions = results.GetExceptions();

            foreach (Command command in results.GetCommands().Keys) {
                try {
                    command.ValidateCommand();
                    command.Execute();
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
