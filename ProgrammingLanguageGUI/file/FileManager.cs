namespace ProgrammingLanguageGUI.file {
    
    /// <summary>
    /// Utility class relating to the saving and loading of files via dialog boxes.
    /// </summary>
    public static class FileManager {
        /// <summary>
        /// Property to determine the currently active file (if any).
        /// </summary>
        private static string activePath = string.Empty;
        private static string DEFAULT_PATH = "C:\\Users\\m_tom\\source\\repos\\ProgrammingLanguageGUI\\ProgrammingLanguageGUI\\resources\\examples\\";

        /// <summary>
        /// Prompts the user to save the given string to a txt file. Used for saving programs.
        /// If there is an active path defined (i.e a file has already been saved or loaded) then
        /// the file is saved automatically to the same path without need for a dialog box.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SaveTextToFile(string text) {
            try {
                if (activePath != string.Empty) {
                    using StreamWriter streamWriter = new StreamWriter(activePath);
                    streamWriter.Write(text);
                    return activePath + " saved successfully.";
                }

                using SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save Program File";
                saveFileDialog.FileName = "output.txt";
                saveFileDialog.InitialDirectory = DEFAULT_PATH;

                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    using StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                    streamWriter.Write(text);
                    activePath = saveFileDialog.FileName;
                    return saveFileDialog.FileName + " saved successfully.";
                }
            } catch (Exception ex) {
                return "Unable to save file: " + ex.ToString();
            }
            return "";
        }

        /// <summary>
        /// Prompts the user to load a txt file. Used for loading programs.
        /// </summary>
        /// <returns>File content in string format</returns>
        /// <exception cref="FileLoadException"></exception>
        public static string LoadFile() {
            try {
                using OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                openFileDialog.Title = "Open Program File";
                openFileDialog.FileName = "";
                openFileDialog.InitialDirectory = DEFAULT_PATH;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    string program = File.ReadAllText(openFileDialog.FileName);
                    activePath = openFileDialog.FileName;
                    return program;
                }
            } catch (Exception ex) {
                throw new FileLoadException("Unable to load file: " + ex.ToString());
            }
            return "";
        }

        /// <summary>
        /// Resets active path.
        /// </summary>
        public static void NewFile() {
            activePath = string.Empty;
        }
    }
}
