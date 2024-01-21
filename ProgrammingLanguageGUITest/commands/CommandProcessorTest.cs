using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest {
    [TestClass]
    public class CommandProcessorTest {
        private CommandProcessor processor;
        private Drawer drawer = new Drawer(new PictureBox());

        [TestInitialize]
        public void Initialize() {
            processor = new CommandProcessor(drawer);
        }

        [TestMethod]
        [DataRow("MOVE 100 100", typeof(Move))]
        [DataRow("DRAWTO 100 100", typeof(DrawTo))]
        [DataRow("CIRCLE 100", typeof(Circle))]
        [DataRow("CLEAR", typeof(Clear))]
        public void ParseCommandShouldReturnCommandWithValidCommand(string command, Type expectedType) {
            Assert.IsInstanceOfType(processor.ParseCommand(command), expectedType);
        }

        [TestMethod]
        public void ParseCommandShouldThrowExceptionForInvalidCommand() { 
            Exception ex = Assert.ThrowsException<CommandNotFoundException>(
                () => processor.ParseCommand("INVALID 100 100"));

            Assert.AreEqual("Command INVALID not recognised.", ex.Message);
        }

        // Write a test for ParseProgram method.

    }
}