using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.file;
using ProgrammingLanguageGUI.runner;

namespace ProgrammingLanguageGUI {
    public partial class Application : Form {
        private Drawer drawer;
        private CommandProcessor commandProcessor;
        private Runner runner;

        public Application() {
            InitializeComponent();
            drawer = new Drawer(drawingBox);
            commandProcessor = new CommandProcessor();
            runner = new Runner(commandProcessor, drawer);
        }

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

            // Implement Syntax parser code here.
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


        private void saveButton_Click(object sender, EventArgs e) {
            string program = programEditor.Text;
            outputText.Text = FileManager.SaveTextToFile(program);
        }

        private void openButton_Click(object sender, EventArgs e) {
            try {
                string program = FileManager.LoadFile();
                if (program != string.Empty) {
                    programEditor.Text = program;
                    outputText.Text = "Program loaded successfully.";
                }
            } catch (FileLoadException ex) {
                outputText.Text = ex.Message;
            }
        }

        private void newButton_Click(object sender, EventArgs e) {
            programEditor.Text = "";
            FileManager.NewFile();
        }

        private void commandText_KeyPress(object sender, KeyPressEventArgs e) {
            // on Enter keypress
            if (e.KeyChar == (char)13) {
                runCommand_Click(sender, e);
            }
        }
    }
}
