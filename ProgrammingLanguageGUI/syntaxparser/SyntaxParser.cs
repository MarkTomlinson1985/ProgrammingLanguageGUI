using System.Text.RegularExpressions;

namespace ProgrammingLanguageGUI.syntaxparser {
    /// <summary>
    /// Contains methods relating to syntax parsing.
    /// </summary>
    public class SyntaxParser {

        /// <summary>
        /// Returns a colour based on the provided word.
        /// If the word arugment is a recognised word, returns the appropriate
        /// colour for that type. Otherwise returns provided default colour.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="defaultColour"></param>
        /// <returns></returns>
        public Color ParseWord(string word, Color defaultColour) {
            if (ColourConfig.COMMAND_WORDS.Contains(word.ToLower())) {
                return ColourConfig.COMMAND_WORDS_COLOUR;
            } else if (ColourConfig.KEY_WORDS.Contains(word.ToLower())) {
                return ColourConfig.KEY_WORDS_COLOUR;
            } else if (ColourConfig.TRANSFORM_WORDS.Contains(word.ToLower())) {
                return ColourConfig.TRANSFORM_COLOUR;
            } else if (int.TryParse(word, out var key)) {
                return ColourConfig.NUMERIC_COLOUR;
            } else if (Regex.IsMatch(word, @"^(.*\(\)|.*\(.*\))$")) {
                return ColourConfig.METHOD_COLOUR;
            }
            return defaultColour;
        }
    }
}
