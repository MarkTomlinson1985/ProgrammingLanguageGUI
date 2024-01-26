namespace ProgrammingLanguageGUI.syntaxparser {
    public class SyntaxParser {
        public Color ParseWord(string word, Color defaultColour) {
            if (ColourConfig.COMMAND_WORDS.Contains(word.ToLower())) {
                return ColourConfig.COMMAND_WORDS_COLOUR;
            } else if (ColourConfig.KEY_WORDS.Contains(word.ToLower())) {
                return ColourConfig.KEY_WORDS_COLOUR;
            } else if (ColourConfig.TRANSFORM_WORDS.Contains(word.ToLower())) {
                return ColourConfig.TRANSFORM_COLOUR;
            } else if (int.TryParse(word, out var key)) {
                return ColourConfig.NUMERIC_COLOUR;
            }
            return defaultColour;
        }
    }
}
