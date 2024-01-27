using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.syntaxparser;
using System.Drawing;

namespace ProgrammingLanguageGUITest.tests.file {
    /// <summary>
    /// Tests relating to the SyntaxParser.
    /// </summary>
    [TestClass]
    public class SyntaxParserTest {
        private SyntaxParser syntaxParser = new SyntaxParser();

        /// <summary>
        /// Tests that the ParseWord method returns the expected colour for Command words.
        /// </summary>
        [TestMethod]
        [DataRow("circle", "clear", "drawto", "fill", "move", "pen", "rectangle", "reset", "triangle")]
        public void ParseWordShouldReturnExpectedColourForCommandWord(params string[] words) {
            Color colour = ColourConfig.COMMAND_WORDS_COLOUR;
            foreach (string word in words) {
                Assert.AreEqual(colour, syntaxParser.ParseWord(word, Color.Empty));
            }
        }

        /// <summary>
        /// Tests that the ParseWord method returns the expected colour for Key words.
        /// </summary>
        [TestMethod]
        [DataRow("while", "endloop", "if", "endif", "var", "method", "endmethod")]
        public void ParseWordShouldReturnExpectedColourForKeyWord(params string[] words) {
            Color colour = ColourConfig.KEY_WORDS_COLOUR;
            foreach (string word in words) {
                Assert.AreEqual(colour, syntaxParser.ParseWord(word, Color.Empty));
            }
        }

        /// <summary>
        /// Tests that the ParseWord method returns the expected colour for numeric input.
        /// </summary>
        [TestMethod]
        [DataRow("0", "10", "200", "-300")]
        public void ParseWordShouldReturnExpectedColourForNumericWord(params string[] words) {
            Color colour = ColourConfig.NUMERIC_COLOUR;
            foreach (string word in words) {
                Assert.AreEqual(colour, syntaxParser.ParseWord(word, Color.Empty));
            }
        }

        /// <summary>
        /// Tests that the ParseWord method returns the expected colour for numeric input.
        /// </summary>
        [TestMethod]
        [DataRow("rotate")]
        public void ParseWordShouldReturnExpectedColourForTransformWord(params string[] words) {
            Color colour = ColourConfig.TRANSFORM_COLOUR;
            foreach (string word in words) {
                Assert.AreEqual(colour, syntaxParser.ParseWord(word, Color.Empty));
            }
        }

        /// <summary>
        /// Tests that the ParseWord method returns the expected colour for numeric input.
        /// </summary>
        [TestMethod]
        [DataRow("myMethod()", "myMethod(100,100)")]
        public void ParseWordShouldReturnExpectedColourForMethod(params string[] words) {
            Color colour = ColourConfig.METHOD_COLOUR;
            foreach (string word in words) {
                Assert.AreEqual(colour, syntaxParser.ParseWord(word, Color.Empty));
            }
        }

        /// <summary>
        /// Tests that the ParseWord method returns the expected colour for miscellaneous input.
        /// </summary>
        [TestMethod]
        [DataRow("the", "test", "INVALID")]
        public void ParseWordShouldReturnDefaultColourForMiscellaneousWord(params string[] words) {
            Color colour = Color.White;
            foreach (string word in words) {
                Assert.AreEqual(colour, syntaxParser.ParseWord(word, colour));
            }
        }

    }
}