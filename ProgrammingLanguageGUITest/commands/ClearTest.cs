using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest {
    /// <summary>
    /// Tests relating to the Clear class.
    /// </summary>
    [TestClass]
    public class ClearTest {

        /// <summary>
        /// Tests the creation and validation of a valid Clear command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments() {
            Clear command = new Clear();

            try {
                command.ValidateCommand();
            } catch (Exception) {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Clear object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments() {
            Clear command = new Clear("ARGUMENT");
            
            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.ValidateCommand());
            string expectedExceptionMessage = "Number of arguments incorrect. Expected: 0 - Actual: 1";

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}