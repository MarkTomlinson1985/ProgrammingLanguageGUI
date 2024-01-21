using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;

namespace ProgrammingLanguageGUI {
    partial class Application {

        private static readonly string[] separator = ["\n"];

        private void TextEditor_TextChanged(object sender, EventArgs e) {
            int numberOfLines = programEditor.Text.Split("\n").Length;
            int lineNumbersLength = lineText.Text.Split(separator, StringSplitOptions.None).Length - 1;

            if (lineNumbersLength != numberOfLines) {
                lineText.Text = "";
                for (int i = 0; i < numberOfLines; i++) {
                    lineText.AppendText((i + 1).ToString());
                    lineText.AppendText(Environment.NewLine);
                }
            }
        }

        private void runCommand_Click(object sender, EventArgs e) {

            // This stuff probably needs to be in its own command controller class.
            try {
                Command command = commandProcessor.ParseCommand(commandText.Text);
                command.ValidateCommand();
                command.Execute();
                outputText.Text = "Command run successfully";
                // add a new CommandException that can be extended to more specific exception classes
            } catch (Exception ex) {
                outputText.Text = ex.Message;
            }

            commandText.Text = "";
        }

        private void runProgram_Click(object sender, EventArgs e) {
            string program = programEditor.Text;
            List<CommandException> exceptions = new List<CommandException>();
            List<Command> commands = new List<Command>();

            ProgramResults results = commandProcessor.ParseProgram(program);
            
            foreach (Command command in results.GetCommands().Keys) {
                commands.Add(command);
            }

            foreach (CommandException exception in results.GetExceptions().Keys) {
                exceptions.Add(new CommandException($"Line {results.GetExceptions()[exception]}: {exception.Message}"));
            }

            foreach (Command command in commands) {
                try {
                    command.ValidateCommand();
                    command.Execute();
                } catch (CommandException ex) {
                    exceptions.Add(new CommandException($"Line {results.GetCommands()[command]}: {ex.Message}"));
                }
            }

            if (exceptions.Count > 0) {
                outputText.Text = "";
                foreach (CommandException ex in exceptions) {
                    outputText.Text = outputText.Text + ex.Message + "\n";
                }
                return;
            }

            outputText.Text = "Program executed successfully.";
        }
    }
}
