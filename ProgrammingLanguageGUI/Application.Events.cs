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
            string command = commandText.Text;
            outputText.Text = runner.RunCommand(command);
            commandText.Text = "";
        }

        private void runProgram_Click(object sender, EventArgs e) {
            string program = programEditor.Text;
            outputText.Text = runner.RunProgram(program);
        }
    }
}
