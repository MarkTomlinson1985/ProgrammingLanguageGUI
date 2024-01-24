using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.tests.commands {
    /// <summary>
    /// Tests relating to the EndLoop class.
    /// </summary>
    [TestClass]
    public class EndLoopTest {
        VariableManager variableManager = new VariableManager();

        /// <summary>
        /// Tests the creation and validation of a valid EndLoop command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments() {
            EndLoop command = new EndLoop();

            try {
                command.Execute(variableManager);
            } catch (Exception) {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a EndLoop object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments() {
            EndLoop command = new EndLoop("ARGUMENT");

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(variableManager));
            string expectedExceptionMessage = "Number of arguments incorrect. Expected: 0 - Actual: 1";

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}