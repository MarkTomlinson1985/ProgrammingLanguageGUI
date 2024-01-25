using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.file;
using ProgrammingLanguageGUI.runner;
using ProgrammingLanguageGUI.syntaxparser;

namespace ProgrammingLanguageGUI {
    public partial class Application : Form {
        private static Application application;
        private Drawer drawer;
        private CommandProcessor commandProcessor;
        private Runner runner;
        private SyntaxParser syntaxParser;
        private VariableManager variableManager;
        private Color defaultColour;
        private static bool hasErrors = false;

        public Application() {
            InitializeComponent();
            application = this;
            drawer = new Drawer(drawingBox);
            variableManager = new VariableManager();
            commandProcessor = new CommandProcessor();
            runner = new Runner(commandProcessor, drawer, variableManager);
            syntaxParser = new SyntaxParser();
            defaultColour = programEditor.ForeColor;
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
        }

        private void ColorProgram() {
            int indexOfNextSpace = programEditor.Text.IndexOf(" ");
            programEditor.StopRepaint();
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
            programEditor.StartRepaint();
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
                    CheckProgramSyntax();
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

        private void programEditor_KeyPress(object sender, KeyPressEventArgs e) {
            // on Enter keypress
            if (e.KeyChar == (char)13) {
                CheckProgramSyntax();
            }
        }

        private void CheckProgramSyntax() {
            //ColorProgram();
            SyntaxResults results = runner.CheckProgramSyntax(programEditor.Text);

            if (!hasErrors && !results.HasErrors) { return; }

            if (!results.HasErrors) {
                ColourText(0, programEditor.Text.Length);
                hasErrors = false;
                return;
            }

            ShowErrors(results);
            hasErrors = true;
        }

        private void ShowErrors(SyntaxResults results) {
            outputText.Text = string.Join("\n", results.SyntaxErrors);

            for (int i = 0; i < programEditor.Lines.Length; i++) {
                int startIndex = programEditor.GetFirstCharIndexFromLine(i);
                int length = programEditor.Lines[i].Length;

                if (results.LineNumbers.Contains(i + 1)) {
                    ColourText(startIndex, length, Color.Red);
                } else {
                    ColourText(startIndex, length);
                }
            }
        }

        private void ColourText(int startIndex, int length) {
            ColourText(startIndex, length, defaultColour);
        }

        private void ColourText(int startIndex, int length, Color colour) {
            programEditor.StopRepaint();
            int currentIndex = programEditor.SelectionStart;

            programEditor.SelectionStart = startIndex;
            programEditor.SelectionLength = length;
            programEditor.SelectionColor = colour;
            programEditor.SelectionStart = currentIndex;
            programEditor.SelectionLength = 0;
            programEditor.StartRepaint();
        }

        public static void AddComponent(Control control) {
            application.Controls.Add(control);
        }
    }
}
