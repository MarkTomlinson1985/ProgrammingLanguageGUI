using ProgrammingLanguageGUI.commands.drawing;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest.tests.commands.drawing
{
    /// <summary>
    /// Tests relating to the Triangle class.
    /// </summary>
    [TestClass]
    public class TriangleTest
    {
        Drawer drawer = new Drawer(new PictureBox());
        VariableManager variableManager = new VariableManager();

        /// <summary>
        /// Tests the creation and validation of a valid Triangle command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments()
        {
            Triangle command = new Triangle("100", "100");

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
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Triangle object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        [DataRow("100", "INVALID", "Provided arguments are not valid numbers.")]
        [DataRow("-100", "100", "Provided size arguments must not be negative.")]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments(
            string argumentOne,
            string argumentTwo,
            string expectedExceptionMessage)
        {
            Triangle command = new Triangle(argumentOne, argumentTwo);

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(drawer, variableManager));

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}