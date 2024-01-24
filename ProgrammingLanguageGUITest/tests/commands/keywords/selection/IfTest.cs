using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.loop;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.tests.commands.keywords.loop {
    /// <summary>
    /// Tests relating to the If class.
    /// </summary>
    [TestClass]
    public class IfTest {
        Drawer drawer = new Drawer(new System.Windows.Forms.PictureBox());
        VariableManager variableManager = new VariableManager();

        /// <summary>
        /// Tests the creation and validation of a valid If command. Any exception will result
        /// in a failure assertion.
        /// Note that in proper usage these values would not be hard-coded, instead at least one
        /// variable would be used. This variable assignment is done as part of the command processing flow.
        /// </summary>
        [TestMethod]
        [DataRow("10 != 9", true)]
        [DataRow("10 != 10", false)]
        [DataRow("10 == 10", true)]
        [DataRow("10 == 12", false)]
        [DataRow("5 < 10", true)]
        [DataRow("15 < 10", false)]
        [DataRow("10 <= 10", true)]
        [DataRow("9 <= 10", true)]
        [DataRow("11 <= 10", false)]
        [DataRow("10 > 5", true)]
        [DataRow("10 > 15", false)]
        [DataRow("10 >= 10", true)]
        [DataRow("15 >= 10", true)]
        [DataRow("15 >= 100", false)]
        public void WhileCommandShouldEvaluateCorrectlyWithValidArguments(string arguments, bool evaluation) {
            If command = new If(arguments.Split(" "));

            try {
                command.Execute(drawer, variableManager);
            } catch (Exception) {
                Assert.Fail();
            }

            Assert.AreEqual(evaluation, command.Evaluate());
        }

        public void WhileCommandShouldExecuteWithInlineCommand() {
            If command = new If("10", "==", "10", "CIRCLE", "50");

            try {
                command.Execute(drawer, variableManager);
            } catch (Exception) {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a If object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        [DataRow("10 ==", "Number of arguments incorrect. Provide at least 3 arguments for comparison.")]
        [DataRow("10 * 10", "Invalid comparison operator: '*'.")]
        [DataRow("10 ==  ", "Invalid comparator.")]
        [DataRow("10 < INVALID", "Provided value is not a valid number.")]
        [DataRow("10 == 10 IF", "Unsupported command defined in selection statement.")]
        [DataRow("10 == 10 INVALID 50", "Invalid command defined in selection statement.")]
        [DataRow("10 == 10  50", "Invalid command defined in selection statement.")]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments(
            string arguments, string expectedExceptionMessage) {
            If command = new If(arguments.Split(" "));

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(drawer, variableManager));

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }
    }
}