using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.tests.commands
{
    /// <summary>
    /// Tests relating to the Rectangle class.
    /// </summary>
    [TestClass]
    public class TriangleTest
    {

        /// <summary>
        /// Tests the creation and validation of a valid Rectangle command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments()
        {
            Triangle command = new Triangle("100", "100");

            try
            {
                command.ValidateCommand();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Rectangle object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        [DataRow("100", "INVALID", "Provided arguments are not valid numbers.")]
        [DataRow("-100", "100", "Provided size arguments must not be negative.")]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments(
            string argumentOne,
            string argumentTwo,
            string expectedExceptionMessage) {
            Triangle command = new Triangle(argumentOne, argumentTwo);

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.ValidateCommand());

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}