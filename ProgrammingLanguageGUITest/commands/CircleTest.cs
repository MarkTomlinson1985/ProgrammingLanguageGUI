using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest {
    [TestClass]
    public class CircleTest {

        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments() {
            Circle command = new Circle("CIRCLE 100");

            try {
                command.ValidateCommand();
            } catch (Exception) {
                Assert.Fail();
            }
        }

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