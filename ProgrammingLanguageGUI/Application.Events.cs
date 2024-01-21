using ProgrammingLanguageGUI.commands;
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
                Debug.WriteLine("Command " + commandText.Text + " is not valid");
                outputText.Text = ex.Message;
            }

            commandText.Text = "";
        }

        private void runProgram_Click(object sender, EventArgs e) {
            try {
                string program = programEditor.Text;
                List<Command> commands = commandProcessor.ParseProgram(program);
                // Have a list of exceptions that can be caught for the whole program
                // and printed out at the end.

                foreach (Command command in commands) {
                    // Have a try catch in here to catch individual exceptions.
                    command.ValidateCommand();
                    command.Execute();
                }

                outputText.Text = "Program executed successfully.";

            } catch (Exception ex) {
                outputText.Text = ex.Message;
            }
        }
    }
}
