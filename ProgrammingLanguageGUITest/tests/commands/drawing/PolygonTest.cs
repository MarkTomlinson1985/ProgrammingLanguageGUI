using ProgrammingLanguageGUI.commands.drawing;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest.tests.commands.drawing {
    /// <summary>
    /// Tests relating to the Polygon class.
    /// </summary>
    [TestClass]
    public class PolygonTest {
        Drawer drawer = new Drawer(new PictureBox());
        VariableManager variableManager = new VariableManager();

        /// <summary>
        /// Tests the creation and validation of a valid Polygon command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        [DataRow("0", "0", "100", "100")]
        [DataRow("100", "100", "200", "300")]
        [DataRow("100", "100", "300", "10", "0", "20")]
        public void ValidateCommandShouldSucceedWithValidArguments(params string[] arguments) {
            Polygon command = new Polygon(arguments);

            try {
                command.Execute(drawer, variableManager);
            } catch (Exception) {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Polygon object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        [DataRow("Invalid number of arguments: odd number of coordinates.", "100", "100", "100")]
        [DataRow("Provided coordinates must not be negative.", "-100", "100", "100", "120")]
        [DataRow("Provided coordinates are not valid numbers.", "0", "0", "100", "test")]
        [DataRow("Invalid number of arguments: polygon requires at least 2 points.", "100", "100")]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments(
            string expectedExceptionMessage, params string[] arguments) {
            Polygon command = new Polygon(arguments);

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(drawer, variableManager));

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}