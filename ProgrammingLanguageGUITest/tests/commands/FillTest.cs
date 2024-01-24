using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest.tests.commands {
    /// <summary>
    /// Tests relating to the Fill class.
    /// </summary>
    [TestClass]
    public class FillTest {
        Drawer drawer = new Drawer(new PictureBox());
        VariableManager variableManager = new VariableManager();

        /// <summary>
        /// Tests the creation and validation of a valid Fill command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        [DataRow("on")]
        [DataRow("oN")]
        [DataRow("off")]
        public void ValidateCommandShouldSucceedWithValidArguments(string colour) {
            Fill command = new Fill(colour);

            try {
                command.Execute(drawer, variableManager);
            } catch (Exception) {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Fill object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        [DataRow("INVALID", "Provided argument invalid - expected on/off.")]
        [DataRow("100", "Provided argument invalid - expected on/off.")]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments(
            string argumentOne,
            string expectedExceptionMessage) {
            Fill command = new Fill(argumentOne);

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(drawer, variableManager));

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}