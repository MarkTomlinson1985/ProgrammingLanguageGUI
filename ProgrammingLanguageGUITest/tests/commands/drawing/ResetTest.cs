using ProgrammingLanguageGUI.commands.drawing;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest.tests.commands.drawing
{
    /// <summary>
    /// Tests relating to the Reset class.
    /// </summary>
    [TestClass]
    public class ResetTest
    {
        Drawer drawer = new Drawer(new PictureBox());
        VariableManager variableManager = new VariableManager();

        /// <summary>
        /// Tests the creation and validation of a valid Reset command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments()
        {
            Reset command = new Reset();

            try
            {
                command.Execute(drawer, variableManager);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Reset object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments()
        {
            Reset command = new Reset("ARGUMENT");

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(drawer, variableManager));
            string expectedExceptionMessage = "Number of arguments incorrect. Expected: 0 - Actual: 1";

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}