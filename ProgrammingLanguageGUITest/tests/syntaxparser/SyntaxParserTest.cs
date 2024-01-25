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
        /// Tests that the ParseWord method returns the expected colour.
        /// </summary>
        [TestMethod]
        [DataRow("Orange", "circle", "clear", "drawto", "fill", "move", "pen", "rectangle", "reset", "triangle")]
        [DataRow("Purple", "while", "if", "var", "method")]
        [DataRow("White", "50", "INVALID", "test")]
        public void ParseWordShouldReturnColour(string colourName, params string[] words) {
            Color colour = Color.FromName(colourName);
            foreach (string word in words) {
                Assert.AreEqual(colour, syntaxParser.ParseWord(word));
            }
        }

    }
}