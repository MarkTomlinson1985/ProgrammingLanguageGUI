namespace ProgrammingLanguageGUI.file {
    public static class FileManager {
        private static string activePath = string.Empty;

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
                saveFileDialog.InitialDirectory = Environment.CurrentDirectory;

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

        public static string LoadFile() {
            try {
                using OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                openFileDialog.Title = "Open Program File";
                openFileDialog.FileName = "";
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;

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

        public static void NewFile() {
            activePath = string.Empty;
        }
    }
}
