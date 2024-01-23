

namespace ProgrammingLanguageGUI.commands.syntaxparser {
    public class SyntaxParser {
        private static List<string> commandWords = new List<string>() { "circle" };

        public Color ParseWord(string word) {
            if (commandWords.Contains(word.ToLower())) {
                return Color.Orange;
            }

            return Color.White;
        }

    }
}
