using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.syntaxparser;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.file;
using ProgrammingLanguageGUI.runner;

namespace ProgrammingLanguageGUI {
    public partial class Application : Form {
        private Drawer drawer;
        private CommandProcessor commandProcessor;
        private Runner runner;
        private SyntaxParser syntaxParser;
        private VariableManager variableManager;

        public Application() {
            InitializeComponent();
            drawer = new Drawer(drawingBox);
            variableManager = new VariableManager();
            commandProcessor = new CommandProcessor(variableManager);
            runner = new Runner(commandProcessor, drawer, variableManager);
            syntaxParser = new SyntaxParser();
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

            // Currently disabled due to selection flickering. Looks a bit naff!
            // Needs reworking.
            // ColorProgram();
            
        }

        private void ColorProgram() {
            int indexOfNextSpace = programEditor.Text.IndexOf(" ");
            for (int i = 0; i < programEditor.Text.Length; i++) {
                if (indexOfNextSpace == -1 || indexOfNextSpace < i) {
                    break;
                }

                string word = programEditor.Text.Substring(i, indexOfNextSpace - i);
                Color wordColour = syntaxParser.ParseWord(word);

                //if (!wordColour.Equals(Color.Empty)) {
                int currentCursorIndex = programEditor.SelectionStart;
                programEditor.SelectionStart = i;
                programEditor.SelectionLength = indexOfNextSpace - i;
                programEditor.SelectionColor = wordColour;
                programEditor.SelectionStart = currentCursorIndex;
                //}

                if (indexOfNextSpace == programEditor.Text.Length - 1) {
                    break;
                }

                i = indexOfNextSpace;
                int indexOfSpace = programEditor.Text.IndexOf(" ", indexOfNextSpace + 1);
                int indexOfNewLine = programEditor.Text.IndexOf("\n", indexOfNextSpace + 1);

                if (indexOfSpace != -1 || indexOfNewLine != -1) {
                    if (indexOfSpace != -1 && indexOfNewLine != -1) {
                        indexOfNextSpace = Math.Min(programEditor.Text.IndexOf(" ", indexOfNextSpace + 1), programEditor.Text.IndexOf("\n", indexOfNextSpace + 1));
                    } else {
                        indexOfNextSpace = Math.Max(indexOfSpace, indexOfNewLine);
                    }
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
            drawer.Reset();
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
            drawer.Reset();
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
