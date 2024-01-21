using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest {
    [TestClass]
    public class ClearTest {
        private Drawer drawer = new Drawer(new PictureBox());

        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments() {
            Clear command = new Clear("CLEAR", drawer);

            try {
                command.ValidateCommand();
            } catch (Exception) {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments() {
            Clear command = new Clear("CLEAR ARGUMENT", drawer);
            
            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.ValidateCommand());
            string expectedExceptionMessage = "Number of arguments incorrect. Expected: 1 - Actual: 2";

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}