namespace ProgrammingLanguageGUI.syntaxparser {
    public class SyntaxParser {
        private static List<string> commandWords = new List<string>() {
            "circle", "clear", "drawto", "fill", "move", "pen", "rectangle", "reset", "triangle" };
        private static List<string> keyWords = new List<string>() {
            "while", "if", "var", "method" };

        public Color ParseWord(string word) {
            if (commandWords.Contains(word.ToLower())) {
                return Color.Orange;
            } else if (keyWords.Contains(word.ToLower())) {
                return Color.Purple;
            }

            return Color.White;
        }

    }
}
