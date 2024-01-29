namespace ProgrammingLanguageGUI.syntaxparser {
    /// <summary>
    /// Contains defined variables for use in the SyntaxParser class.
    /// </summary>
    public class ColourConfig {
        public static Color COMMAND_WORDS_COLOUR = Color.Orange;
        public static Color KEY_WORDS_COLOUR = Color.FromArgb(218, 28, 232);
        public static Color NUMERIC_COLOUR = Color.FromArgb(27, 168, 100);
        public static Color TRANSFORM_COLOUR = Color.FromArgb(27, 168, 100);
        public static Color METHOD_COLOUR = Color.LightBlue;

        public static List<string> COMMAND_WORDS = new List<string>() {
            "circle", 
            "clear", 
            "drawto", 
            "fill", 
            "move", 
            "pen", 
            "rectangle", 
            "reset", 
            "triangle",
            "polygon"};

        public static List<string> KEY_WORDS = new List<string>() {
            "while", 
            "endloop",
            "if",
            "endif",
            "var", 
            "method",
            "endmethod"};

        public static List<string> TRANSFORM_WORDS = new List<string>() {
            "rotate"};
    }
}
