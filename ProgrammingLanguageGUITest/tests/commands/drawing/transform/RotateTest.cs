using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.loop;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.tests.commands.keywords.loop {
    /// <summary>
    /// Tests relating to the Rotate class.
    /// </summary>
    [TestClass]
    public class RotateTest {
        Drawer drawer = new Drawer(new System.Windows.Forms.PictureBox());
        VariableManager variableManager = new VariableManager();

        /// <summary>
        /// Tests the creation and validation of a valid Rotate command with inline command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        public void RotateCommandShouldExecuteWithInlineCommand() {
            Rotate command = (Rotate) CommandFactory.BuildCommand("ROTATE 0 0 POLYGON 100 50 200 500");

            try {
                command.Execute(drawer, variableManager);
            } catch (Exception) {
                Assert.Fail();
            }

            Assert.IsTrue(command.HasInlineCommand());
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Rotate object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        [DataRow("0 0 CIRCLE 50", "Unsupported command defined in transform statement.")]
        [DataRow("0 0 INVALID 50", "Invalid command defined in transform statement.")]
        [DataRow("0 0 ", "Invalid command defined in transform statement.")]
        [DataRow("a 0 ", "Provided rotation origin co-ordinates are not a valid number.")]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments(
            string arguments, string expectedExceptionMessage) {
            Rotate command = new Rotate(arguments.Split(" "));

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(drawer, variableManager));

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }
    }
}