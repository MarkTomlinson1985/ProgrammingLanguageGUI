namespace ProgrammingLanguageGUI.syntaxparser {
    public class ColourConfig {
        public static Color COMMAND_WORDS_COLOUR = Color.Orange;
        public static Color KEY_WORDS_COLOUR = Color.FromArgb(218, 28, 232);
        public static Color NUMERIC_COLOUR = Color.FromArgb(27, 168, 100);


        public static List<string> COMMAND_WORDS = new List<string>() {
            "circle", 
            "clear", 
            "drawto", 
            "fill", 
            "move", 
            "pen", 
            "rectangle", 
            "reset", 
            "triangle" };

        public static List<string> KEY_WORDS = new List<string>() {
            "while", 
            "endloop",
            "if",
            "endif",
            "var", 
            "method",
            "endmethod"};


    }
}
