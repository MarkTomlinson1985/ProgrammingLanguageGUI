using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest {
    [TestClass]
    public class ClearTest {

        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments() {
            Clear command = new Clear("CLEAR");

            try {
                command.ValidateCommand();
            } catch (Exception) {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments() {
            Clear command = new Clear("CLEAR ARGUMENT");
            
            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.ValidateCommand());
            string expectedExceptionMessage = "Number of arguments incorrect. Expected: 1 - Actual: 2";

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}