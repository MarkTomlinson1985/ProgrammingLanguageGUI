using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.method;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.tests.commands {
    /// <summary>
    /// Tests relating to the EndMethod class.
    /// </summary>
    [TestClass]
    public class EndMethodTest {
        Drawer drawer = new Drawer(new System.Windows.Forms.PictureBox());
        VariableManager variableManager = new VariableManager();

        /// <summary>
        /// Tests the creation and validation of a valid EndMethod command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments() {
            EndMethod command = new EndMethod();

            try {
                command.Execute(drawer, variableManager);
            } catch (Exception) {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a EndMethod object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments() {
            EndMethod command = new EndMethod("ARGUMENT");

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(drawer, variableManager));
            string expectedExceptionMessage = "Number of arguments incorrect. Expected: 0 - Actual: 1";

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}