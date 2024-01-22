using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest {
    /// <summary>
    /// Tests relating to the Circle class.
    /// </summary>
    [TestClass]
    public class CircleTest {

        /// <summary>
        /// Tests the creation and validation of a valid Circle command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments() {
            Circle command = new Circle("CIRCLE 100");

            try {
                command.ValidateCommand();
            } catch (Exception) {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Circle object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        [DataRow("INVALID", "Provided radius is not a valid number.")]
        [DataRow("-100", "Provided radius must not be negative.")]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments(
            string argumentOne,
            string expectedExceptionMessage) {
            Circle command = new Circle($"CIRCLE {argumentOne}");
            
            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.ValidateCommand());

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}