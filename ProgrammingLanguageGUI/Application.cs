using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.file;
using ProgrammingLanguageGUI.runner;
using ProgrammingLanguageGUI.syntaxparser;

namespace ProgrammingLanguageGUI {
    public partial class Application : Form {
        private Drawer drawer;
        private CommandProcessor commandProcessor;
        private Runner runner;
        private SyntaxParser syntaxParser;
        private VariableManager variableManager;
        private Color defaultColour;
        private string lastCommand;

        public Application() {
            InitializeComponent();
            drawer = new Drawer(drawingBox);
            variableManager = new VariableManager();
            commandProcessor = new CommandProcessor();
            runner = new Runner(commandProcessor, drawer, variableManager);
            syntaxParser = new SyntaxParser();
            defaultColour = programEditor.ForeColor;
            lastCommand = "";

            FormClosing += Application_FormClosing;
        }

        private void Application_FormClosing(object? sender, FormClosingEventArgs e) {
            ThreadManager.TERMINATE_THREADS = true;
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

        private void runCommand_Click(object sender, EventArgs e) {
            string command = commandText.Text;

            //DEBUG
            if (command.ToLower().Equals("dt")) {
                ThreadManager.TERMINATE_THREADS = true;
                commandText.Text = "";
                return;
            }

            outputText.Text = runner.RunCommand(command);
            lastCommand = commandText.Text;
            commandText.Text = "";
        }

        private void runProgram_Click(object sender, EventArgs e) {
            string program = programEditor.Text;
            drawer.Reset();
            variableManager.Reset();
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
            if (Configuration.EnableSyntaxChecking) {
                SyntaxResults results = runner.CheckProgramSyntax(programEditor.Text);
                ColourProgram(results);
                ShowErrors(results);
            }
        }

        private void ShowErrors(SyntaxResults results) {
            outputText.Text = string.Join("\n", results.SyntaxErrors);

            for (int i = 0; i < programEditor.Lines.Length; i++) {
                if (results.LineNumbers.Contains(i + 1)) {
                    int startIndex = programEditor.GetFirstCharIndexFromLine(i);
                    int length = programEditor.Lines[i].Length;
                    ColourText(startIndex, length, Color.Red);
                }
            }
        }

        private void ColourProgram(SyntaxResults results) {
            for (int i = 0; i < programEditor.Lines.Length; i++) {
                int startIndex = programEditor.GetFirstCharIndexFromLine(i);
                int length = programEditor.Lines[i].Length;

                if (results.LineNumbers.Contains(i + 1)) {
                    continue;
                }

                if (length > 2 && programEditor.Lines[i].Trim().StartsWith("//")) {
                    ColourText(startIndex, length, Color.LightGreen);
                    continue;
                }

                ColourLine(startIndex, length);
            }
        }

        private void ColourLine(int startIndex, int lineLength) {
            int indexOfNextSpace = programEditor.Text.IndexOf(" ", startIndex);

            for (int i = startIndex; i < startIndex + lineLength; i++) {
                if (indexOfNextSpace == -1 || indexOfNextSpace < i || indexOfNextSpace > startIndex + lineLength) {
                    indexOfNextSpace = startIndex + lineLength;
                }

                string word = programEditor.Text.Substring(i, indexOfNextSpace - i);
                Color wordColour = syntaxParser.ParseWord(word.Trim(), defaultColour);

                ColourText(i, word.Length, wordColour);

                // Last word in the program
                if (indexOfNextSpace == programEditor.Text.Length) {
                    break;
                }

                int indexOfSpace = programEditor.Text.IndexOf(" ", indexOfNextSpace + 1);
                int indexOfNewLine = programEditor.Text.IndexOf("\n", indexOfNextSpace + 1);

                i = indexOfNextSpace;

                if (indexOfSpace != -1 || indexOfNewLine != -1) {
                    if (indexOfSpace != -1 && indexOfNewLine != -1) {
                        indexOfNextSpace = Math.Min(programEditor.Text.IndexOf(" ", indexOfNextSpace + 1), programEditor.Text.IndexOf("\n", indexOfNextSpace + 1));
                    } else {
                        indexOfNextSpace = Math.Max(indexOfSpace, indexOfNewLine);
                    }
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

        private void commandText_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Up) {
                commandText.Text = lastCommand;
                commandText.SelectionStart = commandText.TextLength;
            }
        }

        private void toggleSyntaxButton_Click(object sender, EventArgs e) {
            Configuration.EnableSyntaxChecking = !Configuration.EnableSyntaxChecking;
            toggleSyntaxButton.ForeColor = 
                Configuration.EnableSyntaxChecking == true 
                    ? Color.LimeGreen
                    : Color.LightGray;

            if (!Configuration.EnableSyntaxChecking) {
                int currentSelectionIndex = programEditor.SelectionStart;
                programEditor.StopRepaint();
                programEditor.SelectAll();
                programEditor.SelectionColor = defaultColour;
                programEditor.SelectionStart = currentSelectionIndex;
                programEditor.StartRepaint();
            } else {
                CheckProgramSyntax();
            }
        }

        private void toggleSyntaxButton_MouseHover(object sender, EventArgs e) {
            buttonTooltip.SetToolTip(
                toggleSyntaxButton,
                Configuration.EnableSyntaxChecking == true
                    ? "Disable syntax checking"
                    : "Enable syntax checking");
        }
    }
}
