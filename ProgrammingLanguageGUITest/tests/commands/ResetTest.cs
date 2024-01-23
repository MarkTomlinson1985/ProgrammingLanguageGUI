using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.tests.commands {
    /// <summary>
    /// Tests relating to the Reset class.
    /// </summary>
    [TestClass]
    public class ResetTest {

        /// <summary>
        /// Tests the creation and validation of a valid Reset command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments() {
            Reset command = new Reset();

            try {
                command.ValidateCommand();
            } catch (Exception) {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Reset object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments() {
            Reset command = new Reset("ARGUMENT");

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.ValidateCommand());
            string expectedExceptionMessage = "Number of arguments incorrect. Expected: 0 - Actual: 1";

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}